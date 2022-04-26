using System;
using SixLabors.Fonts;

namespace Simplexcel.TestApp
{
    static class WorksheetExtensions
    {
        public static void AdjustWidthToContents(this Worksheet worksheet, IReadOnlyFontCollection fontCollection)
        {
            double? ComputeWidth(Cell cell)
            {
                if (cell.CellType == CellType.Formula)
                {
                    return null;
                }

                // Approximate displayable cell textual content, might not work for every cell because it depends on many factors (formula, formatting etc.)
                var text = cell.Value switch
                {
                    string value => value,
                    IFormattable formattable => formattable.ToString(),
                    _ => null
                };

                if (text == null)
                {
                    return null;
                }

                var fontFamily = fontCollection.Get(cell.FontName);
                var font = new Font(fontFamily, cell.FontSize, GetFontStyle(cell));
                var textOptions = new TextOptions(font) { Dpi = 40f / 3f }; // The 40/3 value was found empirically
                var rectangle = TextMeasurer.Measure(text, textOptions);
                return rectangle.Width;
            }

            foreach (var (address, cell) in worksheet.Cells)
            {
                var maxWidth = worksheet.ColumnWidths[address.Column];
                var cellWidth = ComputeWidth(cell);
                if (maxWidth == null || cellWidth > maxWidth)
                {
                    worksheet.ColumnWidths[address.Column] = cellWidth;
                }
            }
        }

        private static FontStyle GetFontStyle(Cell cell)
        {
            var fontStyle = FontStyle.Regular;
            if (cell.Bold) fontStyle |= FontStyle.Bold;
            if (cell.Italic) fontStyle |= FontStyle.Italic;
            return fontStyle;
        }
    }
}