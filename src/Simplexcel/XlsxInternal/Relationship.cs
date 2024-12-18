namespace Simplexcel.XlsxInternal;

/// <summary>
/// A Relationship inside the Package
/// </summary>
internal class Relationship(RelationshipCounter counter)
{
    public string Id { get; } = "r" + counter.GetNextId();
    public required XmlFile Target { get; init; }
    public required string Type { get; init; }
}