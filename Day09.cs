using System;
using System.IO;
using AdventOfCode2019.Machines;

namespace AdventOfCode2019
{
    public class Day9 : IDay
    {
        private static string ReadFile()
        {
            var fileReader = new StreamReader("./Input/Day09.txt");
            return fileReader.ReadToEnd();
        }

        public bool IsImplemented => true;

        public bool IsPart1Complete => true;

        public void DoAction1()
        {
            var content = ReadFile();
            var computer = new IntComputer(content, false);
            computer.SetInputValues(1);
            computer.Run();
            Console.WriteLine($"Last output = {computer.LastOutput}");
        }

        public bool IsPart2Complete => true;

        public void DoAction2()
        {
            var content = ReadFile();
            var computer = new IntComputer(content, false);
            computer.SetInputValues(2);
            computer.Run();
            Console.WriteLine($"Last output = {computer.LastOutput}");
        }
    }
}
