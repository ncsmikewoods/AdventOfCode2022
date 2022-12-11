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
        
        var operand1Raw = tokens[0];
        var operand2Raw = tokens[2];
        var operatorRaw = tokens[1];
        
        // see if operand2 is old
        // if so, return a closure around an existing func that performs the correct operation with the supplied value
        
        var operandIsNumber = int.TryParse(operand2Raw, out var operand2Num);

        if (!operandIsNumber)
        {
            if (operatorRaw == "+")
            {
                int TheThing(int x)
                {
                    return x + x;
                }

                return TheThing;
            }
            else
            {
                int TheThing(int x)
                {
                    return x * x;
                }

                return TheThing;
            }
        }
        else
        {
            if (operatorRaw == "+")
            {
                int TheThing(int x)
                {
                    return x + operand2Num;
                }

                return TheThing;
            }
            else
            {
                int TheThing(int x)
                {
                    return x * operand2Num;
                }

                return TheThing;
            }
        }
    }
    
    public static Func<int, int> BuildThrowTarget(string testLine, string trueLine, string falseLine)
    {
        var divisor = int.Parse(testLine.Replace("  Test: divisible by ", ""));
        var trueTarget = int.Parse(trueLine.Replace("    If true: throw to monkey ", ""));
        var falseTarget = int.Parse(falseLine.Replace("    If false: throw to monkey ", ""));
        
        int TheThing(int x)
        {
            return x % divisor == 0 ? trueTarget : falseTarget;
        }

        return TheThing;
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