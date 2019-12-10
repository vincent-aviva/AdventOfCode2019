using System;
using System.IO;

namespace AdventOfCode2019
{
    public class Day1 : IDay
    {
        public bool IsImplemented => true;
        public bool IsPart1Complete => true;

        public void DoAction1()
        {
            var totalFuel = 0;
            
            var fileReader = new StreamReader("./Input/Day01.txt");

            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                var fuelNeeded = CalculateFuel(int.Parse(line), false);
                totalFuel += fuelNeeded;
            }

            Console.WriteLine($"Fuel needed is {totalFuel}");
        }

        public bool IsPart2Complete => true;

        public void DoAction2()
        {
            int totalFuel = 0;
            
            var fileReader = new StreamReader("./Input/Day01.txt");

            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                var fuelNeeded = CalculateFuel(int.Parse(line), true);
                totalFuel += fuelNeeded;
            }

            Console.WriteLine($"Total fuel needed is {totalFuel}");
        }

        private int CalculateFuel(int mass, bool fuelFOrFuel)
        {
            double step1 = mass / 3.0;
            int step2 = (int)Math.Floor(step1);
            int step3 = step2 - 2;
            int step4 = step3 > 0 ? step3 : 0;
            int step5 = step4;

            if (fuelFOrFuel && step4 > 0)
            {
                step5 = step4 + CalculateFuel(step4, true);
            }

            return step5;
        }
    }
}