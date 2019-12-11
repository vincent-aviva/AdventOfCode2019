using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2019.Machines;

namespace AdventOfCode2019
{
    public class Day7 : IDay
    {
        private static string ReadFile()
        {
            var fileReader = new StreamReader("./Input/Day07.txt");
            return fileReader.ReadToEnd();
        }

        public bool IsImplemented => true;

        public bool IsPart1Complete => true;

        public void DoAction1()
        {
            Console.WriteLine("Reading file");

            var content = ReadFile();

            var possibilities = new List<Amplification>();

            for (int a = 0;a < 5;a++)
            {
                for (int b = 0;b < 5;b++)
                {
                    for (int c = 0;c < 5;c++)
                    {
                        for (int d = 0;d < 5;d++)
                        {
                            for (int e = 0;e < 5;e++)
                            {
                                if (a == b || a == c || a == d || a == e ||
                                    b == c || b == d || b == e ||
                                    c == d || c == e ||
                                    d == e)
                                {
                                    break;
                                }

                                var ampA = new IntComputer(content, false);
                                ampA.SetInputValues(a, 0);
                                ampA.Run();
                                var ampB = new IntComputer(content, false);
                                ampB.SetInputValues(b, ampA.LastOutput);
                                ampB.Run();
                                var ampC = new IntComputer(content, false);
                                ampC.SetInputValues(b, ampB.LastOutput);
                                ampC.Run();
                                var ampD = new IntComputer(content, false);
                                ampD.SetInputValues(b, ampC.LastOutput);
                                ampD.Run();
                                var ampE = new IntComputer(content, false);
                                ampE.SetInputValues(b, ampD.LastOutput);
                                ampE.Run();

                                possibilities.Add(new Amplification { A = a, B = b, C = c, D = d, E = e, Output = ampE.LastOutput });
                            }
                        }
                    }
                }
            }

            var highestOutput = possibilities.First(x => x.Output == possibilities.Max(y => y.Output));
            Console.WriteLine($"Configuration {highestOutput.A}{highestOutput.B}{highestOutput.C}{highestOutput.D}{highestOutput.E} gives the highest output of {highestOutput.Output}");
        }

        public bool IsPart2Complete => true;

        public void DoAction2()
        {
            var possibilities = new List<Amplification>();

            var content = ReadFile();

            for (int a = 5; a < 10; a++)
            {
                for (int b = 5; b < 10; b++)
                {
                    for (int c = 5; c < 10; c++)
                    {
                        for (int d = 5; d < 10; d++)
                        {
                            for (int e = 5; e < 10; e++)
                            {
                                if (a == b || a == c || a == d || a == e ||
                                    b == c || b == d || b == e ||
                                    c == d || c == e ||
                                    d == e)
                                {
                                    break;
                                }

                                var amplifiers = new List<IntComputer>
                                {
                                    new IntComputer(content, true),
                                    new IntComputer(content, true),
                                    new IntComputer(content, true),
                                    new IntComputer(content, true),
                                    new IntComputer(content, true)
                                };

                                amplifiers[0].SetInputValues(a, 0);
                                amplifiers[1].SetInputValues(b);
                                amplifiers[2].SetInputValues(c);
                                amplifiers[3].SetInputValues(d);
                                amplifiers[4].SetInputValues(e);

                                var ampCounter = 0;
                                while (amplifiers[4].LastOperation != 99)
                                {
                                    if (ampCounter != 0)
                                    {
                                        amplifiers[ampCounter % 5]
                                            .SetInputValues(amplifiers[(ampCounter - 1) % 5].LastOutput);
                                    }

                                    amplifiers[ampCounter % 5].Run();

                                    ampCounter++;
                                }

                                possibilities.Add(new Amplification { A = a, B = b, C = c, D = d, E = e, Output = amplifiers[4].LastOutput });
                            }
                        }
                    }
                }
            }

            var highestOutput = possibilities.First(x => x.Output == possibilities.Max(y => y.Output));
            Console.WriteLine($"Configuration {highestOutput.A}{highestOutput.B}{highestOutput.C}{highestOutput.D}{highestOutput.E} gives the highest output of {highestOutput.Output}");
        }
    }

    internal struct Amplification
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public int D { get; set; }
        public int E { get; set; }
        public int Output { get; set; }
    }
}
