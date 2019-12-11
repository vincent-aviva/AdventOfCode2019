using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day8 : IDay
    {
        private static string ReadFile()
        {
            var fileReader = new StreamReader("./Input/Day08.txt");
            return fileReader.ReadToEnd();
        }

        public bool IsImplemented => true;

        public bool IsPart1Complete => true;

        public void DoAction1()
        {
            var content = ReadFile();
            var image = SpaceImage.Create(content, 6, 25);

            var minimumZerosLayer = image.Layers.First(x => x.DigitCount('0') == image.Layers.Min(y => y.DigitCount('0')));
            var count1 = minimumZerosLayer.DigitCount('1');
            var count2 = minimumZerosLayer.DigitCount('2');
            var output = count1 * count2;

            Console.WriteLine($"Checking space image isn't corrupted. The output is {output}");
        }

        public bool IsPart2Complete => true;

        public void DoAction2()
        {
            var content = ReadFile();
            var image = SpaceImage.Create(content, 6, 25);
            image.Render();
        }
    }

    internal class SpaceImage
    {
        public List<SpaceImageLayer> Layers { get; set; }
        public int PixelsTall { get; set; }
        public int PixelsWide { get; set; }

        private SpaceImage(int pixelsTall, int pixelsWide)
        {
            Layers = new List<SpaceImageLayer>();
            PixelsWide = pixelsWide;
            PixelsTall = pixelsTall;
        }

        internal static SpaceImage Create(string content, int pixelsTall, int pixelsWide)
        {
            var contentPosition = 0;
            var image = new SpaceImage(pixelsTall, pixelsWide);
            var currentLayer = new SpaceImageLayer();

            while (contentPosition + image.PixelsWide <= content.Length)
            {
                if (contentPosition % (image.PixelsWide * image.PixelsTall) == 0)
                {
                    currentLayer = new SpaceImageLayer();
                    image.Layers.Add(currentLayer);
                }

                var rowContent = content.Substring(contentPosition, image.PixelsWide);
                currentLayer.Rows.Add(rowContent.ToCharArray());

                contentPosition += image.PixelsWide;
            }

            return image;
        }

        internal void Render()
        {
            for (int rownumber = 0; rownumber < PixelsTall; rownumber++)
            {
                for (int columnNumber = 0; columnNumber < PixelsWide; columnNumber++)
                {
                    var color = GetColor(rownumber, columnNumber);
                    Console.ForegroundColor = color;
                    Console.Write("■");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 0 = Zwart
        /// 1 = Wit
        /// 2 = Transparant
        /// </summary>
        /// <param name="rownumber"></param>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        private ConsoleColor GetColor(int rownumber, int columnNumber)
        {
            var colorNumber = '2';
            foreach (var layer in Layers)
            {
                colorNumber = layer.Rows[rownumber][columnNumber];
                if (colorNumber != '2')
                {
                    break;
                }
            }

            switch (colorNumber)
            {
                case '0':
                    return ConsoleColor.Blue;
                case '1':
                    return ConsoleColor.Yellow;
                default:
                    return ConsoleColor.Black;
            }
        }
    }

    internal class SpaceImageLayer
    {
        public List<char[]> Rows { get; set; }

        internal SpaceImageLayer()
        {
            Rows = new List<char[]>();
        }

        internal int DigitCount(char digit) => Rows.Sum(y => y.Count(z => z.Equals(digit)));
    }
}
