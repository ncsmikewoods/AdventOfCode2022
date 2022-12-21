using System.Collections.Generic;
using System.Linq;

namespace Day13;

public abstract class Element
{
    public List<Element> Elements { get; set; }
    
    public int Value { get; set; }

    public bool IsList => Elements != default;
    public bool IsValue => Elements == default;

    public Result IsSortedWith(Element right)
    {
        if (IsList && right.IsList)
        {
            var leftList = (ElementList)this;
            var rightList = (ElementList)right;

            return leftList.IsSortedWith(rightList);
        }

        if (IsValue && right.IsValue)
        {
            var leftVal = (ElementValue)this;
            var rightVal = (ElementValue)right;

            return leftVal.IsSortedWith(rightVal);
        }
        
        // one is a list and one is a value so turn the value into a single-element list 
        if (IsValue)
        {
            var leftList = ((ElementValue)this).ConvertToList();
            var rightList = (ElementList)right;
            
            return leftList.IsSortedWith(rightList);
        }
        else
        {
            var leftList = (ElementList)this;
            var rightList = ((ElementValue)right).ConvertToList();
            
            return leftList.IsSortedWith(rightList);            
        }
    }
}

public class ElementList : Element 
{
    public ElementList(IEnumerable<Element> elements)
    {
        Elements = elements.ToList();
    }

    public Result IsSortedWith(ElementList right)
    {
        for (var i = 0; i < Elements.Count; i++)
        {
            var rightIsOutOfElements = i > right.Elements.Count - 1;
            if (rightIsOutOfElements) return Result.Unsorted;
            
            var leftElement = Elements[i];
            var rightElement = right.Elements[i];

            var result = leftElement.IsSortedWith(rightElement);
            if (result == Result.Sorted) return Result.Sorted;
            if (result == Result.Unsorted) return Result.Unsorted;
        }

        return Result.Sorted; // Left ran out before right and there was no determination yet
    }
}

public class ElementValue : Element
{
    public ElementValue(int value)
    {
        Value = value;
    }
    
    public Result IsSortedWith(ElementValue right)
    {
        if (Value < right.Value) return Result.Sorted;
        if (Value > right.Value) return Result.Unsorted;
        
        return Result.Undetermined;
    }

    public ElementList ConvertToList()
    {
        return new ElementList(new List<Element> { this });
    }
}