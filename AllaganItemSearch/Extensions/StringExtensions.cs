using System;
using System.Collections.Generic;

namespace AllaganItemSearch.Extensions;

public static class StringExtensions
{
    private static readonly Dictionary<string, Func<int, TimeSpan>> TimeUnitMap = new(StringComparer.OrdinalIgnoreCase)
    {
        { "second", value => TimeSpan.FromSeconds(value) },
        { "seconds", value => TimeSpan.FromSeconds(value) },
        { "minute", value => TimeSpan.FromMinutes(value) },
        { "minutes", value => TimeSpan.FromMinutes(value) },
        { "hour", value => TimeSpan.FromHours(value) },
        { "hours", value => TimeSpan.FromHours(value) },
        { "day", value => TimeSpan.FromDays(value) },
        { "days", value => TimeSpan.FromDays(value) },
        { "week", value => TimeSpan.FromDays(value * 7) },
        { "weeks", value => TimeSpan.FromDays(value * 7) },
        { "month", value => TimeSpan.FromDays(value * 30) }, // Approximation
        { "months", value => TimeSpan.FromDays(value * 30) }, // Approximation
        { "year", value => TimeSpan.FromDays(value * 365) }, // Approximation
        { "years", value => TimeSpan.FromDays(value * 365) }, // Approximation
    };
}
