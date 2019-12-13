using System;
using System.IO;
using AdventOfCode2019.Machines;

namespace AdventOfCode2019
{
    public class Day13 : IDay
    {
        private static string ReadFile()
        {
            var fileReader = new StreamReader("./Input/Day13.txt");
            return fileReader.ReadToEnd();
        }

        public bool IsImplemented => true;

        public bool IsPart1Complete => true;

        public void DoAction1()
        {
            var arcade = new ArcadeCabinet(ReadFile());
            arcade.Run();

            var blockCount = arcade.GetCount(TileType.Block);
            Console.WriteLine($"At the end there are {blockCount} blocks");
        }

        public bool IsPart2Complete => true;

        public void DoAction2()
        {
            var arcade = new ArcadeCabinet(ReadFile());
            arcade.InputQuarter(2);
            arcade.Run();
        }
    }
}
