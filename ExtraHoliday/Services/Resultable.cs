// Copyright (c) Makajda. All rights reserved. See LICENSE.md file in the solution root for full license information.

namespace ExtraHoliday.Services;
public class Resultable(Countable countable) {
    public Day Day { get; private set; } = countable.Day;
    public Pitstop Pitstop { get; private set; } = countable.Pitstop;
    public DateTime Date { get; private set; } = countable.Date;
    public double Value { get; private set; } = countable.Value;
}
