using System;
using System.IO;
using System.Linq;
using AdventOfCode2019.Machines;

namespace AdventOfCode2019
{
    public class Day5 : IDay
    {
        public bool IsImplemented => true;

        public bool IsPart1Complete => true;

        public void DoAction1() //9961446
        {
            Console.WriteLine("Reading file");

            var fileReader = new StreamReader("./Input/Day05.txt");
            var content = fileReader.ReadToEnd();
            
            var intcodeComputer = new IntComputer(content, false);
            intcodeComputer.SetInputValues(1);
            intcodeComputer.Run();

            Console.WriteLine(intcodeComputer.OutputValues.First(x => x != 0));
        }

        public bool IsPart2Complete => true;

        public void DoAction2() //742621
        {
            Console.WriteLine("Reading file");

            var fileReader = new StreamReader("./Input/Day05.txt");
            var content = fileReader.ReadToEnd();
            
            var intcodeComputer = new IntComputer(content, false);
            intcodeComputer.SetInputValues(1);
            intcodeComputer.Run();

            Console.WriteLine(intcodeComputer.OutputValues.First(x => x != 0));
        }
    }
}
