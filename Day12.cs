using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day12 : IDay
    {
        public bool IsImplemented => true;

        public bool IsPart1Complete => true;

        public void DoAction1()
        {
            var moons = new List<Moon> {
                new Moon(-6, -5, -8),
                new Moon(0, -3, -13),
                new Moon(-15, 10, -11),
                new Moon(-3, -8, 3)
            };

            for (var steps = 1;steps <= 1000;steps++)
            {
                foreach (var moon in moons)
                {
                    moon.UpdateVelocity(moons.Where(x => x != moon).ToList());
                }

                foreach (var moon in moons)
                {
                    moon.UpdatePosition();
                }
            }

            var totalEnergy = moons.Sum(x =>
                (Math.Abs(x.PositionX) + Math.Abs(x.PositionY) + Math.Abs(x.PositionZ)) *
                (Math.Abs(x.VelocityX) + Math.Abs(x.VelocityY) + Math.Abs(x.VelocityZ)));

            Console.WriteLine($"Total energy: {totalEnergy}");
        }

        public bool IsPart2Complete => true;

        public void DoAction2()
        {
            var moons = new List<Moon> {
                new Moon(-6, -5, -8),
                new Moon(0, -3, -13),
                new Moon(-15, 10, -11),
                new Moon(-3, -8, 3)
            };

            int? cycleX = null;
            int? cycleY = null;
            int? cycleZ = null;

            var counter = 0;

            while(!cycleX.HasValue || !cycleY.HasValue || !cycleZ.HasValue)
            {
                foreach (var moon in moons)
                {
                    moon.UpdateVelocity(moons.Where(x => x != moon).ToList());
                }

                foreach (var moon in moons)
                {
                    moon.UpdatePosition();
                }

                counter++;

                if (!cycleX.HasValue && moons.All(x => x.VelocityX == 0))
                {
                    cycleX = counter;
                }

                if (!cycleY.HasValue && moons.All(x => x.VelocityY == 0))
                {
                    cycleY = counter;
                }

                if (!cycleZ.HasValue && moons.All(x => x.VelocityZ == 0))
                {
                    cycleZ = counter;
                }
            }

            var cyclesNeeded = LeastCommonMultiple(cycleX.Value, LeastCommonMultiple(cycleY.Value, cycleZ.Value)) * 2;
            Console.WriteLine($"cycleX = {cycleX}, cycleY = {cycleY}, cycleZ = {cycleZ}");
            Console.WriteLine($"Total cycles needed = {cyclesNeeded}");
        }

        //Taken from https://github.com/IgneSapien/AdventOfCode2019/blob/master/Day12/Program.cs
        static double GreatestCommonDivisor(double a, double b)
        {
            if (a % b == 0)
                return b;
            return GreatestCommonDivisor(b, a % b);
        }

        //Taken from https://github.com/IgneSapien/AdventOfCode2019/blob/master/Day12/Program.cs
        static double LeastCommonMultiple(double a, double b)
        {
            return a * b / GreatestCommonDivisor(a, b);
        }
    }

    internal class Moon
    {
        public int PositionX;
        public int PositionY;
        public int PositionZ;

        public int VelocityX;
        public int VelocityY;
        public int VelocityZ;

        public Moon(int x, int y, int z)
        {
            PositionX = x;
            PositionY = y;
            PositionZ = z;
        }

        internal void UpdateVelocity(List<Moon> otherMoons)
        {
            var gravityX = otherMoons.Count(x => PositionX < x.PositionX) * 1 + otherMoons.Count(x => PositionX > x.PositionX) * -1;
            VelocityX += gravityX;

            var gravityY = otherMoons.Count(x => PositionY < x.PositionY) * 1 + otherMoons.Count(x => PositionY > x.PositionY) * -1;
            VelocityY += gravityY;

            var gravityZ = otherMoons.Count(x => PositionZ < x.PositionZ) * 1 + otherMoons.Count(x => PositionZ > x.PositionZ) * -1;
            VelocityZ += gravityZ;
        }

        public void UpdatePosition()
        {
            PositionX += VelocityX;
            PositionY += VelocityY;
            PositionZ += VelocityZ;
        }
    }
}
