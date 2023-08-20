// Copyright (c) Makajda. All rights reserved. See LICENSE.md file in the solution root for full license information.

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace ExtraHoliday.Common;
public class BindableBase : IModifiable {
    public event PropertyChangedEventHandler PropertyChanged;
    [NotMapped, JsonIgnore] public bool IsModify { get; set; }

    protected void OnPropertyChanged(string propertyName) {
        IsModify = true;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool Setp<T>(ref T field, T value, [CallerMemberName] string propertyName = null) {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    public static DateTimeOffset SetDate(DateTime? value, DateTimeOffset? origin) => new(value?.Year ?? 2023, value?.Month ?? 12, value?.Day ?? 24,
        origin?.Hour ?? 0, origin?.Minute ?? 0, origin?.Second ?? 0, origin?.Offset ?? TimeSpan.Zero);

    public static DateTimeOffset SetTime(TimeSpan? value, DateTimeOffset? origin) => new(origin?.Year ?? 2023, origin?.Month ?? 12, origin?.Day ?? 25,
        value?.Hours ?? 0, value?.Minutes ?? 0, value?.Seconds ?? 0, origin?.Offset ?? TimeSpan.Zero);
}
