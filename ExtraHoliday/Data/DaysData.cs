// Copyright (c) Makajda. All rights reserved. See LICENSE.md file in the solution root for full license information.

namespace ExtraHoliday.Data;
public partial class DaysData {
    const string filename = "days.txt";
    const string dateFormat = "yyyyMMdd";

    public static async Task<List<Day>> GetAll() {
        List<Day> days = [];
        try {
            var r = await FileHelper.ReadTextAsync(filename);
            var s = r.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (var d in s) {
                var day = GetDay(d);
                if (day is not null)
                    days.Add(day);
            }
        }
        catch (Exception) { }

        if (days.Count == 0) {
            days.Add(new Day { Date = new DateTime(1732, 2, 22), Name = "George Washington" });
            days.Add(new Day { Date = new DateTime(1886, 5, 8), Name = "Coca-Cola" });
            days.Add(new Day { Date = new DateTime(1928, 11, 18), Name = "Mickey Mouse" });
            days.Add(new Day { Date = new DateTime(1954, 7, 5), Name = "That's All Right" });
            days.Add(new Day { Date = new DateTime(1998, 9, 4), Name = "Google" });
        }

        return days;
    }

    public static async Task Save(List<Day> days) {
        return;
        var r = string.Join(Environment.NewLine, days.Select((day) => $"{day.Date.ToString(dateFormat)} {day.Name}"));
        await FileHelper.WriteTextAsync(filename, r);
    }

    static Day GetDay(string line) {
        Day day = new();

        if (line.Length > 0) {
            if (DateTime.TryParseExact(
                line[..8],
                dateFormat,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.AssumeUniversal,
                out DateTime date)) {
                day.Date = date;
            }

            if (line.Length > 8)
                day.Name = line[8..].Trim();
        }

        return day;
    }
}
