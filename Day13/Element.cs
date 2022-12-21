using System.Collections.Generic;
using System.Linq;

namespace Day13;

public abstract class Element
{
    public List<Element> Elements { get; set; }
    
    public int Value { get; set; }

    public bool IsList => Elements != default;
}

public class ElementList : Element 
{
    public ElementList()
    {
        Elements = new List<Element>();
    }

    public ElementList(IEnumerable<Element> elements)
    {
        Elements = elements.ToList();
    }

    // public List<Element> Elements { get; set; } = new();
}

public class ElementValue : Element
{
    // public int Value { get; }

    public ElementValue(int value)
    {
        Value = value;
    }
}