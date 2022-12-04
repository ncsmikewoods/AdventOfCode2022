using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day04
{
    public class Solver
    {
        private List<List<Range>> _inputPairs;

        public Solver()
        {
            GetInputs();
        }

        public int Solve1()
        {
            return _inputPairs
                .Count(x => 
                    x[0].FullyContains(x[1]) 
                    || x[1].FullyContains(x[0]));
        }

        public int Solve2()
        {
            return 0;
        }

        void GetInputs()
        {
            var text = File.ReadAllLines("input.txt");
            Console.WriteLine($"Read {text.Length} inputs");
            
            _inputPairs = text.Select(ParseLine).ToList();
        }

        List<Range> ParseLine(string line)
        {
            var tokens = line.Split(',', '-');

            return new List<Range>
            {
                new Range(int.Parse(tokens[0]), int.Parse(tokens[1])),
                new Range(int.Parse(tokens[2]), int.Parse(tokens[3]))
            };
        }
    }

    public class Range
    {
        public int Start { get; }
        public int End { get; }

        public Range(int start, int end)
        {
            Start = start;
            End = end;
        }

        public bool FullyContains(Range other)
        {
            return Start <= other.Start && End >= other.End;
        }
    }
}