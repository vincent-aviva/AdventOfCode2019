using AdventOfCode2019.Machines;
using System;
using System.IO;

namespace AdventOfCode2019
{
    public class Day2 : IDay
    {
        private static string ReadFile()
        {
            Console.WriteLine("Reading file");
            var fileReader = new StreamReader("./Input/Day02.txt");
            return fileReader.ReadToEnd();
        }

        public bool IsImplemented => true;
        public bool IsPart1Complete => true;
        public void DoAction1()
        {
            var content = ReadFile();
            
            var intComputer = new IntComputer(content, false);
            intComputer.Memory[1] = 12;
            intComputer.Memory[2] = 2;
            intComputer.Run();

            Console.WriteLine($"Output {intComputer.Memory[0]}");
        }

        public bool IsPart2Complete => true;
        public void DoAction2()
        {
            var content = ReadFile();

            var loopLength = 100;

            for (var a = 0;a < loopLength;a++)
            {
                for (var b = 0;b < loopLength;b++)
                {
                    var intComputer = new IntComputer(content, false);
                    intComputer.Memory[1] = a;
                    intComputer.Memory[2] = b;
                    intComputer.Run();

                    if (intComputer.Memory[0] == 19690720)
                    {
                        Console.WriteLine($"Output {intComputer.Memory[0]} for Noun {a} and Verb {b}");
                        a = loopLength;
                        b = loopLength;
                    }
                }
            }
        }
    }
}
