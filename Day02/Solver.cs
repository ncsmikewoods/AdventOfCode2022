using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day02
{
    public class Solver
    {
        private List<int> _totals;
        private List<string[]> _inputs;

        private Dictionary<string, string> _winners = new()
        {
            { "X", "C" },
            { "Y", "A" },
            { "Z", "B" },
        };
        
        private Dictionary<string, string> _equals = new()
        {
            { "X", "A" },
            { "Y", "B" },
            { "Z", "C" },
        };

        private Dictionary<string, int> _points = new()
        {
            {"X", 1},
            {"Y", 2},
            {"Z", 3},
        };

        public Solver()
        {
            GetInputs();
        }

        public int Solve1()
        {
            return _inputs.Sum(x => CalculateScore(x[1], x[0]));
        }

        public int Solve2()
        {
            return 0;
        }

        int CalculateScore(string me, string them)
        {
            var points = _points[me] + CalculateHeadToHead(me, them);
            return points;
        }

        int CalculateHeadToHead(string me, string them)
        {
            if (_equals[me] == them) return 3;
            return _winners[me] == them ? 6 : 0;
        }

        void GetInputs()
        {
            var text = File.ReadAllLines("input.txt");
            Console.WriteLine($"Read {text.Length} inputs");
            _inputs = text.Select(x => x.Split(" ")).ToList();
        }
    }
}