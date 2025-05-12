# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [4.0.0][4.0.0] (2025-05-12)
* **Breaking Change:** Drop support for .NET Framework 4.5 whose support ended on January 12th, 2016. Replaced by support for .NET Framework 4.6.2.
* **Breaking Change:** Change the static `Cell Formula(string formula)` method signature to `Cell Formula(Formula formula, string format = BuiltInCellFormat.General)`. This is a binary breaking change but is still source compatible thanks to the implicit conversion operator from `string` to the new `Formula` type.
* Add a new `Formula` type for formula cells that can contains both the formula and its pre-computed value. The pre-computed value is important for applications beside Excel that can read xlsx files. This is, for example, the value which is used by Quick Look on macOS. If the pre-computed value is not supplied, then Quick Look displays an empty cell.
* Add support for C# 9.0 record types when using `Worksheet.Populate`/`Worksheet.FromData`. The `EqualityContract` column is no longer generated, same as for all properties decorated with the `[CompilerGenerated]` attribute.
* Add two new properties on the `Workbook` class: `CreationDate` and `ModificationDate`, defaulting to `DateTime.UtcNow`.
* Fix a bug where ignored errors would not actually be ignored. Only the `NumberStoredAsText` ignored error was actually working. Now all ignored error (`CalculatedColumn`, `EmptyCellReference` etc.) are properly ignored.

