using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019.Helpers;

namespace AdventOfCode2019
{
    public class Day4 : IDay
    {
        public bool IsImplemented => true;
        public bool IsPart1Complete => true;
        public void DoAction1() //2090
        {
            RunCode(130254, 678275, false);
        }

        public bool IsPart2Complete => true;
        public void DoAction2() //1419
        {
            RunCode(130254, 678275, true);
        }

        private void RunCode(int start, int stop, bool needsPart2)
        {
            var counter = 0;
            for (int i = start;i <= stop;i++)
            {
                if (MatchesRequirements(i, needsPart2))
                {
                    counter++;
                }
            }

            Console.WriteLine($"There are {counter} possibilities");
        }

        private bool MatchesRequirements(int input, bool needsPart2)
        {
            var foundDouble = false;
            var skippedDigits = new List<int>();
            var digits = input.GetIntArray(true);

            for (var i = 1 ; i < digits.Length ; i++)
            {
                if (digits[i-1] > digits[i])
                {
                    return false;
                }

                if (digits[i-1] == digits[i] && skippedDigits.All(x => x != digits[i]))
                {
                    if (needsPart2)
                    {
                        if (i < 5 && digits[i] == digits[i + 1])
                        {
                            skippedDigits.Add(digits[i]);
                        }
                        else
                        {
                            foundDouble = true;
                        }
                    }
                    else
                    {
                        foundDouble = true;
                    }
                }
            }

            return foundDouble;
        }
    }
}
