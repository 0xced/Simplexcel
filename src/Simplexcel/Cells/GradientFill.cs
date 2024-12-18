using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplexcel;

// TODO: Not Yet Implemented
internal class GradientFill : IEquatable<GradientFill>
{
    public IList<GradientStop> Stops { get; } = [];

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != typeof(GradientFill)) return false;
        return Equals((GradientFill)obj);
    }

    public bool Equals(GradientFill? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return other.Stops.SequenceEqual(Stops);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        foreach (var stop in Stops)
        {
            hashCode.Add(stop);
        }
        return hashCode.ToHashCode();
    }

    public static bool operator ==(GradientFill left, GradientFill right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(GradientFill left, GradientFill right)
    {
        return !Equals(left, right);
    }
}