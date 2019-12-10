using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Helpers
{
    public static class IntExtensions
    {
        public static int[] GetIntArray(this int num, bool reverse)
        {
            var listOfInts = new List<int>();
            while (num > 0)
            {
                listOfInts.Add(num % 10);
                num = num / 10;
            }

            if (reverse)
            {
                listOfInts.Reverse();
            }

            return listOfInts.ToArray();
        }
    }
}
