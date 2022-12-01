using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day01
{
    public class Solver
    {
        private List<int> _totals;

        public Solver()
        {
            GetInputs();
        }

        public int Solve1()
        {
            return _totals.Max();
        }

        public int Solve2()
        {
            return _totals
                .OrderByDescending(x => x)
                .Take(3)
                .Sum();
        }


        void GetInputs()
        {
            var text = File.ReadAllText("input.txt");

            var people = text.Split($"{Environment.NewLine}{Environment.NewLine}");

            _totals = people.Select(x => 
                    x.Split(Environment.NewLine)
                        .Sum(int.Parse))
                .ToList();
            
            Console.WriteLine($"Read {_totals.Count} inputs");
        }
    }
}