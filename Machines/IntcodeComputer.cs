using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019.Helpers;

namespace AdventOfCode2019.Machines
{
    internal class IntcodeComputer
    {
        internal int[] _memory;
        internal List<int> _outputValues;
        internal List<int> _inputValues;

        private int _counter;
        private bool _runInFeedback;

        internal IntcodeComputer(string input, bool runInFeedback)
        {
            _memory = input.Split(new[] { ',' }, StringSplitOptions.None).Select(x => Convert.ToInt32(x)).ToArray();
            _counter = 0;

            _inputValues = new List<int>();
            _outputValues = new List<int>();
            _runInFeedback = runInFeedback;
        }

        internal void SetInputValues(params int[] inputValues)
        {
            _inputValues.AddRange(inputValues);
        }

        internal int lastOperation = 0;

        internal void Run()
        {
            var inputValueQuestion = 0;

            while (_counter < _memory.Length)
            {
                var operationInput = _memory[_counter];

                if (operationInput == 99)
                {
                    lastOperation = 99;
                    if (_runInFeedback)
                    {
                        _counter = 0;
                    }

                    break;
                }

                var digits = operationInput.GetIntArray(false);
                lastOperation = digits.Length == 1 ? digits[0] : digits[1] * 10 + digits[0];
                var positionMode1 = digits.Length > 2 ? digits[2] : 0;
                var positionMode2 = digits.Length > 3 ? digits[3] : 0;

                int number1;
                int number2;
                int position;
                int value1;
                int value2;

                if (lastOperation == 1)
                {
                    #region number1 + number2 -> number3

                    number1 = _memory[_counter + 1];
                    value1 = GetValue(number1, positionMode1);
                    number2 = _memory[_counter + 2];
                    value2 = GetValue(number2, positionMode2);
                    position = _memory[_counter + 3];

                    _memory[position] = value1 + value2;
                    _counter += 4;

                    #endregion
                }
                else if (lastOperation == 2)
                {
                    #region number1 * number2 -> number3

                    number1 = _memory[_counter + 1];
                    value1 = GetValue(number1, positionMode1);
                    number2 = _memory[_counter + 2];
                    value2 = GetValue(number2, positionMode2);
                    position = _memory[_counter + 3];

                    _memory[position] = value1 * value2;
                    _counter += 4;

                    #endregion
                }
                else if (lastOperation == 3)
                {
                    #region Input

                    if (_inputValues != null && _inputValues.Count() > inputValueQuestion)
                    {
                        value1 = _inputValues[inputValueQuestion];
                        inputValueQuestion++;
                    }
                    else
                    {
                        Console.WriteLine("Please provide an input value");
                        value1 = Convert.ToInt32(Console.ReadLine());
                    }


                    position = _memory[_counter + 1];
                    _memory[position] = value1;

                    _counter += 2;

                    #endregion
                }
                else if (lastOperation == 4)
                {
                    #region Output
                    number1 = _memory[_counter + 1];
                    value1 = GetValue(number1, positionMode1);

                    SendOutput(value1);

                    _counter += 2;
                    #endregion


                    if (_runInFeedback)
                    {
                        break;
                    }
                }
                else if (lastOperation == 5)
                {
                    #region Jump if true
                    number1 = _memory[_counter + 1];
                    value1 = GetValue(number1, positionMode1);
                    number2 = _memory[_counter + 2];
                    value2 = GetValue(number2, positionMode2);

                    if (value1 != 0)
                    {
                        _counter = value2;
                    }
                    else
                    {
                        _counter += 3;
                    }
                    #endregion
                }
                else if (lastOperation == 6)
                {
                    #region Jump is false
                    number1 = _memory[_counter + 1];
                    value1 = GetValue(number1, positionMode1);
                    number2 = _memory[_counter + 2];
                    value2 = GetValue(number2, positionMode2);

                    if (value1 == 0)
                    {
                        _counter = value2;
                    }
                    else
                    {
                        _counter += 3;
                    }
                    #endregion
                }
                else if (lastOperation == 7)
                {
                    #region Less then
                    number1 = _memory[_counter + 1];
                    value1 = GetValue(number1, positionMode1);
                    number2 = _memory[_counter + 2];
                    value2 = GetValue(number2, positionMode2);
                    position = _memory[_counter + 3];

                    _memory[position] = (value1 < value2 ? 1 : 0);
                    _counter += 4;
                    #endregion
                }
                else if (lastOperation == 8)
                {
                    #region Equals
                    number1 = _memory[_counter + 1];
                    value1 = GetValue(number1, positionMode1);
                    number2 = _memory[_counter + 2];
                    value2 = GetValue(number2, positionMode2);
                    position = _memory[_counter + 3];

                    _memory[position] = (value1 == value2 ? 1 : 0);
                    _counter += 4;
                    #endregion
                }
                else
                {
                    Console.WriteLine($"ERROR, wrong operation detected. Operation={lastOperation} counter={_counter}");
                    break;
                }
            }
        }

        internal int GetValue(int number, int mode)
        {
            switch (mode)
            {
                case 0: //Number is position in Array
                    return _memory[number];
                case 1: //Number is value
                    return number;
            }

            return 0;
        }

        internal void SendOutput(int value)
        {
            _outputValues.Add(value);
        }

        internal void ReceiveInput(int value)
        {
            SetInputValues(value);
            this.Run();
        }
    }
}
