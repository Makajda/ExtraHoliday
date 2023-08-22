// Copyright (c) Makajda. All rights reserved. See LICENSE.md file in the solution root for full license information.

namespace ExtraHoliday.Common;

public static class FileHelper {
    public static async Task WriteTextAsync(string filename, string text) {
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var file = Path.Combine(folder, filename);
        await File.WriteAllTextAsync(file, text);
    }

    public static async Task<string> ReadTextAsync(string filename) {
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var file = Path.Combine(folder, filename);
        var text = await File.ReadAllTextAsync(file);
        return text;
    }
}