using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day6 : IDay
    {
        public bool IsImplemented => true;

        public bool IsPart1Complete => true;

        public void DoAction1() //139597
        {
            var input = new List<string>();
            var fileReader = new StreamReader("./Input/Day06.txt");

            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                input.Add(line);
            }

            var orbits = Translate(input);

            var counter = 0;
            foreach (var orbit in orbits)
            {
                counter++;
                var parent = orbit.ParentObject;
                while (parent != "COM")
                {
                    var parentOrbit = orbits.First(x => x.Orbitingbject == parent);
                    parent = parentOrbit.ParentObject;
                    counter++;
                }
            }

            Console.WriteLine($"Total orbits = {counter}");
        }

        public bool IsPart2Complete => true;

        public void DoAction2() //286
        {
            var input = new List<string>();
            var fileReader = new StreamReader("./Input/Day06.txt");

            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                input.Add(line);
            }

            var orbits = Translate(input);

            var youStart = orbits.First(x => x.Orbitingbject == "YOU").ParentObject;
            var youPath = GetPathToCom(orbits, youStart);
            var sanStart = orbits.First(x => x.Orbitingbject == "SAN").ParentObject;
            var sanPath = GetPathToCom(orbits, sanStart);

            foreach (var key in youPath.Keys)
            {
                if (sanPath.ContainsKey(key))
                {
                    var steps = youPath[key] + sanPath[key];
                    Console.WriteLine($"Total steps needed to get to SAN is {steps}");
                    break;
                }
            }
        }

        private List<PlanetOrbit> Translate(List<string> input)
        {
            var orbits = new List<PlanetOrbit>();
            foreach (var inputValue in input)
            {
                var orbitData = inputValue.Split(')');
                orbits.Add(new PlanetOrbit { ParentObject = orbitData[0], Orbitingbject = orbitData[1], StepsToCom = 0 });
            }

            return orbits;
        }

        private Dictionary<string, int> GetPathToCom(List<PlanetOrbit> orbits, string startingPoint)
        {
            var steps = 0;
            var output = new Dictionary<string, int>();
            output.Add(startingPoint, 0);

            var parent = startingPoint;
            while (parent != "COM")
            {
                var parentOrbit = orbits.First(x => x.Orbitingbject == parent);
                parent = parentOrbit.ParentObject;

                steps++;
                output.Add(parent, steps);
            }

            return output;
        }
    }

    internal struct PlanetOrbit
    {
        public string ParentObject { get; set; }
        public string Orbitingbject { get; set; }
        public int StepsToCom { get; set; }
    }
}
