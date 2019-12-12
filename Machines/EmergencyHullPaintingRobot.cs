using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2019.Machines
{
    internal class EmergencyHullPaintingRobot
    {
        private readonly IntComputer _brain;
        private Point _currentPosition;
        private MachineDirection _currentDirection;
        private readonly Dictionary<Point, ConsoleColor> _paintedPanels;

        internal EmergencyHullPaintingRobot(string input)
        {
            _brain = new IntComputer(input, true);
            _currentPosition = new Point(0, 0);
            _currentDirection = MachineDirection.Up;
            _paintedPanels = new Dictionary<Point, ConsoleColor>();
        }

        internal void AddPaintedPanel(Point point, ConsoleColor color)
        {
            _paintedPanels.Add(point, color);
        }

        internal void Run()
        {
            _brain.SetInputValues(GetColor());

            while (_brain.LastOperation != 99)
            {
                _brain.Run();
                SetColor(_brain.LastOutput);

                _brain.Run();
                Move(_brain.LastOutput);

                _brain.SetInputValues(GetColor());
            }
        }

        internal long GetColor()
        {
            var panelColor = ConsoleColor.Black;
            if (_paintedPanels.ContainsKey(_currentPosition))
            {
                panelColor = _paintedPanels[_currentPosition];
            }

            return panelColor == ConsoleColor.White ? 1 : 0;
        }

        private void SetColor(long color)
        {
            var panelColor = color == 0 ? ConsoleColor.Black : ConsoleColor.White;
            if (_paintedPanels.ContainsKey(_currentPosition))
            {
                _paintedPanels[_currentPosition] = panelColor;
            }
            else
            {
                _paintedPanels.Add(_currentPosition, panelColor);
            }
        }

        private void Move(long direction)
        {
            if (direction == 0) //Left
            {
                switch (_currentDirection)
                {
                    case MachineDirection.Up:
                        _currentDirection = MachineDirection.Left;
                        break;
                    case MachineDirection.Right:
                        _currentDirection = MachineDirection.Up;
                        break;
                    case MachineDirection.Down:
                        _currentDirection = MachineDirection.Right;
                        break;
                    case MachineDirection.Left:
                        _currentDirection = MachineDirection.Down;
                        break;
                }
            }
            else if (direction == 1) //Right
            {
                switch (_currentDirection)
                {
                    case MachineDirection.Up:
                        _currentDirection = MachineDirection.Right;
                        break;
                    case MachineDirection.Right:
                        _currentDirection = MachineDirection.Down;
                        break;
                    case MachineDirection.Down:
                        _currentDirection = MachineDirection.Left;
                        break;
                    case MachineDirection.Left:
                        _currentDirection = MachineDirection.Up;
                        break;
                }
            }

            switch (_currentDirection)
            {
                case MachineDirection.Up:
                    _currentPosition = new Point(_currentPosition.X, _currentPosition.Y -1);
                    break;
                case MachineDirection.Right:
                    _currentPosition = new Point(_currentPosition.X + 1, _currentPosition.Y);
                    break;
                case MachineDirection.Down:
                    _currentPosition = new Point(_currentPosition.X, _currentPosition.Y + 1);
                    break;
                case MachineDirection.Left:
                    _currentPosition = new Point(_currentPosition.X - 1, _currentPosition.Y);
                    break;
            }
        }

        internal void Render()
        {
            var offSetX = _paintedPanels.Min(x => x.Key.X) * -1;
            var offSetY = _paintedPanels.Min(x => x.Key.Y) * -1 + 2;

            Console.Clear();
            Console.WriteLine("The registration identifier is:");

            foreach (var paintedPanel in _paintedPanels)
            {
                Console.SetCursorPosition(paintedPanel.Key.X + offSetX, paintedPanel.Key.Y + offSetY);
                Console.ForegroundColor = paintedPanel.Value;
                Console.Write("■");

                System.Threading.Thread.Sleep(25);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 8);
        }
    }

    public enum MachineDirection
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }
}
