// Copyright (c) Makajda. All rights reserved. See LICENSE.md file in the solution root for full license information.

namespace ExtraHoliday.Services;
public class Calc(DataContext data) {
    const int CountPast = 77;
    const int CountToday = int.MaxValue;
    const int CountFuture = 199;

    public async Task<(IEnumerable<Hoda> hodas, Hoda startToday)> Recalc() {
        Hoda startView = null;
        List<Hoda> hodaPast = null, hodaToday = null, hodaFuture = null;

        var tasks = new[] {
                    Task.Factory.StartNew(() =>
                    {
                        hodaPast = Recalc(CountPast, (d, p, t) => new CountablePast(d, p, t), (x, y) => x > y);
                        startView = hodaPast.Skip(1).FirstOrDefault();
                        hodaPast.Reverse();
                    }),
                    Task.Factory.StartNew(() => hodaToday = Recalc(CountToday, (d, p, t) => new CountableToday(d, p, t), (x, y) => x == y)),
                    Task.Factory.StartNew(() => hodaFuture = Recalc(CountFuture, (d, p, t) => new CountableFuture(d, p, t), (x, y) => x < y))
                };

        await Task.WhenAll(tasks);

        List<Hoda> hodas = new();
        hodas.AddRange(hodaPast);
        hodas.AddRange(hodaToday);
        hodas.AddRange(hodaFuture);

        return (hodas, startView);
    }

    List<Hoda> Recalc(int count, Func<Day, Pitstop, DateTime, Countable> createCountable, Func<DateTime, DateTime, bool> nextPredicate) {
        var hodas = new List<Hoda>();
        var countables = new List<Countable>();
        var today = DateTime.Today;

        foreach (var day in data.Days)
            foreach (var pitstop in data.Pitstops)
                countables.Add(createCountable(day, pitstop, today));

        while (true) {
            var selCountable = countables.Aggregate((x, y) => x.IsStoped ? y :
                (y.IsStoped ? x : (nextPredicate(x.Date.Date, y.Date.Date) ? x : y)));

            if (selCountable.IsStoped) {
                break;
            }

            if (selCountable.Value > 0 || selCountable.Pitstop.IsAllowZero) {
                hodas.Add(new Hoda(selCountable));
                if (hodas.Count >= count) {
                    break;
                }
            }

            selCountable.CalcNext();
        }

        return hodas;
    }
}
