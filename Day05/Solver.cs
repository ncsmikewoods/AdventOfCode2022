using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day05
{
    public class Solver
    {
        private List<Stack<char>> _stacks;
        private List<List<int>> _moves;

        public Solver()
        {
            GetInputs();
        }

        public string Solve1()
        {
            foreach (var move in _moves)
            {
                var count = move[0];
                var source = move[1];
                var destination = move[2];

                for (var i = 0; i < count; i++)
                {
                    _stacks[destination].Push(_stacks[source].Pop());
                }
            }

            return BuildOutput(_stacks);
        }

        public string Solve2()
        {
            GetInputs();
            
            foreach (var move in _moves)
            {
                var count = move[0];
                var source = move[1];
                var destination = move[2];

                var items = Enumerable.Range(1, count).Select(_ => _stacks[source].Pop());

                foreach (var item in items.Reverse())
                {
                    _stacks[destination].Push(item);
                }
            }
            
            return BuildOutput(_stacks);
        }

        string BuildOutput(List<Stack<char>> stacks)
        {
            var stackTops = stacks
                .Select(stack => 
                    stack.Count == 0 
                        ? '\0' 
                        : stack.Peek())
                .ToArray();
            return string.Join("", stackTops);
        }
        
        void GetInputs()
        {
            var text = File.ReadAllText("input.txt");

            var halves = text.Split($"{Environment.NewLine}{Environment.NewLine}");

            _stacks = ParseTopHalf(halves[0]);
            _moves = ParseBottomHalf(halves[1]);
        }

        List<Stack<char>> ParseTopHalf(string stacksRaw)
        {
            var lines = stacksRaw.Split($"{Environment.NewLine}");

            var lastLine = lines[^1];
            var lastStackNumberRaw = lastLine[^2];
            var stackCount = (int)char.GetNumericValue(lastStackNumberRaw);

            var stacks = 
                Enumerable.Range(1, stackCount)
                    .Select(_ => new Stack<char>())
                    .ToList();

            // Iterate through the stack input left to right, bottom to top
            for (var row = lines.Length - 2; row >= 0; row--)
            {
                for (var col = 0; col < stackCount; col++)
                {
                    var charIndex = 1 + (4 * col);
                    var item = lines[row][charIndex];
                    
                    if (item != ' ') stacks[col].Push(item);
                }
            }

            return stacks;
        }

        List<List<int>> ParseBottomHalf(string movesRaw)
        {
            var lines = movesRaw.Split($"{Environment.NewLine}");

            var moves = new List<List<int>>();
            
            foreach (var line in lines)
            {
                var tokens = line.Split(" ").ToList();
                var nums = new List<int>{int.Parse(tokens[1]), int.Parse(tokens[3])-1, int.Parse(tokens[5])-1};
                moves.Add(nums);
            }

            return moves;
        }
    }
}