// Copyright (c) Makajda. All rights reserved. See LICENSE.md file in the solution root for full license information.

namespace ExtraHoliday.Services;
public class CountableToday : Countable {
    readonly DateTime today;

    public CountableToday(Day day, Pitstop pitstop, DateTime today) : base(day, pitstop, today) {
        this.today = today.Date;
        RecreateCutoff(0d);
    }

    protected override void RecreateCutoff(double delta) {
        try {
            double newValue;
            DateTime newDate;

            if (Date.Date >= Day.Date.Date) {
                newValue = Pitstop.Moreing(Value + delta);
                newDate = Pitstop.Add(Day.Date.Date, newValue);
            }
            else {
                newValue = Pitstop.Lessing(Value - delta);
                newDate = Pitstop.Add(Day.Date.Date, -newValue);
            }

            if (newDate.Date == today.Date) {
                Date = newDate;
                Value = newValue;
            }
            else {
                IsStoped = true;
            }
        }
        catch (ArgumentOutOfRangeException) {
            IsStoped = true;
        }
    }
}
