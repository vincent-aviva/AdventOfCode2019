using System;
using System.Drawing;
using System.IO;
using AdventOfCode2019.Machines;

namespace AdventOfCode2019
{
    public class Day11 : IDay
    {
        private static string ReadFile()
        {
            var fileReader = new StreamReader("./Input/Day11.txt");
            return fileReader.ReadToEnd();
        }


        public bool IsImplemented => true;

        public bool IsPart1Complete => true;

        public void DoAction1()
        {
            var paintingMachine = new EmergencyHullPaintingRobot(ReadFile());
            paintingMachine.Run();
        }

        public bool IsPart2Complete => true;

        public void DoAction2()
        {
            var paintingMachine = new EmergencyHullPaintingRobot(ReadFile());
            paintingMachine.AddPaintedPanel(new Point(0, 0), ConsoleColor.White);
            paintingMachine.Run();

            paintingMachine.Render();
        }
    }
}
