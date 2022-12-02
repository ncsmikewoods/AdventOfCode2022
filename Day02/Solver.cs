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
        
        private Dictionary<string, string> _loserTo = new()
        {
            { "A", "Z"},
            { "B", "X"},
            { "C", "Y"},
        };
        
        private Dictionary<string, string> _winnerTo = new()
        {
            { "A", "Y"},
            { "B", "Z"},
            { "C", "X"},
        };
        
        private Dictionary<string, string> _equals = new()
        {
            { "A", "X" },
            { "B", "Y" },
            { "C", "Z" },
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
            return _inputs.Sum(x => CalculateScorePart1(x[1], x[0]));
        }

        public int Solve2()
        {
            return _inputs.Sum(x => CalculateScorePart2(x[0], x[1]));
        }

        int CalculateScorePart1(string me, string them)
        {
            var points = _points[me] + CalculateHeadToHeadPart1(me, them);
            return points;
        }

        int CalculateHeadToHeadPart1(string me, string them)
        {
            if (me == _equals[them]) return 3;
            return _winners[me] == them ? 6 : 0;
        }
        
        int CalculateScorePart2(string them, string outcome)
        {
            var (headToHeadScore, myMove) = CalculateHeadToHeadPart2(them, outcome);
            
            var points = _points[myMove] + headToHeadScore;
            return points;
        }
        
        (int, string) CalculateHeadToHeadPart2(string them, string outcome)
        {
            if (outcome == "Y") return (3, _equals[them]);
            return outcome == "X" ? (0, _loserTo[them]) : (6, _winnerTo[them]);
        }

        void GetInputs()
        {
            var text = File.ReadAllLines("input.txt");
            Console.WriteLine($"Read {text.Length} inputs");
            _inputs = text.Select(x => x.Split(" ")).ToList();
        }
    }
}