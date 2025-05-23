﻿using System;
using Simplexcel.XlsxInternal;

namespace Simplexcel;

/// <summary>
/// A cell inside a Worksheet
/// </summary>
public sealed class Cell
{
    internal XlsxCellStyle XlsxCellStyle { get; }

    /// <summary>
    /// Create a new Cell of the given <see cref="CellType"/>.
    /// You can also implicitly create a cell from a string or number.
    /// </summary>
    /// <param name="cellType"></param>
    public Cell(CellType cellType) : this(cellType, null, BuiltInCellFormat.General) { }

    /// <summary>
    /// Create a new Cell of the given <see cref="CellType"/>, with the given value and format. For some common formats, see <see cref="BuiltInCellFormat"/>.
    /// You can also implicitly create a cell from a string or number.
    /// </summary>
    /// <param name="type">The Type of the cell.</param>
    /// <param name="value">The Content of the cell.</param>
    /// <param name="format">The Excel Format for the cell, see <see cref="BuiltInCellFormat"/></param>
    public Cell(CellType type, object value, string format)
    {
        // Validate the type of the value argument here in order to avoid InvalidCastException later in Simplexcel.XlsxInternal.XlsxWriter.XlsxWriterInternal.GetXlsxRows
        switch (type)
        {
            case CellType.Text when value is not string:
                throw new ArgumentException("The value must be a string for text cells.", nameof(value));
            case CellType.Number when value is not decimal:
                throw new ArgumentException("The value must be a decimal for number cells.", nameof(value));
            case CellType.Date when value is not null && value is not DateTime:
                throw new ArgumentException("The value must be a DateTime for date cells.", nameof(value));
            case CellType.Formula when value is not (Simplexcel.Formula or string):
                throw new ArgumentException("The value must be a Formula or a string for formula cells.", nameof(value));
        }

        XlsxCellStyle = new XlsxCellStyle
        {
            Format = format
        };

        Value = value;
        CellType = type;
        IgnoredErrors = new IgnoredError();
    }

    /// <summary>
    /// The Excel Format for the cell, see <see cref="BuiltInCellFormat"/>
    /// </summary>
    public string Format
    {
        get => XlsxCellStyle.Format;
        set => XlsxCellStyle.Format = value;
    }

    /// <summary>
    /// The border around the cell
    /// </summary>
    public CellBorder Border
    {
        get => XlsxCellStyle.Border;
        set => XlsxCellStyle.Border = value;
    }

    /// <summary>
    /// The name of the Font (Default: Calibri)
    /// </summary>
    public string FontName
    {
        get => XlsxCellStyle.Font.Name;
        set => XlsxCellStyle.Font.Name = value;
    }

    /// <summary>
    /// The Size of the Font (Default: 11)
    /// </summary>
    public int FontSize
    {
        get => XlsxCellStyle.Font.Size;
        set => XlsxCellStyle.Font.Size = value;
    }

    /// <summary>
    /// Should the text be bold?
    /// </summary>
    public bool Bold
    {
        get => XlsxCellStyle.Font.Bold;
        set => XlsxCellStyle.Font.Bold = value;
    }

    /// <summary>
    /// Should the text be italic?
    /// </summary>
    public bool Italic
    {
        get => XlsxCellStyle.Font.Italic;
        set => XlsxCellStyle.Font.Italic = value;
    }

    /// <summary>
    /// Should the text be underlined?
    /// </summary>
    public bool Underline
    {
        get => XlsxCellStyle.Font.Underline;
        set => XlsxCellStyle.Font.Underline = value;
    }

    /// <summary>
    /// The font color.
    /// </summary>
    public Color TextColor
    {
        get => XlsxCellStyle.Font.TextColor;
        set => XlsxCellStyle.Font.TextColor = value;
    }

    /// <summary>
    /// The interior/fill color.
    /// </summary>
    public PatternFill Fill
    {
        get => XlsxCellStyle.Fill;
        set => XlsxCellStyle.Fill = value;
    }

    /// <summary>
    /// The Horizontal Alignment of content within a Cell
    /// </summary>
    public HorizontalAlign HorizontalAlignment
    {
        get => XlsxCellStyle.HorizontalAlignment;
        set => XlsxCellStyle.HorizontalAlignment = value;
    }

    /// <summary>
    /// The Vertical Alignment of content within this Cell
    /// </summary>
    public VerticalAlign VerticalAlignment
    {
        get => XlsxCellStyle.VerticalAlignment;
        set => XlsxCellStyle.VerticalAlignment = value;
    }

