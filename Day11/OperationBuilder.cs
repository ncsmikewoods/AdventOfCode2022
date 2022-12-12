using System;

namespace Day11;

public static class OperationBuilder
{
    // TODO : Refactor.  It's very likely the if/thens can be generalized
    public static Func<int, int> BuildOperation(string line)
    {
        var tokens = line
            .Replace("  Operation: new = ", "")
            .Split(' ');
        
        var operand2Raw = tokens[2];
        var operatorRaw = tokens[1];
        
        var operandIsNumber = int.TryParse(operand2Raw, out var operand2Num);

        if (!operandIsNumber)
        {
            if (operatorRaw == "+")
            {
                int Operation(int x)
                {
                    return x + x;
                }

                return Operation;
            }
            else
            {
                int Operation(int x)
                {
                    return x * x;
                }

                return Operation;
            }
        }
        else
        {
            if (operatorRaw == "+")
            {
                int Operation(int x)
                {
                    return x + operand2Num;
                }

                return Operation;
            }
            else
            {
                int Operation(int x)
                {
                    return x * operand2Num;
                }

                return Operation;
            }
        }
    }
    
    public static Func<int, int> BuildThrowTargetSelector(string testLine, string trueLine, string falseLine)
    {
        var divisor = int.Parse(testLine.Replace("  Test: divisible by ", ""));
        var trueTarget = int.Parse(trueLine.Replace("    If true: throw to monkey ", ""));
        var falseTarget = int.Parse(falseLine.Replace("    If false: throw to monkey ", ""));
        
        int ThrowTargetSelector(int x)
        {
            return x % divisor == 0 ? trueTarget : falseTarget;
        }

        return ThrowTargetSelector;
    }
}