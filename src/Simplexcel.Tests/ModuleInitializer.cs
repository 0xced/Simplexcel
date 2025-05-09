using System.Runtime.CompilerServices;

namespace Simplexcel.Tests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        VerifySylvanDataExcel.Initialize();
    }
}