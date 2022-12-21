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
}