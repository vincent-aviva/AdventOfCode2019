using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day3 : IDay
    {
        public bool IsImplemented => true;
        public bool IsPart1Complete => true;
        public void DoAction1()
        {
            Console.WriteLine("Reading file");
            var fileReader = new StreamReader("./Input/Day03.txt");

            var command1 = fileReader.ReadLine();
            var command2 = fileReader.ReadLine();

            var output = CalculateManhattenDistance(command1, command2);

            Console.WriteLine($"The lowest Manhatten distance is {output}");
        }

        public bool IsPart2Complete => true;
        public void DoAction2()
        {
            Console.WriteLine("Reading file");
            var fileReader = new StreamReader("./Input/Day03.txt");

            var command1 = fileReader.ReadLine();
            var command2 = fileReader.ReadLine();

            var output = CalculateMinSteps(command1, command2);

            Console.WriteLine($"The minimal steps is {output}");
        }

        private int CalculateManhattenDistance(string command1, string command2)
        {
            Console.WriteLine("Calculating path 1");
            var path1 = CalculatePath(command1);
            Console.WriteLine("Calculating path 2");
            var path2 = CalculatePath(command2);

            Console.WriteLine("Finding intersection");
            var intersections = new List<int>();
            foreach (var point1 in path1)
            {
                var point2 = path2.FirstOrDefault(a => point1.X == a.X && point1.Y == a.Y);
                if (point2 != null)
                {
                    var distance = Math.Abs(point1.X) + Math.Abs(point1.Y);
                    intersections.Add(distance);
                }
            }

            return intersections.Min(x => x);
        }

        private int CalculateMinSteps(string command1, string command2)
        {
            Console.WriteLine("Calculating path 1");
            var path1 = CalculatePath(command1);
            Console.WriteLine("Calculating path 2");
            var path2 = CalculatePath(command2);

            Console.WriteLine("Finding intersection");
            var intersections = new List<int>();
            foreach (var point1 in path1)
            {
                var point2 = path2.FirstOrDefault(a => point1.X == a.X && point1.Y == a.Y);
                if (point2 != null)
                {
                    var steps = point1.Steps + point2.Steps;
                    intersections.Add(steps);
                }
            }

            return intersections.Min(x => x);
        }

        private List<Point> CalculatePath(string command)
        {
            var paths = new List<Point>();
            var instructions = command.Split(new[] { ',' }, StringSplitOptions.None);

            int x = 0, y = 0;
            int counter = 0;
            foreach (var instruction in instructions)
            {
                var direction = instruction[0];
                var length = int.Parse(instruction.Substring(1));

                for (int a = 0;a < length;a++)
                {
                    counter++;
                    switch (direction)
                    {
                        case 'R':
                            x -= 1;
                            break;
                        case 'L':
                            x += 1;
                            break;
                        case 'U':
                            y += 1;
                            break;
                        case 'D':
                            y -= 1;
                            break;
                    }
                    paths.Add(new Point { X = x, Y = y, Steps = counter });
                }
            }

            return paths;
        }
    }

    internal class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Steps { get; set; }
    }
}
