using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    /// <summary>
    /// With some help from https://github.com/Sukasa/Advent-of-Code-2019/blob/master/Day10/Day10.cs
    /// </summary>
    public class Day10 : IDay
    {
        public bool IsImplemented => true;

        public bool IsPart1Complete => true;

        public void DoAction1()
        {
            var asteroids = CreateAsteroidMap();

            var optimizedList = asteroids.Select(x => new { x.Point, AngleCount = x.OtherAsteroids.Select(y => y.Angle).Distinct().Count() }).ToList();

            var bestAsteroid = optimizedList.First(x => x.AngleCount == optimizedList.Max(y => y.AngleCount));
            Console.WriteLine($"The asteroid with the most asteroids in sight is at position {bestAsteroid.Point.X},{bestAsteroid.Point.Y} with a view count of {bestAsteroid.AngleCount}");
        }

        public bool IsPart2Complete => true;

        public void DoAction2() //517
        {
            var asteroids = CreateAsteroidMap();

            var optimizedList = asteroids.Select(x => new { Asteroid = x, AngleCount = x.OtherAsteroids.Select(y => y.Angle).Distinct().Count() }).ToList();

            var bestAsteroid = optimizedList.First(x => x.AngleCount == optimizedList.Max(y => y.AngleCount)).Asteroid;

            var targets = new Dictionary<float, List<AsteroidTarget>>();
            foreach (var angle in bestAsteroid.OtherAsteroids.Select(x => x.Angle).Distinct())
            {
                targets.Add(angle, bestAsteroid.OtherAsteroids.Where(x => x.Angle == angle).OrderBy(x => x.Distance).ToList());
            }
            
            Console.Clear();
            Console.Write(File.ReadAllText("./Input/Day10.txt"));

            Console.SetCursorPosition(bestAsteroid.Point.X, bestAsteroid.Point.Y);
            Console.Write("O");

            var keyOrder = targets.Keys.OrderBy(x => (180 - x) % 180).ToArray();
            var keyCounter = 0;

            Console.ForegroundColor = ConsoleColor.Red;

            AsteroidTarget vaporized = new AsteroidTarget();
            for (var countdown = 200; countdown > 0; countdown--)
            {
                List<AsteroidTarget> firingLine;
                do
                {
                    firingLine = targets[keyOrder[keyCounter]];
                    keyCounter = (++keyCounter) % keyOrder.Length;

                } while (firingLine.Count == 0);

                vaporized = firingLine[0];
                firingLine.RemoveAt(0);
                Console.SetCursorPosition(vaporized.Point.X, vaporized.Point.Y);
                Console.Write("#");

                System.Threading.Thread.Sleep(25);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, asteroids.Max(x => x.Point.Y) + 2);

            Console.Write($"The 200 vaporized asteroid is at position {vaporized.Point.X}.{vaporized.Point.Y} The answer is {(vaporized.Point.X * 100) + vaporized.Point.Y}");
        }

        private static List<Asteroid> CreateAsteroidMap()
        {
            var fileReader = new StreamReader("./Input/Day10.txt");

            var asteroidMap = new List<char[]>();
            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                asteroidMap.Add(line.ToCharArray());
            }

            var asteroids = new List<Asteroid>();
            for (int y = 0;y < asteroidMap.Count;y++)
            {
                for (int x = 0;x < asteroidMap[y].Length;x++)
                {
                    if (asteroidMap[y][x] == '#')
                    {
                        asteroids.Add(new Asteroid { Point = new Point(x, y), OtherAsteroids = new List<AsteroidTarget>() });
                    }
                }
            }

            foreach (var asteroid in asteroids)
            {
                foreach (var otherAsteroid in asteroids)
                {
                    if (asteroid == otherAsteroid)
                    {
                        continue;
                    }

                    var angle = (float)Math.Atan2(asteroid.Point.X - otherAsteroid.Point.X, asteroid.Point.Y - otherAsteroid.Point.Y);
                    asteroid.OtherAsteroids.Add(new AsteroidTarget {
                        Point = otherAsteroid.Point,
                        Angle = angle,
                        Distance = (float)Math.Sqrt(Math.Pow(asteroid.Point.X - otherAsteroid.Point.X, 2) + Math.Pow(asteroid.Point.Y - otherAsteroid.Point.Y, 2))
                });
                }
            }

            return asteroids;
        }
    }

    internal class Asteroid
    {
        public Point Point { get; set; }

        public List<AsteroidTarget> OtherAsteroids { get; set; }
    }

    internal class AsteroidTarget
    {
        public Point Point { get; set; }

        public float Angle { get; set; }

        public float Distance { get; set; }
    }
}
