using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode._2021
{
  class Advent2021_15 : IAdventProblem
  {
    public string RawInput
    {
      get;
      set;
    }
    public int BestPathRisk
    {
      get;
      set;
    } = Int32.MaxValue;

    public object Part1(string input, bool isTestData)
    {
      BestPathRisk = Int32.MaxValue;
      var lines = ParseInput(input, isTestData);
      var grid = lines.Select(s => s.Trim().ToCharArray().Select(c => c - '0').ToArray()).ToArray();

      var visited = new HashSet<int>();
      var totalPath = 0;

      Path(visited, grid, 0, 0, totalPath);
      var val = BestPathRisk - grid[0][0];
      return val;
    }

    public object Part2(string input, bool isTestData)
    {
      var lines = ParseInput(input);

      
      return 0;
    }

    public List<string> ParseInput(string input,bool isTestData= false)
    {
      RawInput = input;

      if( isTestData )
      {
        return new List<string>(input.Trim().Split("\r\n"));
      }

      return new List<string>(input.Trim().Split("\n"));
    }

    public void Path(HashSet<int> visited, int[][] grid, int x, int y, int totalPath)
    {
      var maxX = grid[0].Length;
      var maxY = grid.Length;

      if (x < 0 || x > grid.Length || y < 0 || y > grid.Length)
      {
        return;
      }

      //var position = (y * maxX) + x;
      //if( visited.Contains(position))
      //{
        //return;
      //}

      //visited.Add(position);
      totalPath += grid[y][x];

      if(BestPathRisk != Int32.MaxValue && totalPath > BestPathRisk )
      {
        return;
      }

      if( x == maxX -1 && y == maxY - 1)
      {
        BestPathRisk = totalPath < BestPathRisk ? totalPath : BestPathRisk;
        return;
      }

      if (x + 1 < maxX)
      {
        Path(visited, grid, x + 1, y, totalPath);
      }

      if (x - 1 > 0)
      {
        //Path(visited, grid, x - 1, y, totalPath);
      }

      if (y + 1 < maxY)
      {
        Path(visited, grid, x, y+1, totalPath);
      }

      if (y - 1 > 0)
      {
        //Path(visited, grid, x, y-1, totalPath);
      }

      return;
    }

    public int Print(int[,] grid, int maxX, int maxY)
    {
      int count = 0;

      var sb = new StringBuilder();
      for (var y = 0; y < maxY; y++)
      {
        for (var x = 0; x < maxX; x++)
        {
          if( grid[y, x] > 0 )
          {
            count++;
            if (maxY < 100 && maxX < 100)
            {
              sb.Append("*");
            }
          }
          else
          {
            if (maxY < 100 && maxX < 100)
            {
              sb.Append(" ");
            }
          }

        }
        sb.Append("\n");
      }

      Console.WriteLine(sb.ToString());

      return count;
    }
  }
}
