using System;

namespace Simplexcel;

/// <summary>
/// A formula and its pre-computed value.
/// <para>
/// The pre-computed value is important for applications beside Excel that can read xlsx files.
/// This is, for example, the value which is used by Quick Look on macOS. If the pre-computed
/// value is not supplied, then Quick Look displays an empty cell.
/// </para>
/// </summary>
/// <remarks>
/// The pre-computed value must be supplied because evaluating the formula is outside the scope of this library.
/// </remarks>
public sealed class Formula
{
    /// <summary>
    /// Create a new <see cref="Formula"/> from a string expression, without the leading <c>=</c> sign. E.g. <c>SUM(A1:A5)</c>
    /// </summary>
    public static implicit operator Formula(string expression) => new(expression);

    /// <summary>
    /// Initializes a new instance of the <see cref="Formula"/> class.
    /// </summary>
    /// <param name="expression">The Excel formula expression, without the leading <c>=</c> sign. E.g. <c>SUM(A1:A5)</c></param>
    /// <param name="computedValue">The most recent computed value for the formula.</param>
    public Formula(string expression, object computedValue = null)
    {
        Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        ComputedValue = computedValue;
    }

    /// <summary>
    /// The Excel formula expression, without the leading <c>=</c> sign. E.g. <c>SUM(A1:A5)</c>
    /// </summary>
    public string Expression { get; }

    /// <summary>
    /// The most recent computed value for the formula.
    /// </summary>
    public object ComputedValue { get; }

    /// <inheritdoc/>
    public override string ToString() => Expression;
}