using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Machines
{
    internal class IntComputer
    {
        private readonly List<long> _inputValues;
        private long _memoryPosition;
        private int _inputValuePosition;
        private readonly bool _breakOnOutput;
        private long _relativeBase;
        private IInputControl _inputControl;

        internal long[] Memory { get; }
        internal long LastOperation { get; private set; }
        internal List<long> OutputValues { get; }

        internal IntComputer(string input, bool breakOnOutput)
        {
            Memory = new long[10000];
            var inputArr = input.Split(new[] { ',' }, StringSplitOptions.None).Select(x => Convert.ToInt64(x)).ToArray();
            for (int i = 0; i < 10000; i++)
            {
                Memory[i] = inputArr.Length > i ? inputArr[i] : 0;
            }

            _breakOnOutput = breakOnOutput;

            _memoryPosition = 0;
            _inputValuePosition = 0;
            _inputValues = new List<long>();
            OutputValues = new List<long>();
        }

        internal void SetInputControl(IInputControl inputControl)
        {
            _inputControl = inputControl;
        }

        internal void SetInputValues(params long[] inputValues)
        {
            _inputValues.AddRange(inputValues);
        }

        internal void Run()
        {
            while (_memoryPosition < Memory.Length)
            {
                var operationInput = Convert.ToInt32(Memory[_memoryPosition]);

                LastOperation = operationInput % 100;
                var positionMode1 = (NumberMode)((operationInput / 100) % 10);
                var positionMode2 = (NumberMode)((operationInput / 1000) % 10);
                var positionMode3 = (NumberMode)((operationInput / 10000) % 10);

                long number1;
                long number2;
                long position;
                long value1;
                long value2;

                if (LastOperation == 1)
                {
                    #region number1 + number2 -> number3
                    number1 = Memory[_memoryPosition + 1];
                    value1 = GetValue(number1, positionMode1);
                    number2 = Memory[_memoryPosition + 2];
                    value2 = GetValue(number2, positionMode2);
                    position = Memory[_memoryPosition + 3];

                    SetValue(position, positionMode3, value1 + value2);
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

                    SetValue(position, positionMode3, value1 * value2);
                    _memoryPosition += 4;
                    #endregion
                }
                else if (LastOperation == 3)
                {
                    #region Input
                    if (_inputValues != null && _inputValues.Count > _inputValuePosition)
                    {
                        value1 = _inputValues[_inputValuePosition];
                        _inputValuePosition++;
                    }
                    else if (_inputControl != null)
                    {
                        value1 = _inputControl.GetInput();
                    }
                    else
                    {
                        Console.WriteLine("Please provide an input value");
                        var input = Console.ReadLine();
                        value1 = Convert.ToInt32(input);
                    }

                    position = Memory[_memoryPosition + 1];
                    SetValue(position, positionMode1, value1);

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

                    SetValue(position, positionMode3, (value1 < value2 ? 1 : 0));
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

                    SetValue(position, positionMode3, (value1 == value2 ? 1 : 0));
                    _memoryPosition += 4;
                    #endregion
                }
                else if (LastOperation == 9)
                {
                    #region Adjust relative base
                    number1 = Memory[_memoryPosition + 1];
                    value1 = GetValue(number1, positionMode1);

                    _relativeBase += value1;

                    _memoryPosition += 2;
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

        private long GetValue(long number, NumberMode mode)
        {
            switch (mode)
            {
                case NumberMode.Position:
                    return Memory[number];
                case NumberMode.Value:
                    return number;
                case NumberMode.Relative:
                    return Memory[number + _relativeBase];
            }

            return 0;
        }

        private void SetValue(long position, NumberMode mode, long value)
        {
            switch (mode)
            {
                case NumberMode.Position:
                    Memory[position] = value;
                    break;
                case NumberMode.Relative:
                    Memory[position + _relativeBase] = value;
                    break;
            }
        }

        internal long LastOutput => OutputValues.Last();

        internal string AllInputValues => string.Join(", ", _inputValues.Select(x => x.ToString()));

        internal string AllOutputValues => string.Join(", ", OutputValues.Select(x => x.ToString()));

        internal string FullMemory => string.Join(", ", Memory.Select(x => x.ToString()));
    }

    internal enum NumberMode
    {
        Position = 0,
        Value = 1,
        Relative = 2
    }
}
