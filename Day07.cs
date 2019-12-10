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

                                var ampA = new IntcodeComputer(content, false);
                                ampA.SetInputValues(a, 0);
                                ampA.Run();
                                var ampB = new IntcodeComputer(content, false);
                                ampB.SetInputValues(b, ampA._outputValues.Last());
                                ampB.Run();
                                var ampC = new IntcodeComputer(content, false);
                                ampC.SetInputValues(b, ampB._outputValues.Last());
                                ampC.Run();
                                var ampD = new IntcodeComputer(content, false);
                                ampD.SetInputValues(b, ampC._outputValues.Last());
                                ampD.Run();
                                var ampE = new IntcodeComputer(content, false);
                                ampE.SetInputValues(b, ampD._outputValues.Last());
                                ampE.Run();

                                possibilities.Add(new Amplification { A = a, B = b, C = c, D = d, E = e, Output = ampE._outputValues.Last() });
                            }
                        }
                    }
                }
            }

            var highestOutput = possibilities.First(x => x.Output == possibilities.Max(y => y.Output));
            Console.WriteLine($"Highest output {highestOutput.Output}");
        }

        public bool IsPart2Complete => false;

        public void DoAction2()
        {
            //var possibilities = new List<Amplification>();

            var content = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";

            int a = 9, b = 8, c = 7, d = 6, e = 5;
            var amplifiers = new List<IntcodeComputer>
            {
                new IntcodeComputer(content, true),
                new IntcodeComputer(content, true),
                new IntcodeComputer(content, true),
                new IntcodeComputer(content, true),
                new IntcodeComputer(content, true)
            };

            amplifiers[0].SetInputValues(a, 0);
            amplifiers[1].SetInputValues(b);
            amplifiers[2].SetInputValues(c);
            amplifiers[3].SetInputValues(d);
            amplifiers[4].SetInputValues(e);

            var ampCounter = 0;
            while (amplifiers[4].lastOperation != 99)
            {
                if (ampCounter != 0)
                {
                    amplifiers[ampCounter % 5].SetInputValues(amplifiers[(ampCounter - 1) % 5]._outputValues.Last());
                }

                Console.WriteLine($"Starting amplifier {ampCounter % 5} with inputs {string.Join(", ", amplifiers[ampCounter % 5]._inputValues.Select(x => x.ToString()))}");


                amplifiers[ampCounter % 5].Run();
                
                Console.WriteLine($"Exited with lastOperation {amplifiers[ampCounter % 5].lastOperation} and outputs {string.Join(", ", amplifiers[ampCounter % 5]._outputValues.Select(x => x.ToString()))}");
                Console.WriteLine();
                
                ampCounter++;
            }

            Console.WriteLine(amplifiers[4]._outputValues.Last()); //139629729

            //possibilities.Add(new Amplification { a = a, b = b, c = c, d = d, e = e, output = output });

            /*
            Console.WriteLine("Reading file");

            var fileReader = new StreamReader("./Input/Day07.txt");
            var content = fileReader.ReadToEnd();
            
            var arr = content.Split(new[] { ',' }, StringSplitOptions.None).Select(x => Convert.ToInt32(x))
                .ToArray();

            var possibilities = new List<Amplification>();

            for (int a = 5;a < 10;a++)
            {
                for (int b = 5;b < 10;b++)
                {
                    for (int c = 5;c < 10;c++)
                    {
                        for (int d = 5;d < 10;d++)
                        {
                            for (int e = 5;e < 10;e++)
                            {
                                if (a == b || a == c || a == d || a == e ||
                                    b == c || b == d || b == e ||
                                    c == d || c == e ||
                                    d == e)
                                {
                                    break;
                                }

                                while (intcodeComputer.lastOperation != 99)
                                {
                                    intcodeComputer.Process(arr, new List<int> {a, intcodeComputer.output});
                                    intcodeComputer.Process(arr, new List<int> {b, intcodeComputer.output});
                                    intcodeComputer.Process(arr, new List<int> {c, intcodeComputer.output});
                                    intcodeComputer.Process(arr, new List<int> {d, intcodeComputer.output});
                                    intcodeComputer.Process(arr, new List<int> {e, intcodeComputer.output});
                                }

                                possibilities.Add(new Amplification { a = a, b = b, c = c, d = d, e = e, output = intcodeComputer.output });
                            }
                        }
                    }
                }
            }
            */

            //var highestOutput = possibilities.First(x => x.output == possibilities.Max(y => y.output));
            //Console.WriteLine($"{highestOutput.a}{highestOutput.b}{highestOutput.c}{highestOutput.d}{highestOutput.e} gives output {highestOutput.output}");
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
