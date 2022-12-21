using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day13;

public abstract class Element
{
    public List<Element> Elements { get; set; }
    
    public int Value { get; set; }

    public bool IsList => Elements != default;
    public bool IsValue => Elements == default;

    public Result IsSortedWith(Element right)
    {
        Console.WriteLine($"- Compare {ToString()} vs {right.ToString()}");
        
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
            
            Console.WriteLine($"Mixed types; convert left to {leftList} and retry comparison");
            return leftList.IsSortedWith(rightList);
        }
        else
        {
            var leftList = (ElementList)this;
            var rightList = ((ElementValue)right).ConvertToList();
            
            Console.WriteLine($"Mixed types; convert right to {right} and retry comparison");
            return leftList.IsSortedWith(rightList);            
        }
    }

    public new abstract string ToString();
}

public class ElementList : Element 
{
    public ElementList(IEnumerable<Element> elements)
    {
        Elements = elements.ToList();
    }

    public Result IsSortedWith(ElementList right)
    {
        Console.WriteLine($"- Compare {ToString()} vs {right.ToString()}");
        
        for (var i = 0; i < Elements.Count; i++)
        {
            var rightIsOutOfElements = i > right.Elements.Count - 1;
            if (rightIsOutOfElements)
            {
                Console.WriteLine("- Right side ran out of items, so inputs are not in the right order");
                return Result.Unsorted;
            }
            
            var leftElement = Elements[i];
            var rightElement = right.Elements[i];

            var result = leftElement.IsSortedWith(rightElement);
            if (result == Result.Sorted)
            {
                Console.WriteLine("- Left side is smaller, so inputs are in the right order");
                return Result.Sorted;
            }
            if (result == Result.Unsorted)
            {
                Console.WriteLine("- Right side is smaller, so inputs are not in the right order");
                return Result.Unsorted;
            }
        }

        if (Elements.Count < right.Elements.Count)
        {
            Console.WriteLine("- Left side ran out of items, so inputs are in the right order");
            return Result.Sorted; 
        }
        
        return Result.Undetermined; // All elements in each list are identical.
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("[");
        sb.Append(string.Join(",", Elements.Select(x => x.ToString())));
        sb.Append("]");
        return sb.ToString();
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
    
    public override string ToString()
    {
        return Value.ToString();
    }
}