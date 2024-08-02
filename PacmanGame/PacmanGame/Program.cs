using System.IO;
using System.Threading.Tasks.Sources;
using System.Xml;
using System.Threading;


namespace PacmanGame
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.CursorVisible = false;

            char[,] map = ReadMap("map.txt");
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);

            Task.Run(() =>
            {
                while (true)
                {
                    pressedKey = Console.ReadKey();
                }
            });

            int packmanX = 1;
            int packmanY = 1;
            int score = 0;

            while (true)
            {

                Console.Clear();

                HandleInput(pressedKey, ref packmanX, ref packmanY, map, ref score);

                Console.ForegroundColor = ConsoleColor.Blue;
                DrawMap(map);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(packmanX, packmanY);
                Console.Write("@");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(32, 0);
                Console.Write($"Score: {score}");

                Thread.Sleep(100);


            }


        }

        private static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines("map.txt");

            char[,] map = new char[GetMaxLengthOfLine(file), file.Length];

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = file[y][x];
                }

            }
            return map;

        }

        private static void DrawMap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.Write("\n");
            }
        }

        private static void HandleInput(ConsoleKeyInfo pressedKey, ref int packmanX, ref int packmanY, char[,] map, ref int score)
        {
            int[] dirrection = GetDirection(pressedKey);

            int nextPacmanPositionX = packmanX + dirrection[0];
            int nextPacmanPositionY = packmanY + dirrection[1];

            char nextCell = map[nextPacmanPositionX, nextPacmanPositionY];

            if (nextCell == ' ' || nextCell == '.')
            {
                packmanX = nextPacmanPositionX;
                packmanY = nextPacmanPositionY;

                if (nextCell == '.')
                {
                    score++;
                    map[nextPacmanPositionX, nextPacmanPositionY] = ' ';
                }
            }
        }

        private static int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            int[] dirrection = { 0, 0 };

            if (pressedKey.Key == ConsoleKey.UpArrow)
            {
                dirrection[1] = -1;
            }
            else if (pressedKey.Key == ConsoleKey.DownArrow)
            {
                dirrection[1] = 1;
            }
            else if (pressedKey.Key == ConsoleKey.LeftArrow)
            {
                dirrection[0] = -1;
            }
            else if (pressedKey.Key == ConsoleKey.RightArrow)
            {
                dirrection[0] = 1;
            }

            return dirrection;
        }

        private static int GetMaxLengthOfLine(string[] lines)
        {
            int maxLength = lines[0].Length;

            foreach (var line in lines)
            {
                if (line.Length > maxLength)
                    maxLength = line.Length;
            }

            return maxLength;
        }
    }
}