    /// <summary>
    /// Any errors that are ignored in this Cell
    /// </summary>
    public IgnoredError IgnoredErrors { get; }

    /// <summary>
    /// The Type of the cell.
    /// </summary>
    public CellType CellType { get; }

    /// <summary>
    /// The Content of the cell.
    /// </summary>
    public object Value { get; set; }

    /// <summary>
    /// Should this cell be a Hyperlink to something?
    /// </summary>
    public string Hyperlink { get; set; }

    /// <summary>
    /// Create a new <see cref="Cell"/> that includes a Formula (e.g., SUM(A1:A5)). Do not include the initial <c>=</c> sign.
    /// </summary>
    /// <param name="formula">The formula, without the initial <c>=</c> sign (so "SUM(A1:A5)", not "=SUM(A1:A5)")</param>
    /// <param name="format">The Excel Format for the cell, see <see cref="BuiltInCellFormat"/></param>
    /// <returns></returns>
    public static Cell Formula(Formula formula, string format = BuiltInCellFormat.General)
    {
        return new Cell(CellType.Formula, formula, format);
    }

    /// <summary>
    /// Create a new <see cref="Cell"/> with a <see cref="CellType"/> of Text from a string.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static implicit operator Cell(string value)
    {
        return new Cell(CellType.Text, value, BuiltInCellFormat.Text);
    }

    /// <summary>
    /// Create a new <see cref="Cell"/> with a <see cref="CellType"/> of Number (formatted without decimal places) from an integer.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static implicit operator Cell(long value)
    {
        return new Cell(CellType.Number, Convert.ToDecimal(value), BuiltInCellFormat.NumberNoDecimalPlaces);
    }

    /// <summary>
    /// Create a new <see cref="Cell"/> with a <see cref="CellType"/> of Number (formatted with 2 decimal places) places from a decimal.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static implicit operator Cell(Decimal value)
    {
        return new Cell(CellType.Number, value, BuiltInCellFormat.NumberTwoDecimalPlaces);
    }

    /// <summary>
    /// Create a new <see cref="Cell"/> with a <see cref="CellType"/> of Number (formatted with 2 decimal places) places from a double.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static implicit operator Cell(double value)
    {
        return new Cell(CellType.Number, Convert.ToDecimal(value), BuiltInCellFormat.NumberTwoDecimalPlaces);
    }

    /// <summary>
    /// Create a new <see cref="Cell"/> with a <see cref="CellType"/> of Date, formatted as DateAndTime.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static implicit operator Cell(DateTime value)
    {
        return new Cell(CellType.Date, value, BuiltInCellFormat.DateAndTime);
    }

    /// <summary>
    /// Create a Cell from the given object, trying to determine the best cell type/format.
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static Cell FromObject(object val)
    {
        Cell cell;
        if (val is sbyte or short or int or long or byte or uint or ushort or ulong)
        {
            cell = new Cell(CellType.Number, Convert.ToDecimal(val), BuiltInCellFormat.NumberNoDecimalPlaces);
        }
        else if (val is float or double or decimal)
        {
            cell = new Cell(CellType.Number, Convert.ToDecimal(val), BuiltInCellFormat.General);
        }
        else if (val is DateTime)
        {
            cell = new Cell(CellType.Date, val, BuiltInCellFormat.DateAndTime);
        }
        else
        {
            cell = new Cell(CellType.Text, val?.ToString() ?? string.Empty, BuiltInCellFormat.Text);
        }
        return cell;
    }

    /// <summary>
    /// The largest positive number Excel can handle before <see cref="LargeNumberHandlingMode"/> applies
    /// </summary>
    public static decimal LargeNumberPositiveLimit => 99999999999m;

    /// <summary>
    /// The largest negative number Excel can handle before <see cref="LargeNumberHandlingMode"/> applies
    /// </summary>
    public static decimal LargeNumberNegativeLimit => -99999999999m;

    /// <summary>
    /// Check if the given number is so large that <see cref="LargeNumberHandlingMode"/> would apply to it
    /// </summary>
    /// <param name="number">The number to check</param>
    /// <returns></returns>
    public static bool IsLargeNumber(decimal? number) => number.HasValue && (number.Value < LargeNumberNegativeLimit || number.Value > LargeNumberPositiveLimit);
}