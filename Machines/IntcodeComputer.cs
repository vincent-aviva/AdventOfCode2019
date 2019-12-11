using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019.Helpers;

namespace AdventOfCode2019.Machines
{
    internal class IntComputer
    {
        private readonly List<int> _inputValues;
        private int _memoryPosition;
        private int inputValuePosition;
        private readonly bool _breakOnOutput;

        internal int[] Memory { get; }
        internal int LastOperation { get; private set; }
        internal List<int> OutputValues { get; }

        internal IntComputer(string input, bool breakOnOutput)
        {
            Memory = input.Split(new[] { ',' }, StringSplitOptions.None).Select(x => Convert.ToInt32(x)).ToArray();
            _breakOnOutput = breakOnOutput;

            _memoryPosition = 0;
            inputValuePosition = 0;
            _inputValues = new List<int>();
            OutputValues = new List<int>();
        }

        internal void SetInputValues(params int[] inputValues)
        {
            _inputValues.AddRange(inputValues);
        }

        internal void Run()
        {
            while (_memoryPosition < Memory.Length)
            {
                var operationInput = Memory[_memoryPosition];

                var operationDigits = operationInput.GetIntArray(false);
                LastOperation = operationDigits.Length == 1 ? operationDigits[0] : operationDigits[1] * 10 + operationDigits[0];
                var positionMode1 = operationDigits.Length > 2 ? operationDigits[2] : 0;
                var positionMode2 = operationDigits.Length > 3 ? operationDigits[3] : 0;

                int number1;
                int number2;
                int position;
                int value1;
                int value2;

                if (LastOperation == 1)
                {
                    #region number1 + number2 -> number3

                    number1 = Memory[_memoryPosition + 1];
                    value1 = GetValue(number1, positionMode1);
                    number2 = Memory[_memoryPosition + 2];
                    value2 = GetValue(number2, positionMode2);
                    position = Memory[_memoryPosition + 3];

                    Memory[position] = value1 + value2;
                    _memoryPosition += 4;

                    #endregion
                }
                else if (LastOperation == 2)
                {
                    #region number1 * number2 -> number3

                    number1 = Memory[_memoryPosition + 1];
                    value1 = GetValue(number1, positionMode1);
                    number2 = Memory[_memoryPosition + 2];
                    value2 = GetValue(number2, positionMode2);
                    position = Memory[_memoryPosition + 3];

                    Memory[position] = value1 * value2;
                    _memoryPosition += 4;

                    #endregion
                }
                else if (LastOperation == 3)
                {
                    #region Input

                    if (_inputValues != null && _inputValues.Count > inputValuePosition)
                    {
                        value1 = _inputValues[inputValuePosition];
                        inputValuePosition++;
                    }
                    else
                    {
                        Console.WriteLine("Please provide an input value");
                        value1 = Convert.ToInt32(Console.ReadLine());
                    }


                    position = Memory[_memoryPosition + 1];
                    Memory[position] = value1;

                    _memoryPosition += 2;

                    #endregion
                }
                else if (LastOperation == 4)
                {
                    #region Output
                    number1 = Memory[_memoryPosition + 1];
                    value1 = GetValue(number1, positionMode1);

                    OutputValues.Add(value1);

                    _memoryPosition += 2;
                    #endregion
                    
                    if (_breakOnOutput)
                    {
                        break;
                    }
                }
                else if (LastOperation == 5)
                {
                    #region Jump if true
                    number1 = Memory[_memoryPosition + 1];
                    value1 = GetValue(number1, positionMode1);
                    number2 = Memory[_memoryPosition + 2];
                    value2 = GetValue(number2, positionMode2);

                    if (value1 != 0)
                    {
                        _memoryPosition = value2;
                    }
                    else
                    {
                        _memoryPosition += 3;
                    }
                    #endregion
                }
                else if (LastOperation == 6)
                {
                    #region Jump is false
                    number1 = Memory[_memoryPosition + 1];
                    value1 = GetValue(number1, positionMode1);
                    number2 = Memory[_memoryPosition + 2];
                    value2 = GetValue(number2, positionMode2);

                    if (value1 == 0)
                    {
                        _memoryPosition = value2;
                    }
                    else
                    {
                        _memoryPosition += 3;
                    }
                    #endregion
                }
                else if (LastOperation == 7)
                {
                    #region Less then
                    number1 = Memory[_memoryPosition + 1];
                    value1 = GetValue(number1, positionMode1);
                    number2 = Memory[_memoryPosition + 2];
                    value2 = GetValue(number2, positionMode2);
                    position = Memory[_memoryPosition + 3];

                    Memory[position] = (value1 < value2 ? 1 : 0);
                    _memoryPosition += 4;
                    #endregion
                }
                else if (LastOperation == 8)
                {
                    #region Equals
                    number1 = Memory[_memoryPosition + 1];
                    value1 = GetValue(number1, positionMode1);
                    number2 = Memory[_memoryPosition + 2];
                    value2 = GetValue(number2, positionMode2);
                    position = Memory[_memoryPosition + 3];

                    Memory[position] = (value1 == value2 ? 1 : 0);
                    _memoryPosition += 4;
                    #endregion
                }
                else if (LastOperation == 99)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"ERROR, wrong operation detected. Operation={LastOperation} counter={_memoryPosition}");
                    break;
                }
            }
        }

        private int GetValue(int number, int mode)
        {
            switch (mode)
            {
                case 0: //Number is position in Array
                    return Memory[number];
                case 1: //Number is value
                    return number;
            }

            return 0;
        }

        internal int LastOutput => OutputValues.Last();

        internal string AllInputValues => string.Join(", ", _inputValues.Select(x => x.ToString()));

        internal string AllOutputValues => string.Join(", ", OutputValues.Select(x => x.ToString()));

        internal string FullMemory => string.Join(", ", Memory.Select(x => x.ToString()));
    }
}
