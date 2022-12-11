using System;

namespace Day11;

public static class OperationBuilder
{
    // TODO : Refactor.  It's very likely the if/thens can be generalized
    public static Func<double, double> BuildOperation(string line)
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
                double Operation(double x)
                {
                    return x + x;
                }

                return Operation;
            }
            else
            {
                double Operation(double x)
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
                double Operation(double x)
                {
                    return x + operand2Num;
                }

                return Operation;
            }
            else
            {
                double Operation(double x)
                {
                    return x * operand2Num;
                }

                return Operation;
            }
        }
    }
    
    public static Func<double, int> BuildThrowTargetSelector(string testLine, string trueLine, string falseLine)
    {
        var divisor = int.Parse(testLine.Replace("  Test: divisible by ", ""));
        var trueTarget = int.Parse(trueLine.Replace("    If true: throw to monkey ", ""));
        var falseTarget = int.Parse(falseLine.Replace("    If false: throw to monkey ", ""));
        
        int ThrowTargetSelector(double x)
        {
            return x % divisor == 0 ? trueTarget : falseTarget;
        }

        return ThrowTargetSelector;
    }

    static Func<int, int, int> BuildMultiplication(int operand1, int operand2)
    {
        int TheThing(int x, int y)
        {
            return x * y;
        }

        return TheThing;
    }
    
    static Func<int, int, int> BuildAddition(int operand1, int operand2)
    {
        int TheThing(int x, int y)
        {
            return x + y;
        }

        return TheThing;
    }
}