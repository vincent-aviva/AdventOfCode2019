using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2019.Machines
{
    internal class RepairDroid : IInputControl
    {
        private readonly IntComputer _brain;
        private readonly Dictionary<Point, Tile> _tiles;
        private readonly Dictionary<int, Point> _steps;
        private Point _currentPosition;
        private Point _processedPosition;
        private int _stepCounter;

        internal RepairDroid(string input)
        {
            _brain = new IntComputer(input, true);
            _brain.SetInputControl(this);

            _tiles = new Dictionary<Point, Tile>();
            _steps = new Dictionary<int, Point>();
        }

        internal void Run()
        {
            _stepCounter = 0;
            _currentPosition = new Point(50, 50);
            _tiles.Add(_currentPosition, new Tile { StepsFromStart = _stepCounter, TileType = TileType.Normal});
            AddStep(_stepCounter, _currentPosition);

            while (_tiles.All(x => x.Value.TileType != TileType.OxygenSystem) && _brain.LastOperation != 99)
            {
                _brain.Run();
                
                ProcessStatus(_brain.LastOutput);
            }
        }

        public int GetInput()
        {
            var direction = 0;

            int stepLoop = _steps.Keys.Max(x => x);
            while (direction == 0 && stepLoop >= 0)
            {
                direction = GetPosition(_steps[stepLoop]);
                _stepCounter = stepLoop;
                stepLoop--;
            }

            return direction;
        }

        private int GetPosition(Point point)
        {
            var newPoint = new Point(point.X, point.Y - 1);
            if (!_tiles.ContainsKey(newPoint))
            {
                _processedPosition = newPoint;
                return (int)MovementCommand.North;
            }
            newPoint = new Point(point.X + 1, point.Y);
            if (!_tiles.ContainsKey(newPoint))
            {
                _processedPosition = newPoint;
                return (int)MovementCommand.East;
            }
            newPoint = new Point(point.X, point.Y + 1);
            if (!_tiles.ContainsKey(newPoint))
            {
                _processedPosition = newPoint;
                return (int)MovementCommand.South;
            }
            newPoint = new Point(point.X - 1, point.Y);
            if (!_tiles.ContainsKey(newPoint))
            {
                _processedPosition = newPoint;
                return (int)MovementCommand.West;
            }
            return 0;
        }

        private void ProcessStatus(long status)
        {
            RenderTile(_currentPosition, _tiles[_currentPosition].TileType);

            if (!_tiles.ContainsKey(_processedPosition))
            {
                switch (status)
                {
                    case 0: //Hit a wall
                        _tiles.Add(_processedPosition, new Tile { StepsFromStart = _stepCounter, TileType = TileType.Wall});
                        RenderTile(_processedPosition, _tiles[_processedPosition].TileType);
                        break;
                    case 1: //Moved one step
                        _tiles.Add(_processedPosition, new Tile { StepsFromStart = _stepCounter, TileType = TileType.Normal });
                        _currentPosition = _processedPosition;

                        AddStep(++_stepCounter, _currentPosition);
                        break;
                    case 2: //Moved one step and found oxygen system
                        _tiles.Add(_processedPosition, new Tile { StepsFromStart = _stepCounter, TileType = TileType.OxygenSystem });
                        _currentPosition = _processedPosition;

                        AddStep(++_stepCounter, _currentPosition);
                        break;
                }
            }

            RenderTile(_currentPosition, TileType.Droid);
            System.Threading.Thread.Sleep(25);
        }

        private void AddStep(int step, Point position)
        {
            if (_steps.ContainsKey(step))
            {
                _steps[step] = position;
            }
            else
            {
                _steps.Add(step, position);
            }
        }

        private void RenderTile(Point position, TileType tileType)
        {
            Console.SetCursorPosition(position.X, position.Y + 2);
            switch (tileType)
            {
                case TileType.Normal:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write(" ");
                    break;
                case TileType.Wall:
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write("■");
                    break;
                case TileType.OxygenSystem:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("x");
                    break;
                case TileType.Droid:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write("D");
                    break;
            }
        }

        internal enum MovementCommand
        {
            North = 1,
            South = 2,
            West = 3,
            East = 4
        }

        internal class Tile
        {
            public TileType TileType { get; set; }
            public int StepsFromStart { get; set; }
        }

        internal enum TileType
        {
            Wall,
            Normal,
            OxygenSystem,
            Droid
        }
    }
}
