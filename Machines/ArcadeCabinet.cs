using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;

namespace AdventOfCode2019.Machines
{
    internal class ArcadeCabinet : IInputControl
    {
        private readonly IntComputer _brain;
        private readonly Dictionary<Point, TileType> _tiles;
        private Point previousBallPosition;

        internal ArcadeCabinet(string input)
        {
            _brain = new IntComputer(input, true);
            _brain.SetInputControl(this);
            _tiles = new Dictionary<Point, TileType>();
        }

        internal void InputQuarter(long quarters)
        {
            _brain.Memory[0] = quarters;
        }

        internal void Run()
        {
            Console.Clear();

            long x, y, output;
            while (_brain.LastOperation != 99)
            {
                _brain.Run();
                x = _brain.LastOutput;

                _brain.Run();
                y = _brain.LastOutput;

                _brain.Run();
                output = _brain.LastOutput;

                if (_brain.LastOperation != 99)
                {
                    if (x == -1 && y == 0)
                    {
                        SetScore(output);
                    }
                    else
                    {
                        CreateTile(x, y, output);
                    }
                }

                System.Threading.Thread.Sleep(1);
            }

            Console.SetCursorPosition(0, _tiles.Keys.Max(key => key.Y) + 4);
        }

        private void CreateTile(long x, long y, long tileId)
        {
            var position = new Point(Convert.ToInt32(x), Convert.ToInt32(y));
            if (_tiles.ContainsKey(position))
            {
                _tiles[position] = (TileType)tileId;
            }
            else
            {
                _tiles.Add(position, (TileType)tileId);
            }

            Console.SetCursorPosition(position.X, position.Y +2);
            switch ((TileType)tileId)
            {
                case TileType.Empty:
                    Console.Write(" ");
                    break;
                case TileType.Wall:
                    Console.Write("■");
                    break;
                case TileType.Block:
                    Console.Write("o");
                    break;
                case TileType.HorizontalPaddle:
                    Console.Write("-");
                    break;
                case TileType.Ball:
                    Console.Write(".");
                    break;
            }
        }

        private void SetScore(long score)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Your score: {score}");
        }

        internal int GetCount(TileType tileType)
        {
            return _tiles.Values.Count(x => x == tileType);
        }

        public int GetInput()
        {
            Point ballPosition = new Point(-1, -1), paddlePosition = new Point(-1, -1);
            foreach (var tile in _tiles)
            {
                if (tile.Value == TileType.Ball)
                {
                    ballPosition = tile.Key;
                }
                else if (tile.Value == TileType.HorizontalPaddle)
                {
                    paddlePosition = tile.Key;
                }

                if (ballPosition.X != -1 && ballPosition.Y != -1 && paddlePosition.X != -1 && paddlePosition.Y != -1)
                {
                    break;
                }
            }

            if (ballPosition.X > paddlePosition.X)
            {
                return 1;
            }
            else if (ballPosition.X < paddlePosition.X)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    internal enum TileType
    {
        Empty = 0,
        Wall = 1,
        Block = 2,
        HorizontalPaddle = 3,
        Ball = 4
    }
}
