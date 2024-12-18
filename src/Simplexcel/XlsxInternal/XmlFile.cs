using System.Xml.Linq;

namespace Simplexcel.XlsxInternal;

/// <summary>
/// An XML File in the package
/// </summary>
internal class XmlFile
{
    /// <summary>
    /// The path to the file within the package, without leading /
    /// </summary>
    internal required string Path { get; init; }

    /// <summary>
    /// The Content Type of the file (default: application/xml)
    /// </summary>
    internal string ContentType { get; init; } = "application/xml";

    /// <summary>
    /// The actual file content
    /// </summary>
    internal required XDocument Content { get; init; }
}