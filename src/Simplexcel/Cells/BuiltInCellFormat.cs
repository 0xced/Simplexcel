﻿namespace Simplexcel;

/// <summary>
/// Excel Built-In Cell formats
/// </summary>
public static class BuiltInCellFormat
{
    /// <summary>
    /// General
    /// </summary>
    public const string General = "General";

    /// <summary>
    /// 0
    /// </summary>
    public const string NumberNoDecimalPlaces = "0";

    /// <summary>
    /// 0.00
    /// </summary>
    public const string NumberTwoDecimalPlaces = "0.00";

    /// <summary>
    /// 0%
    /// </summary>
    public const string PercentNoDecimalPlaces = "0%";

    /// <summary>
    /// 0.00%
    /// </summary>
    public const string PercentTwoDecimalPlaces = "0.00%";

    /// <summary>
    /// @
    /// </summary>
    public const string Text = "@";

    /// <summary>
    /// m/d/yy h:mm
    /// </summary>
    public const string DateAndTime = "m/d/yy h:mm";

    /// <summary>
    /// mm-dd-yy
    /// </summary>
    public const string DateOnly = "mm-dd-yy";

    /// <summary>
    /// h:mm
    /// </summary>
    public const string TimeOnly = "h:mm";
}