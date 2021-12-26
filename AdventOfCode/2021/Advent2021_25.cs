using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode._2021
{
  class Advent2021_25 : IAdventProblem
  {
    public string RawInput
    {
      get;
      set;
    }

    public object Part1(string input, bool isTestData)
    {
      var lines = ParseInput(input, isTestData);

      var xMax = lines[0].Trim().Length;
      var yMax = lines.Count;

      var grid = new char[xMax,yMax];

      for( int y = 0; y < yMax; y++ )
      {
        for (int x = 0; x < xMax; x++)
        {
          grid[x, y] = lines[y][x];
        }
      }

      var totalSteps = 0;
      var steps = 0;
      var rounds = 0;

      Print(grid, xMax, yMax);

      do
      {
        steps = 0;
        rounds++;

        // Iterate the grid
        for (int y = yMax-1; y >= 0; y--)
        {
          for (int x = xMax-1; x >= 0; x--)
          {
            if ( grid[x,y] == '.')
            {
              var tempX = x == 0 ? xMax-1 : x;
              if(grid[tempX, y] == '>')
              {
                grid[tempX, y] = '.';
                grid[x, y] = '>';
                steps++;
                totalSteps++;
              }
            }
          }
        }

        // Iterate the grid
        for (int y = yMax - 1; y >= 0; y--)
        {
          for (int x = xMax - 1; x >= 0; x--)
          {
            if (grid[x, y] == '.')
            {
              var tempY = y == 0 ? yMax-1 : y;
              if (grid[x, tempY] == 'v')
              {
                grid[x, tempY] = '.';
                grid[x, y] = 'v';
                steps++;
                totalSteps++;
              }
            }
          }
        }

        Print(grid, xMax, yMax);
      } while (steps > 0);

      return rounds;
    }

    public object Part2(string input, bool isTestData)
    {
      var lines = ParseInput(input, isTestData);

      return 0;
    }

    public List<string> ParseInput(string input, bool isTestData)
    {
      RawInput = input.Trim();

      if (isTestData)
      {
        return new List<string>(input.Trim().Split("\r\n"));
      }
      
      return new List<string>(input.Trim().Split("\n"));
    }

    public int Print(char[,] grid, int maxX, int maxY)
    {
      int count = 0;
      var sb = new StringBuilder();
      for (var y = 0; y < maxY; y++)
      {
        for (var x = 0; x < maxX; x++)
        {
          sb.Append(grid[x,y]);
        }
        sb.Append("\n");
      }

      Console.WriteLine(sb.ToString());

      return count;
    }
  }
}
