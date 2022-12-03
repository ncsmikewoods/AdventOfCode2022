using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day02
{
    public class Solver
    {
        private List<int[]> _inputs;
        private readonly List<int> _key = new() { 3, 1, 2, 3, 1 };

        public Solver()
        {
            GetInputs();
        }

        public int Solve1()
        {
            return _inputs.Sum(x => CalculateScorePart1(x[0], x[1]));
        }

        public int Solve2()
        {
            return _inputs.Sum(x => CalculateScorePart2(x[0], x[1]));
        }

        int CalculateScorePart1(int them, int me)
        {
            var outcome = CalculateOutcome(them, me);
            return me + CalculateOutcomePoints(outcome);
        }
        
        int CalculateScorePart2(int them, int outcome)
        {
            var myMove = CalculateMyMove(them, outcome);

            var outcomePoints = CalculateOutcomePoints(outcome);
            return myMove + outcomePoints;
        }

        int CalculateOutcome(int them, int me)
        {
            // TODO : Ideally, refactor for no comparisons
            if (them == me) return 2;
            return _key[me - 1] == them ? 3 : 1;
        }
        
        int CalculateMyMove(int them, int outcome)
        {
            // to lose offset theirs to the right, to win offset theirs to the left
            var offset = outcome - 2;
            return _key[them + offset];
        }

        static int CalculateOutcomePoints(int outcome)
        {
            return (outcome - 1) * 3; // L = 0, D = 3, W = 6
        }
        
        void GetInputs()
        {
            var text = File.ReadAllLines("input.txt");
            Console.WriteLine($"Read {text.Length} inputs");
            _inputs = text.Select(ParseLine).ToList();
        }

        static int[] ParseLine(string line)
        {
            var tokens = line.Split(" ").Select(Normalize);
            return tokens.ToArray();
        }

        static int Normalize(string s)
        {
            var number = (int)s.First();
            if (number >= 88) number -= 23; // Convert x/y/z into a/b/c
            return number - 64; // Convert a/b/c to 1/2/3
        }
    }
}