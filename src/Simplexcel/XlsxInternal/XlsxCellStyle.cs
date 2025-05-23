﻿using System;

namespace Simplexcel.XlsxInternal;

/// <summary>
/// The Style Information about a cell. Isolated from the Cell object because we need to compare styles when building the styles.xml file
/// </summary>
internal class XlsxCellStyle : IEquatable<XlsxCellStyle>
{
    internal XlsxFont Font { get; set; } = new();
        
    internal CellBorder Border { get; set; }
        
    internal string Format { get; set; }

    internal VerticalAlign VerticalAlignment { get; set; }

    internal HorizontalAlign HorizontalAlignment { get; set; }

    internal PatternFill Fill { get; set; } = new();

    /// <summary>
    /// Compare this <see cref="XlsxCellStyle"/> to another <see cref="XlsxCellStyle"/>
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != typeof(XlsxCellStyle)) return false;
        return Equals((XlsxCellStyle)obj);
    }

    /// <summary>
    /// Compare this <see cref="XlsxCellStyle"/> to another <see cref="XlsxCellStyle"/>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(XlsxCellStyle other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return Equals(other.Border, Border)
               && other.Font.Equals(Font)
               && other.Format.Equals(Format)
               && Equals(other.VerticalAlignment, VerticalAlignment)
               && Equals(other.HorizontalAlignment, HorizontalAlignment)
               && Equals(other.Fill, Fill)
            ;
    }

    public override int GetHashCode() => HashCode.Combine(Border, Font, Format, VerticalAlignment, HorizontalAlignment, Fill);

    /// <summary>
    /// Compare a <see cref="XlsxCellStyle"/> to another <see cref="XlsxCellStyle"/>
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(XlsxCellStyle left, XlsxCellStyle right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Compare a <see cref="XlsxCellStyle"/> to another <see cref="XlsxCellStyle"/>
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(XlsxCellStyle left, XlsxCellStyle right)
    {
        return !Equals(left, right);
    }
}