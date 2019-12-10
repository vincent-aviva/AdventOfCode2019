using System;
using System.Collections.Generic;

namespace AdventOfCode2019
{
    class Program
    {
        internal static List<IDay> days => new List<IDay> { new Day1(), new Day2(), new Day3(), new Day4(), new Day5(), new Day6(), new Day7(), new Day8(), new Day9(), new Day10(), new Day11(), new Day12(), new Day13(), new Day14(), new Day15(), new Day16(), new Day17(), new Day18(), new Day19(), new Day20(), new Day21(), new Day22(), new Day23(), new Day24(), new Day25() };

        static void Main(string[] args)
        {
            foreach (var day in days)
            {
                if (day.IsImplemented && !day.IsPart1Complete)
                {
                    Console.WriteLine($"Running {day} - Part 1.");
                    day.DoAction1();
                }
                if (day.IsImplemented && !day.IsPart2Complete)
                {
                    Console.WriteLine($"Running {day} - Part 2.");
                    day.DoAction2();
                }
            }

            Console.WriteLine("");
            Console.WriteLine("A days work is done.");
            Console.ReadLine();
        }
    }
}
