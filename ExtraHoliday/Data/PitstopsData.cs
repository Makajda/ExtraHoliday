// Copyright (c) Makajda. All rights reserved. See LICENSE.md file in the solution root for full license information.

namespace ExtraHoliday.Data;
public class PitstopsData {
    List<Pitstop> pitstops;
    public List<Pitstop> Pitstops { get => pitstops ??= GetAll(); }
    public List<Pitstop> GetAll() {
        const double SiderealPeriodVenus = 224.70069d;
        const double SiderealPeriodMars = 686.971d;
        const double SiderealPeriodJupiter = 4331.572d;
        const double SiderealPeriodSaturn = 10832.327d;
        const double DistanceMoon = 384401d;
        const double SpeedMoon = 5d;

        pitstops = new List<Pitstop> {
            // ***************************************************
            new Pitstop() {
                Key = Lankeys.Year,
                IsAllowZero = true,
                Add = (d, n) => d.AddYears((int)n),
                Total = (d, t) => {
                    var c = new DateTime(t.Year, d.Month, d.Day);
                    double tValue = Math.Abs(t.Year - d.Year);
                    if (t.Date >= d.Date) {
                        if (c.Date > t.Date) {
                            tValue -= 1d;
                        }
                    }
                    else {
                        if (c.Date < t.Date) {
                            tValue -= 1d;
                        }
                    }

                    return tValue;
                }
            },

            // ***************************************************
            new Pitstop() {
                Key = Lankeys.Venus,
                Add = (d, n) => d.AddDays(n * SiderealPeriodVenus),
                Total = (d, t) => (t - d).TotalDays / SiderealPeriodVenus
            },

            // ***************************************************
            new Pitstop() {
                Key = Lankeys.Mars,
                Add = (d, n) => d.AddDays(n * SiderealPeriodMars),
                Total = (d, t) => (t - d).TotalDays / SiderealPeriodMars
            },

            // ***************************************************
            new Pitstop() {
                Key = Lankeys.Jupiter,
                Add = (d, n) => d.AddDays(n * SiderealPeriodJupiter),
                Total = (d, t) => (t - d).TotalDays / SiderealPeriodJupiter
            },

            // ***************************************************
            new Pitstop() {
                Key = Lankeys.Saturn,
                Add = (d, n) => d.AddDays(n * SiderealPeriodSaturn),
                Total = (d, t) => (t - d).TotalDays / SiderealPeriodSaturn
            },

            // ***************************************************
            new Pitstop() {
                Key = Lankeys.Moon,
                Add = (d, n) => d.AddHours(n * DistanceMoon / SpeedMoon),
                Total = (d, t) => (t - d).TotalHours * SpeedMoon / DistanceMoon
            },

            // ***************************************************
            new PitstopCent() {
                Key = Lankeys.Month,
                Cent = 100d,
                Add = (d, n) => d.AddMonths((int)n),
                Total = (d, t) => {
                    double todayValue = Math.Abs((t.Year - d.Year) * 12 + t.Month - d.Month);
                    if (t.Date >= d.Date) {
                        if (t.Day < d.Day) {
                            todayValue -= 1d;
                        }
                    }
                    else {
                        if (d.Day < t.Day) {
                            todayValue -= 1d;
                        }
                    }

                    return todayValue;
                }
            },

            // ***************************************************
            new PitstopCent() {
                Key = Lankeys.Week,
                Cent = 100d,
                Add = (d, n) => d.AddDays(n * 7),
                Total = (d, t) => (t - d).TotalDays / 7
            },

            // ***************************************************
            new PitstopCent() {
                Key = Lankeys.Day,
                Cent = 1_000d,
                Add = (d, n) => d.AddDays(n),
                Total = (d, t) => (t - d).TotalDays
            },

            // ***************************************************
            new PitstopCent() {
                Key = Lankeys.Hour,
                Cent = 100_000d,
                Add = (d, n) => d.AddHours(n),
                Total = (d, t) => (t - d).TotalHours
            },

            // ***************************************************
            new PitstopCent() {
                Key = Lankeys.Minute,
                Cent = 1_000_000d,
                Add = (d, n) => d.AddMinutes(n),
                Total = (d, t) => (t - d).TotalMinutes
            },

            // ***************************************************
            new PitstopCent() {
                Key = Lankeys.Second,
                Cent = 100_000_000d,
                Add = (d, n) => d.AddSeconds(n),
                Total = (d, t) => (t - d).TotalSeconds
            }
        };

        var ln = new Ln();
        foreach (var pitstop in pitstops)
            pitstop.Title = ln[pitstop.Key];

        return pitstops;
    }
}
