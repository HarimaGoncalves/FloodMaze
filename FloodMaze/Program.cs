using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloodMaze
{
    class Program
    {
        const string _maze = @"
IX   X  .
   X XX .
XX X    .
     XX .
 XXX X  .
  XX XX .
X X   X .
    X   .";

        static int[][] _moves = {
        new int[] { -1, 0 },
        new int[] { 0, -1 },
        new int[] { 0, 1 },
        new int[] { 1, 0 } };

        static int[][] GetMazeArray(string maze)
        {
            string[] lines = maze.Split(new char[] { '.', '\n', '\r' },
                StringSplitOptions.RemoveEmptyEntries);

            int[][] array = new int[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                // Create row.
                var row = new int[line.Length];
                for (int x = 0; x < line.Length; x++)
                {
                    switch (line[x].ToString())
                    {
                        case "X":
                            row[x] = -1;
                            break;
                        case "I":
                            row[x] = 1;
                            break;
                        default:
                            row[x] = 0;
                            break;
                    }
                }
                array[i] = row;
            }
            return array;
        }

        static void Display(int[][] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                var row = array[i];
                for (int x = 0; x < row.Length; x++)
                {
                    switch (row[x])
                    {
                        case -1:
                            Console.Write("|X |");
                            break;
                        case 0:
                            Console.Write("|0 |");
                            break;
                        default:
                            if (x == 0 && i == 0)
                            {
                                Console.Write("|" + "*" + " |");
                            }
                            else
                            {
                                if ((row[x] - 1).ToString().Length > 1)
                                {
                                    Console.Write("|" + (row[x] - 1) + "|");
                                }
                                else
                                {
                                    Console.Write("|" + (row[x] - 1) + " |");
                                }
                            }
                            break;
                    }
                }
                Console.WriteLine();
            }
        }

        static bool IsValidPos(int[][] array, int row, int newRow, int newColumn)
        {
            if (newRow < 0)
                return false;
            if (newColumn < 0)
                return false;
            if (newRow >= array.Length)
                return false;
            if (newColumn >= array[row].Length)
                return false;
            return true;
        }

        static void ModifyPath(int[][] array)
        {
            for (int rowIndex = 0; rowIndex < array.Length; rowIndex++)
            {
                var row = array[rowIndex];
                for (int columnIndex = 0; columnIndex < row.Length; columnIndex++)
                {
                    int value = array[rowIndex][columnIndex];
                    if (value >= 1)
                    {
                        foreach (var movePair in _moves)
                        {
                            int newRow = rowIndex + movePair[0];
                            int newColumn = columnIndex + movePair[1];
                            if (IsValidPos(array, rowIndex, newRow, newColumn))
                            {
                                int testValue = array[newRow][newColumn];
                                if (testValue == 0)
                                {
                                    array[newRow][newColumn] = array[rowIndex][columnIndex] + 1;
                                }
                                else
                                {
                                    if (testValue > (array[rowIndex][columnIndex] + 1))
                                    {
                                        array[newRow][newColumn] = array[rowIndex][columnIndex] + 1;
                                    }
                                    else
                                    {
                                        array[newRow][newColumn] = testValue;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        static void Main()
        {
            var array = GetMazeArray(_maze);
            Console.WriteLine("ORIGINAL");
            Display(array);

            for (int i = 0; i < 4; i++)
            {
                ModifyPath(array);
            }
            Console.WriteLine("AFTER");
            Display(array);
            string line = Console.ReadLine();
        }
    }
}


