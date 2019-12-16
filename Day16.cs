using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day16 : IDay
    {
        private static string ReadFile()
        {
            var fileReader = new StreamReader("./Input/Day16.txt");
            return fileReader.ReadToEnd();
        }

        private readonly int[] _repeatingPattern = new[] { 0, 1, 0, -1 };

        public bool IsImplemented => true;

        public bool IsPart1Complete => false;

        public void DoAction1()
        {
            //RunFFT("12345678", 0, 10);
            //return;

            RunFFT(ReadFile(), 0);
        }

        public bool IsPart2Complete => true;

        public void DoAction2()
        {
            var input = ReadFile();
            var input2 = string.Concat(Enumerable.Repeat(input, 10000));

            var offset = Convert.ToInt32(input.Substring(0, 7));

            RunFFT(input2, offset);
        }

        private void RunFFT(string input, int offset, int runCount = 100)
        {
            var output = new List<int>();
            var numbers = input.Select(x => Convert.ToInt32(x.ToString())).ToList();
            for (var run = 1;run <= runCount;run++)
            {

                Console.SetCursorPosition(0, 3);
                Console.Write($"Run {run} / {runCount}");
                output = new List<int>();

                for (var rowIndex = 0;rowIndex < numbers.Count;rowIndex++)
                {
                    Console.SetCursorPosition(0, 4);
                    Console.Write($"Row {rowIndex+1} / {numbers.Count}");

                    var rowNumber = 0;
                    for (var columnIndex = 0;columnIndex < numbers.Count;columnIndex++)
                    {
                        //Console.SetCursorPosition(0, 5);
                        //Console.Write($"Column {columnIndex+1} / {numbers.Count}");

                        //Add 1 column to skip the first number and then divide by the row numbers to get a repeating character
                        var multiplierNumber = (columnIndex + 1) / (rowIndex + 1);
                        var multiplier = _repeatingPattern[multiplierNumber % 4];

                        rowNumber += (numbers[columnIndex] * multiplier);
                    }

                    output.Add(Math.Abs(rowNumber % 10));
                }

                numbers = output;
            }

            var result = output.Skip(offset).Take(9);
            Console.SetCursorPosition(0, 8);
            Console.WriteLine($"Final run outputs {string.Join("", result)}");
        }
    }
}