## 3.1.0 (2022-04-30)
* Support for Standard built-in number formats, which localize properly (PR [#36](https://github.com/mstum/Simplexcel/pull/36) and [#37](https://github.com/mstum/Simplexcel/pull/37))
* Support for [AutoFilter](https://support.microsoft.com/en-us/office/use-autofilter-to-filter-your-data-7d87d63e-ebd0-424b-8106-e2ab61133d92) (PR [#35](https://github.com/mstum/Simplexcel/pull/35))
* Thanks to [0xced](https://github.com/0xced) for these!

## 3.0.2 (2020-12-25)
* Add [`SourceLink`](https://docs.microsoft.com/en-us/dotnet/standard/library-guidance/sourcelink) support

## 3.0.1 (2019-11-20)
* Fix `TypeInitializationException` in SimplexcelVersion in some contexts (e.g., UWP, Xamarin) ([Issue #30](https://github.com/mstum/Simplexcel/issues/30))

## 3.0.0 (2019-11-08)
* Remove targeting netstandard1.3, add targeting for netstandard2.1
* The library is now signed and strongly named for improved compatibility
  * The AssemblyVersion is `3.0.0.0` and will not change in the future
  * The AssemblyFileVersion and AssemblyInformationalVersion will contain the actual version number
  * The actual signing key is checked in as [`simplexcel_oss.snk`](src/simplexcel_oss.snk), and a dump of the public key and token is in [`simplexcel_oss.txt`](src/simplexcel_oss.txt)
  * There is a static `SimplexcelVersion` class with some helpers:
    * PublicKeyToken is the public key token for the assembly (e.g., 65e777c740a5d92a)
    * PublicKey is the full public key token for the assembly (e.g., 0024000004800000940000000602000000240000525341310...)
    * VersionString is the AssemblyInformationalVersion as a string, which may include a suffix if it's a development version (e.g., 2.3.0.177-v3-dev)
    * Version is the AssemblyFileVersion as a Version object, which does include any suffix (e.g., 2.3.0.177)
* No functional changes, just making sure that this is independent of future changes

## 2.3.0 (2019-11-02)
* Add `Worksheet.FreezeTopLeft` method (by @bcopeland in PR #26) to freeze more than just the top row/left column
* Support for Formulas, for example: `sheet.Cells["B4"] = Cell.Formula("MEDIAN(A:A)");`. See [the test app](https://github.com/mstum/Simplexcel/blob/0e22dddfcb26b9672ba3ccab6d229da7535127e7/src/Simplexcel.TestApp/Program.cs#L167) for some examples

## 2.2.1 (2018-09-19)
* Fixed bug where Background Color wasn't correctly applied to a Fill. ([Issue 23](https://github.com/mstum/Simplexcel/issues/23))

## 2.2.0 (2018-02-24)
* Add `IgnoredErrors` to a `Cell`, to disable Excel warnings (like "Number stored as text").
* If `LargeNumberHandlingMode.StoreAsText` is set on a sheet, the "Number stored as Text" warning is automatically disabled for that cell.
* Add `Cell.Fill` property, which allows setting the Fill of the cell, including the background color, pattern type (diagonal, crosshatch, grid, etc.) and pattern color
* Add `netstandard2.0` version, on top of the existing `netstandard1.3` and `net45` versions.

## 2.1.0 (2017-09-25)
* **Functional Change:** Numbers with more than 11 digits are forced as Text by Default, because [of a limitation in Excel](https://support.microsoft.com/en-us/help/2643223/long-numbers-are-displayed-incorrectly-in-excel). To restore the previous functionality, you can set `Worksheet.LargeNumberHandlingMode` to `LargeNumberHandlingMode.None`. You can also use `Cell.IsLargeNumber` to check if a given number would be affected by this.
* **Functional Change:** `Worksheet.Populate`/`Worksheet.FromData` now also reads properties from base classes.
* `Worksheet.Populate`/`Worksheet.FromData` accept a new argument, `cacheTypeColumns` which defaults to false. If set to true, then Simplexcel will cache the Reflection-based lookup of object properties. This is useful for if you have a few types that you create sheets from a lot.
* You can add `[XlsxColumn]` to a Property so that `Worksheet.Populate`/`Worksheet.FromData` can set the column name and a given column order. *Caveat:* If you set `ColumnIndex` on some, but not all Properties, the properties without a `ColumnIndex` will be on the right of the last assigned column, even if that means gaps. I recommend that you either set `ColumnIndex` on all properties or none.
* You can add `[XlsxIgnoreColumn]` to a Property so that `Worksheet.Populate`/`Worksheet.FromData` ignores it.
* Added `Cell.HorizontalAlignment` and `Cell.VerticalAlignment` to allow setting the alignment of a cell (left/center/right/justify, top/middle/bottom/justify).
* Added XmlDoc to Nuget package, so you should get Intellisense with proper comments now.

## 2.0.5 (2017-09-23)
* Add support for manual page breaks. Call `Worksheet.InsertManualPageBreakAfterRow` or `Worksheet.InsertManualPageBreakAfterColumn` with either the zero-based index of the row/column after which to create the break, or with a cell address (e.g., B5) to create the break below or to the left of that cell.

## 2.0.4 (2017-09-17)
* Support for [freezing panes](https://support.office.com/en-us/article/Freeze-panes-to-lock-rows-and-columns-dab2ffc9-020d-4026-8121-67dd25f2508f). Right now, this is being kept simple: call either `Worksheet.FreezeTopRow` or `Worksheet.FreezeLeftColumn` to freeze either the first row (1) or the leftmost column (A).
* If a Stream is not seekable (e.g., HttpContext.Response.OutputStream), Simplexcel automatically creates a temporary MemoryStream as an intermediate.
* Add `Cell.FromObject` to make Cell creation easier by guessing the correct type.
* Support `DateTime` cells, thanks to @mguinness and PR #16.

## 2.0.3 (2017-09-08)
* Add `Worksheet.Populate<T>` method to fill a sheet with data. Caveats: Does not loot at inherited members, doesn't look at complex types.
* Also add static `Worksheet.FromData<T>` method to create and populate the sheet in one.

## 2.0.2 (2017-06-17)
* Add additional validation when saving to a Stream. The stream must be seekable (and of course writeable), otherwise an Exception is thrown.

## 2.0.1 (2017-05-18)
* Fix [Issue #12](https://github.com/mstum/Simplexcel/issues/12): Sanitizing Regex stripped out too many characters (like the Ampersand or Emojis). Note that certain Unicode characters only work on newer versions of Excel (e.g., Emojis work in Excel 2013 but not 2007 or 2010).

## 2.0.0 (2017-04-22)
* Re-target to .net Framework 4.5 and .NET Standard 1.3.
* No longer use `System.Drawing.Color` but new type `Simplexcel.Color` should work.
* Classes no longer use Data Contract serializer, hence no more `[DataContract]`, `[DataMember]`, etc. attributes.
* Remove `CompressionLevel` - the entire creation of the actual .xlsx file is re-done (no more dependency on `System.IO.Packaging`) and compression is now a simple bool.

## 1.0.5 (2014-01-30)
* SharedStrings are sanitized to avoid XML Errors when using Escape chars (like 0x1B).

## 1.0.4 (2014-01-21)
* Workbook.Save throws an InvalidOperationException if there are no sheets.

## 1.0.3 (2013-08-20)
* Added support for external hyperlinks.
* Made Workbooks serializable using the .net DataContractSerializer.

## 1.0.2 (2013-01-10)
* Initial Public Release.

* [Unreleased]: https://github.com/0xced/Simplexcel/compare/4.0.0...HEAD
* [4.0.0]: https://github.com/0xced/Simplexcel/compare/121f7913fd8bf7fd5e51632b0e70364e8057269f...4.0.0
