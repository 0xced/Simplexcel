# Simplexcel

[Official take over](https://github.com/mstum/Simplexcel/issues/43#issuecomment-1227796312) of the original project by Michael Stum.

[![NuGet](https://img.shields.io/nuget/v/Simplexcel.svg?label=NuGet&logo=NuGet)](https://www.nuget.org/packages/simplexcel)

This is a simple .xlsx generator library for .NET Framework (4.6.2 and later), .NET Standard 2.0, and .NET Standard 2.1.

It does not aim to implement the entirety of the Office Open XML Workbook format and all the small and big features Excel offers.
Instead, it is meant as a way to handle common tasks that can't be handled by other workarounds (e.g., CSV Files or HTML Tables) and is fully supported under ASP.NET and ASP.NET Core (unlike, say, COM Interop which Microsoft explicitly doesn't support on a server).

# Features
* You can store numbers as numbers, so no more unwanted conversion to scientific notation on large numbers!
* You can store text that looks like a number as text, so no more truncation of leading zeroes because Excel thinks it's a number
* You can have multiple Worksheets
* You have basic formatting: Font Name/Size, Bold/Underline/Italic, Color, Border around a cell
* You can specify the size of cells
* Workbooks can be saved compressed or uncompressed (CPU Load vs. File Size)
* You can specify repeating rows and columns (from the top and left respectively), useful when printing
* Fully supported in ASP.NET, ASP.NET Core and Windows Services
* Supports both .NET Framework and .NET

# Usage
See [USAGE.md](USAGE.md) for instructions how to use.

# Release Notes
See [CHANGELOG.md](CHANGELOG.md) for the release notes.

# License
See [LICENSE.txt](LICENSE.txt) for the license (MIT).
