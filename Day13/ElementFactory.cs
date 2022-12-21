using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Day13;

public static class ElementFactory
{
    public static ElementList BuildPacket(string input)
    {
        var jarray = (JArray)JsonConvert.DeserializeObject(input);
        return BuildElementList(jarray);
    }

    static ElementList BuildElementList(JArray jarray)
    {
        var elements = new List<Element>();

        foreach (var jtoken in jarray)
        {
            var element = BuildElement(jtoken);
            elements.Add(element);
        }
        
        return new ElementList(elements);
    }
    
    static Element BuildElement(JToken jtoken)
    {
        if (jtoken.Type == JTokenType.Integer)
        {
            return new ElementValue((int)jtoken);
        }
        
        if (jtoken.Type == JTokenType.Array)
        {
            return BuildElementList(jtoken as JArray);
        }

        throw new Exception("Unexpected JToken type: " + jtoken.Type);
    }
    
    // public static ElementList BuildPacket(string input)
    // {
    //     // var fullList = GetFullList(input);
    //     return (ElementList)BuildElement(input[1..^1]);
    // }
    
    static Element BuildElement(string input)
    {
        // empty list
        if (string.IsNullOrWhiteSpace(input))
        {
            return new ElementList();
        }
        
        // single value
        if (int.TryParse(input, out var parsedValue))
        {
            return new ElementValue(parsedValue);
        }
        
        // // flat array with no subarrays
        // if (!input.Contains('['))
        // {
        //     var elements = 
        //         input
        //             .Split(",")
        //             .Select(BuildElement);
        //     return new ElementList(elements);
        // }
        
        // strip brackets from a list
        // if (input[0] == '[')
        // {
        //     var fullList = GetFullList(input);
        //     return BuildElement(fullList);
        // }

        var elementsAtThisLevel = GetElementsAtThisLevel(input);
        var elements = elementsAtThisLevel.Select(BuildElement);
        return new ElementList(elements);
    }

    static List<string> GetElementsAtThisLevel(string input) // comes in like 1,2,[3,4],[5,6]
    {
        if (string.IsNullOrWhiteSpace(input)) return new List<string>();
        
        var elements = new List<string>();
        var index = 0;

        var element = "";
        
        while (index < input.Length)
        {
            var curr = input[index];
            if (curr == ',')
            {
                if (!string.IsNullOrWhiteSpace(element))
                {
                    elements.Add(element);
                }
                
                element = "";
                index++;
                continue;
            }

            if (curr == '[')
            {
                var listRaw = "[" + GetFullList(input[index..]) + "]";
                elements.Add(listRaw);
                
                index += listRaw.Length;
                continue;
            }

            element += curr;
            index++;
        }

        // Not actually sure this is the right way to handle this
        if (!string.IsNullOrWhiteSpace(element))
        {
            elements.Add(element);
        }

        return elements;
    }
    
    static string GetFullList(string input)
    {
        var level = 1;
        var index = 1;

        while (index < input.Length && level > 0)
        {
            var current = input[index];
            if (current == '[') level++;
            if (current == ']') level--;
            index++;
        }

        return input[1..(index - 1)];
    }
}