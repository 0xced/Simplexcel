namespace Simplexcel.XlsxInternal;

/// <summary>
/// A Relationship inside the Package
/// </summary>
internal class Relationship(RelationshipCounter counter)
{
    public string Id { get; set; } = "r" + counter.GetNextId();
    public XmlFile Target { get; set; }
    public string Type { get; set; }
}