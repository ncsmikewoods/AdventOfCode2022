using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day03
{
    public class Solver
    {
        private List<List<string>> _inputs;
        private List<string[]> _inputGroups;

        public Solver()
        {
            GetInputs();
        }

        public int Solve1()
        {
            var commons = "";

            foreach (var input in _inputs)
            {
                var sharedGift = input.First().Intersect(input.Last()).First();
                commons += sharedGift;
            }

            return commons.Sum(CalculatePriority);
        }

        public int Solve2()
        {
            var badges = "";

            foreach (var group in _inputGroups)
            {
                var badge =
                    group[0]
                        .Intersect(group[1])
                        .Intersect(group[2])
                        .First();
                badges += badge;
            }

            return badges.Sum(CalculatePriority);
        }

        int CalculatePriority(char c)
        {
            var number = (int)c;

            return number switch
            {
                >= 97 and <= 122 => number - 96, // lowercase so 1-26
                _ => number - 38 // uppercase  so 27-52 
            };
        }

        void GetInputs()
        {
            var text = File.ReadAllLines("input.txt");
            Console.WriteLine($"Read {text.Length} inputs");
            
            _inputs = text.Select(ParseLine).ToList();
            _inputGroups = text.Chunk(3).ToList();
        }

        List<string> ParseLine(string line)
        {
            var half = line.Length / 2;
            return new List<string>
            {
                new(line.Take(half).ToArray()),
                new(line.Skip(half).Take(half).ToArray())
            };
        }
    }
}