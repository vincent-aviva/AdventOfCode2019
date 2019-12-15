using System;
using System.IO;
using AdventOfCode2019.Machines;

namespace AdventOfCode2019
{
    public class Day15 : IDay
    {
        private static string ReadFile()
        {
            var fileReader = new StreamReader("./Input/Day15.txt");
            return fileReader.ReadToEnd();
        }

        public bool IsImplemented => true;

        public bool IsPart1Complete => false;

        public void DoAction1()
        {
            var droid = new RepairDroid(ReadFile());
            droid.Run();
        }

        public bool IsPart2Complete => false;

        public void DoAction2()
        {
            throw new NotImplementedException();
        }
    }
}
