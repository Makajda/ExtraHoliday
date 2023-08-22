// Copyright (c) Makajda. All rights reserved. See LICENSE.md file in the solution root for full license information.

namespace ExtraHoliday.Data;
public class DaysData {
    const string filename = "days.txt";
    const string dateFormat = "yyyyMMdd";

    public async Task<List<Day>> GetAll() {
        var days = new List<Day>();
        try {
            var r = await FileHelper.ReadTextAsync(filename);
            var s = r.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var d in s) {
                var day = GetDay(d);
                if (day != null)
                    days.Add(day);
            }
        }
        catch (Exception) { }

        if (days.Count == 0) {
            days.Add(new Day { Date = new DateTime(1732, 2, 22), Name = "George Washington" });
            days.Add(new Day { Date = new DateTime(1884, 5, 8), Name = "Harry Truman" });
            days.Add(new Day { Date = new DateTime(1913, 1, 9), Name = "Richard Nixon" });
            days.Add(new Day { Date = new DateTime(1911, 2, 6), Name = "Ronald Reagan" });
            days.Add(new Day { Date = new DateTime(1946, 7, 6), Name = "George Bush" });
            days.Add(new Day { Date = new DateTime(1961, 8, 4), Name = "Barack Obama" });
            days.Add(new Day { Date = new DateTime(1946, 6, 14), Name = "Donald Trump" });
            days.Add(new Day { Date = new DateTime(1942, 11, 20), Name = "Joseph Biden" });
        }

        return days;
    }

    public async Task Save(List<Day> days) {
        var r = string.Join(Environment.NewLine, days.Select((day) => $"{day.Date.ToString(dateFormat)} {day.Name}"));
        await FileHelper.WriteTextAsync(filename, r);
    }

    static Day GetDay(string line) {
        var day = new Day();

        if (line.Length > 0) {
            if (DateTime.TryParseExact(
                line[..8],
                dateFormat,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.AssumeUniversal,
                out DateTime date)) {
                day.Date = date;
            }

            if (line.Length > 8) {
                day.Name = line[8..].Trim();
            }
        }

        return day;
    }
}
