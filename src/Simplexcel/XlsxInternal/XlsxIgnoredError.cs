using System;
using System.Collections.Generic;

namespace Simplexcel.XlsxInternal;

internal sealed class XlsxIgnoredError
{
    private readonly IgnoredError _ignoredError = new();
    internal HashSet<CellAddress> Cells { get; } = [];

    internal IgnoredError IgnoredError
    {
        // Note: This is a mutable reference, but changing it would be... bad.
        get => _ignoredError;
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            // And because the reference is mutable, this stores a copy so that modifications to the Worksheet don't break this
            _ignoredError.NumberStoredAsText = value.NumberStoredAsText;
            _ignoredError.CalculatedColumn = value.CalculatedColumn;
            _ignoredError.EmptyCellReference = value.EmptyCellReference;
            _ignoredError.EvalError = value.EvalError;
            _ignoredError.Formula = value.Formula;
            _ignoredError.FormulaRange = value.FormulaRange;
            _ignoredError.ListDataValidation = value.ListDataValidation;
            _ignoredError.TwoDigitTextYear = value.TwoDigitTextYear;
            _ignoredError.UnlockedFormula = value.UnlockedFormula;
            IgnoredErrorId = _ignoredError.GetHashCode();
        }
    }

    internal int IgnoredErrorId { get; private set; }

    internal string GetSqRef()
    {
        var ranges = CellAddressHelper.DetermineRanges(Cells);
        return string.Join(" ", ranges);
    }
}