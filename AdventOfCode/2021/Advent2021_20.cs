using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode._2021
{
  class Advent2021_20 : IAdventProblem
  {
    public string RawInput
    {
      get;
      set;
    }

    public string ImageProcessingLookup
    {
      get;
      set;
    }

    public object Part1(string input, bool isTestData)
    {
      string[] imageData = ParseInput(input, isTestData);
    
      int iterations = 2;
      var width = imageData[0].Length+2;
      var height = imageData.Length+2;
      var grid = new int[width, height];
      var offset = 1;

      for ( int y = 0; y < imageData.Length; y++ )
      {
        for( int x = 0; x < imageData[0].Length; x++ )
        {
          grid[y + offset, x + offset] = imageData[y][x] == '#' ? 1 : 0;
        }
      }

      var total = 0;

      do
      {
        grid = EnchanceImage(grid);
        //total = Analyze(grid, isTestData ? $"testPart1_{iterations}" : $"realPart1_{iterations}");
        iterations--;
      } while (iterations > 0);

      return total;
    }

    public int[,] EnchanceImage(int[,] originalGrid)
    {
      var maxX = originalGrid.GetLength(0);
      var maxY = originalGrid.GetLength(1);

      var newGrid = new int[maxX+2, maxY+2];

      for (int y = -1; y <= maxY; y++)
      {
        for (int x = -1; x <= maxX; x++)
        {
          var index = GetIndexFromNeighbors(originalGrid, maxX, maxY, x, y);
          if(ImageProcessingLookup[index] == '#')
          {
            newGrid[y+1,x+1] = 1;
          }
        }
      }

      return newGrid;
    }

    public int GetIndexFromNeighbors(int[,] grid, int maxX, int maxY, int x, int y )
    {
      var defaultValue = grid[0, 0];
      var sb = new StringBuilder();

      sb.Append(SafeLookup(grid, maxX, maxY, x - 1, y - 1, defaultValue));
      sb.Append(SafeLookup(grid, maxX, maxY, x,     y - 1, defaultValue));
      sb.Append(SafeLookup(grid, maxX, maxY, x + 1, y - 1, defaultValue));

      sb.Append(SafeLookup(grid, maxX, maxY, x - 1, y,     defaultValue));
      sb.Append(SafeLookup(grid, maxX, maxY, x,     y,     defaultValue));
      sb.Append(SafeLookup(grid, maxX, maxY, x + 1, y,     defaultValue));

      sb.Append(SafeLookup(grid, maxX, maxY, x - 1, y + 1, defaultValue));
      sb.Append(SafeLookup(grid, maxX, maxY, x,     y + 1, defaultValue));
      sb.Append(SafeLookup(grid, maxX, maxY, x + 1, y + 1, defaultValue));

      int val = Convert.ToInt32(sb.ToString(), 2);

      return val;
    }

    public int SafeLookup(int[,] originalGrid, int maxX, int maxY, int x, int y, int defaultValue)
    {
      if (x > 0 && x < maxX-1 && y > 0 && y < maxY-1)
      {
        return originalGrid[y,x];
      }

      return defaultValue;
    }

    public object Part2(string input, bool isTestData)
    {
      var imageData = ParseInput(input, isTestData);

      int iterations = 50;
      var width = imageData[0].Length + 2;
      var height = imageData.Length + 2;
      var grid = new int[width, height];
      var offset = 1;

      for (int y = 0; y < imageData.Length; y++)
      {
        for (int x = 0; x < imageData[0].Length; x++)
        {
          grid[y + offset, x + offset] = imageData[y][x] == '#' ? 1 : 0;
        }
      }

      var total = 0;

      do
      {
        grid = EnchanceImage(grid);
        //total = Analyze(grid, isTestData ? $"testPart1_{iterations}" : $"realPart1_{iterations}");
        iterations--;
      } while (iterations > 0);


      return total;
    }

    public string[] ParseInput(string input, bool isTestData)
    {
      RawInput = input.Trim();

      List<string> lines = null;

      if (isTestData)
      {
        lines = new List<string>(input.Trim().Split("\r\n\r\n"));
      }
      else
      {
        lines = new List<string>(input.Trim().Split("\n\n"));
      }

      ImageProcessingLookup = lines[0];

      string[] imageData;
      if (isTestData)
      {
        imageData = lines[1].Split("\r\n");
      }
      else
      {
        imageData = lines[1].Split("\n");
      }

      return imageData;
    }
  }
}
