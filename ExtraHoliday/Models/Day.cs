// Copyright (c) Makajda. All rights reserved. See LICENSE.md file in the solution root for full license information.

namespace ExtraHoliday.Models;
public class Day : BindableBase {
    DateTime date; public DateTime Date { get => date; set => Setp(ref date, value); }

    string name; public string Name { get => name; set => Setp(ref name, value); }
    public Day Clone() => new() { Date = this.Date, Name = this.Name };

    public void CopyTo(Day result) {
        result.Date = this.Date;
        result.Name = this.Name;
    }

    // Time does not compare
    public override bool Equals(object obj) => obj is Day day && Date.Date == day.Date.Date && Name == day.Name;
    public override int GetHashCode() => HashCode.Combine(Date, Name);
    public override string ToString() => $"{base.ToString()}: {Date.ToShortDateString()}, {Name}";
}
