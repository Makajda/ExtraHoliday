// Copyright (c) Makajda. All rights reserved. See LICENSE.md file in the solution root for full license information.

namespace ExtraHoliday.Services;
public class Calc(DaysData daysData, PitstopsData pitstopsData) {
    const int CountPast = 133;
    const int CountToday = int.MaxValue;
    const int CountFuture = 199;

    public async Task<(IEnumerable<Resultable> resultables, Resultable startToday)> Recalc() {
        var pitstops = pitstopsData.Pitstops;
        var days = await daysData.GetDays();
        Resultable startView = null;
        List<Resultable> resultablePast = null, resultableToday = null, resultableFuture = null;

        var tasks = new[] {
                    Task.Factory.StartNew(() =>
                    {
                        resultablePast = Recalc(days, pitstops, CountPast, (d, p, t) => new CountablePast(d, p, t), (x, y) => x > y);
                        startView = resultablePast.Skip(1).FirstOrDefault();
                        resultablePast.Reverse();
                    }),
                    Task.Factory.StartNew(() => resultableToday = Recalc(days, pitstops, CountToday, (d, p, t) => new CountableToday(d, p, t), (x, y) => x == y)),
                    Task.Factory.StartNew(() => resultableFuture = Recalc(days, pitstops, CountFuture, (d, p, t) => new CountableFuture(d, p, t), (x, y) => x < y))
                };

        await Task.WhenAll(tasks);

        List<Resultable> resultables = new();
        resultables.AddRange(resultablePast);
        resultables.AddRange(resultableToday);
        resultables.AddRange(resultableFuture);

        return (resultables, startView);
    }

    static List<Resultable> Recalc(IEnumerable<Day> days, IEnumerable<Pitstop> pitstops, int count,
        Func<Day, Pitstop, DateTime, Countable> createCountable, Func<DateTime, DateTime, bool> nextPredicate) {
        var resultables = new List<Resultable>();

        var countables = new List<Countable>();
        var today = DateTime.Today;

        foreach (var day in days) {
            foreach (var pitstop in pitstops) {
                countables.Add(createCountable(day, pitstop, today));
            }
        }

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
