// Copyright (c) Makajda. All rights reserved. See LICENSE.md file in the solution root for full license information.

namespace ExtraHoliday.Services;
public class Calc(DaysData daysRepository) {
    const int CountPast = 133;
    const int CountToday = int.MaxValue;
    const int CountFuture = 199;

    public async Task<(IEnumerable<Resultable> resultables, Resultable startToday)> Recalc() {
        var days = await daysRepository.GetDays();
        var pitstops = PitstopsData.GetAll();

        Resultable startToday = null;
        IEnumerable<Resultable> resultables = null;

        if (days != null && pitstops != null && days.Count != 0 && pitstops.Count != 0) {
            IEnumerable<Resultable> resultablePast = null, resultableToday = null, resultableFuture = null;

            var tasks = new[] {
                    Task.Factory.StartNew(() =>
                    {
                        resultablePast = Recalc(days, pitstops, CountPast, (d, p, t) => new CountablePast(d, p, t), (x, y) => x > y).Reverse();
                        startToday = resultablePast.LastOrDefault();
                    }),
                    Task.Factory.StartNew(() => resultableToday = Recalc(days, pitstops, CountToday, (d, p, t) => new CountableToday(d, p, t), (x, y) => x == y)),
                    Task.Factory.StartNew(() => resultableFuture = Recalc(days, pitstops, CountFuture, (d, p, t) => new CountableFuture(d, p, t), (x, y) => x < y))
                };

            await Task.WhenAll(tasks);
            resultables = resultablePast.Union(resultableToday).Union(resultableFuture);
        }

        return (resultables, startToday);
    }

    static IEnumerable<Resultable> Recalc(IEnumerable<Day> days, IEnumerable<Pitstop> pitstops, int count,
        Func<Day, Pitstop, DateTime, Countable> createCountable, Func<DateTime, DateTime, bool> nextPredicate) {
        var resultables = new List<Resultable>();

        var countables = new List<Countable>();
        var today = DateTime.Today;

        foreach (var day in days) {
            foreach (var pitstop in pitstops) {
                countables.Add(createCountable(day, pitstop, today));
            }
        }

        //var nonStopedCountables = countables.Where(n => !n.IsStoped);
        //while (nonStopedCountables.Any()) {
        //    var selCountable = nonStopedCountables.Aggregate((x, y) => nextPredicate(x.Date.Date, y.Date.Date) ? x : y);
        //    if (selCountable.Value > 0 || selCountable.Pitstop.IsAllowZero) {
        //        resultables.Add(new Resultable(selCountable));
        //        if (resultables.Count >= count) {
        //            break;
        //        }
        //    }

        //    selCountable.CalcNext();
        //    nonStopedCountables = nonStopedCountables.Where(n => !n.IsStoped);
        //}

        while (true) {
            var selCountable = countables.Aggregate((x, y) => x.IsStoped ? y :
                (y.IsStoped ? x : (nextPredicate(x.Date.Date, y.Date.Date) ? x : y)));

            if (selCountable.IsStoped) {
                break;
            }

            if (selCountable.Value > 0 || selCountable.Pitstop.IsAllowZero) {
                resultables.Add(new Resultable(selCountable));
                if (resultables.Count >= count) {
                    break;
                }
            }

            selCountable.CalcNext();
        }

        return resultables;
    }
}
