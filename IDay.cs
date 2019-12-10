using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    interface IDay
    {
        bool IsImplemented { get; }
        bool IsPart1Complete { get; }

        bool IsPart2Complete { get; }

        void DoAction1();

        void DoAction2();
    }
}
