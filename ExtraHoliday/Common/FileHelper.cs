﻿// Copyright (c) Makajda. All rights reserved. See LICENSE.md file in the solution root for full license information.

namespace ExtraHoliday.Common;
public interface IFileHelper {
    Task WriteTextAsync(string filename, string text);
    Task<string> ReadTextAsync(string filename);
}

public static class FileHelper {
    static readonly IFileHelper fileHelper = DependencyService.Get<IFileHelper>();

    public static Task WriteTextAsync(string filename, string text) {
        return fileHelper.WriteTextAsync(filename, text);
    }

    public static Task<string> ReadTextAsync(string filename) {
        return fileHelper.ReadTextAsync(filename);
    }
}
