using System.Collections.Generic;

namespace AdventOfCode2019.Helpers
{
    public static class IntExtensions
    {
        public static int[] GetIntArray(this int num)
        {
            var listOfInts = new List<int>();
            while (num > 0)
            {
                listOfInts.Add(num % 10);
                num = num / 10;
            }

            listOfInts.Reverse();
            
            return listOfInts.ToArray();
        }
    }
}
