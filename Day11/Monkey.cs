using System;
using System.Collections.Generic;
using System.Linq;

namespace Day11;

public class Monkey
{
    public Monkey(string inputRaw)
    {
        var lines = inputRaw
            .Split($"{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries)
            .Skip(1)
            .ToList();

        Items = lines[0]
            .Replace("  Starting items: ", "")
            .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(double.Parse)
            .ToList();
        
        CalculateWorry = OperationBuilder.BuildOperation(lines[1]);
        GetThrowTarget = OperationBuilder.BuildThrowTargetSelector(lines[2], lines[3], lines[4]);
    }
    
    private List<double> Items { get; set; }

    private Func<double, double> CalculateWorry { get; }
    private Func<double, int> GetThrowTarget { get; }

    public int InspectionCount { get; private set; }
    
    public void ThrowInspectAndThrowItems(Func<double, double> worryReliefStrategy, List<Monkey> monkeys)
    {
        InspectionCount += Items.Count;
        
        while (Items.Count > 0)
        {
            Items[0] = CalculateWorry(Items[0]);
            Items[0] = worryReliefStrategy(Items[0]);

            var throwTarget = GetThrowTarget(Items.First());
            monkeys[throwTarget].Catch(Items.First());

            Items = Items.Skip(1).ToList();
        }
    }
    
    private void Catch(double item)
    {
        Items.Add(item);
    }
}