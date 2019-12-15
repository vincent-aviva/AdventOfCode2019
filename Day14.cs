using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day14 : IDay
    {
        private static List<ChemicalReaction> ReadFile()
        {
            var reactions = new List<ChemicalReaction>();

            var fileReader = new StreamReader("./Input/Day14.txt");
            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                reactions.Add(ChemicalReaction.Parse(line));
            }

            return reactions;
        }


        public bool IsImplemented => true;

        public bool IsPart1Complete => true;

        public void DoAction1()
        {
            var refinery = new Refinery(ReadFile());
            refinery.Calculate("FUEL", 1);

            Console.WriteLine($"Total ORE needed is {refinery.OreNeeded}");
        }

        public bool IsPart2Complete => true;

        public void DoAction2()
        {
            var totalOre = 1000000000000;

            var refinery = new Refinery(ReadFile());

            var counter = 7659700;
            while (true)
            {
                refinery.Calculate("FUEL", counter);

                Console.WriteLine($"For {counter} FUEL you need {refinery.OreNeeded}");

                if (refinery.OreNeeded > totalOre)
                {
                    break;
                }

                counter++;
            }

            Console.WriteLine();
            Console.WriteLine($"The maximum number of fuel with 1 trillion ore is {counter-1}");
        }
    }

    internal class Refinery
    {
        private readonly List<ChemicalReaction> _reactions;
        internal long OreNeeded { get; private set; }

        internal Refinery(List<ChemicalReaction> reactions)
        {
            _reactions = reactions;
        }

        internal void Calculate(string name, int amount)
        {
            OreNeeded = 0;

            var componentsNeeded = new Dictionary<string, long> { { name, amount } };
            while (componentsNeeded.Any(component => component.Key != "ORE" && component.Value > 0))
            {
                var fill = componentsNeeded.First(component => component.Key != "ORE" && component.Value > 0);
                var reaction = _reactions.First(x => x.OutputComponent.Name == fill.Key);
                var multiplier = Convert.ToInt64(Math.Ceiling(componentsNeeded[fill.Key] / (reaction.OutputComponent.Amount * 1.0)));

                componentsNeeded[fill.Key] -= reaction.OutputComponent.Amount * multiplier;

                foreach (var input in reaction.InputComponents)
                {
                    if (componentsNeeded.ContainsKey(input.Name))
                    {
                        componentsNeeded[input.Name] += input.Amount * multiplier;
                    }
                    else
                    {
                        componentsNeeded.Add(input.Name, input.Amount * multiplier);
                    }
                }
            }

            OreNeeded = componentsNeeded["ORE"];
        }
    }

    internal class ChemicalReaction
    {
        public List<ReactionComponent> InputComponents { get; set; }
        public ReactionComponent OutputComponent { get; set; }

        public static ChemicalReaction Parse(string reactionInput)
        {
            var returnValue = new ChemicalReaction {
                InputComponents = new List<ReactionComponent>()
            };

            var inOut = reactionInput.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
            var inputs = inOut[0].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var input in inputs)
            {
                returnValue.InputComponents.Add(ReactionComponent.Parse(input.Trim()));
            }

            returnValue.OutputComponent = ReactionComponent.Parse(inOut[1].Trim());

            return returnValue;
        }
    }

    internal struct ReactionComponent
    {
        public int Amount { get; set; }
        public string Name { get; set; }

        public static ReactionComponent Parse(string input)
        {
            var parsedArr = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return new ReactionComponent {
                Amount = int.Parse(parsedArr[0].Trim()),
                Name = parsedArr[1].Trim()
            };
        }
    }
}
