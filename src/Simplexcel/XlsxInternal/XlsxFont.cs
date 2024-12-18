using System;

namespace Simplexcel.XlsxInternal;

internal class XlsxFont : IEquatable<XlsxFont>
{
    internal string Name { get; set; }
    internal int Size { get; set; }
    internal bool Bold { get; set; }
    internal bool Italic { get; set; }
    internal bool Underline { get; set; }
    internal Color TextColor { get; set; }

    internal XlsxFont()
    {
        Name = "Calibri";
        Size = 11;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != typeof(XlsxFont)) return false;
        return Equals((XlsxFont)obj);
    }

    public bool Equals(XlsxFont? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(other.Name, Name)
               && other.Size == Size
               && other.Bold.Equals(Bold)
               && other.Italic.Equals(Italic)
               && other.Underline.Equals(Underline)
               && other.TextColor.Equals(TextColor);
    }

    public override int GetHashCode() => HashCode.Combine(Name, Size, Bold, Italic, Underline, TextColor);

    public static bool operator ==(XlsxFont left, XlsxFont right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(XlsxFont left, XlsxFont right)
    {
        return !Equals(left, right);
    }
}