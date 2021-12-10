using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2021
{
  class Advent2021_09 : IAdventProblem
  {
    public object Part1(string input, bool isTestData)
    {
      var lines = ParseInput(input);
      var riskSum = 0;

      var grid = lines.Select(s => s.ToCharArray().Select(c => c - '0').ToArray()).ToArray();
      if ( isTestData )
      {
        return 0; 
      }

      for (var y = 0; y < lines.Count(); y++)
      {
        for (var x = 0; x < lines[0].Length; x++)
        {
          if (IsLowPoint(grid, lines[0].Length, lines.Count(), x, y))
          {
            var risk = grid[y][x] - '0' + 1;
            riskSum += risk;
          }
        }
      }

      return riskSum;
    }

    public object Part2(string input, bool isTestData)
    {
      var lines = ParseInput(input);
      var grid = lines.Select(s => s.Trim().ToCharArray().Select(c => c - '0').ToArray()).ToArray();
      var riskSum = 0;

      var sizes = new List<int>();
      for (var y = 0; y < lines.Count(); y++)
      {
        for (var x = 0; x < lines[0].Trim().Length; x++)
        {
          if (IsLowPoint(grid, lines[0].Trim().Length, lines.Count(), x, y))
          {
            sizes.Add(GetRegionSize( grid, lines[0].Trim().Length, lines.Count(), x, y));
          }
        }
      }

      sizes.Sort();
      sizes.Reverse();
      var output = sizes[0] * sizes[1] * sizes[2];
      return output;

    }

    public bool IsLowPoint(int[][] grid, int maxX, int maxY, int x, int y)
    {
      if (x < 0 || x >= maxX || y < 0 || y >= maxY)
      {
        return false;
      }

      var val = grid[y][x];
      var right = 9;
      if (x + 1 < maxX)
      {
        right = grid[y][x + 1];
        if (right <= val)
        {
          return false;
        }
      }

      var left = 9;
      if (x - 1 > 0)
      {
        left = grid[y][x - 1];
        if (left <= val)
        {
          return false;
        }
      }

      var bottom = 9;
      if (y + 1 < maxY)
      {
        bottom = grid[y + 1][x];
        if (bottom <= val)
        {
          return false;
        }
      }

      var top = 9;
      if (y - 1 > 0)
      {
        top = grid[y - 1][x];
        if (top <= val)
        {
          return false;
        }
      }

      return true;
    }

    public bool IsEdgeOfBasin(int[][] grid, int maxX, int maxY, int x, int y)
    {
      if (x < 0 || x >= maxX || y < 0 || y >= maxY)
      {
        return false;
      }

      if( grid[y][x] == 9 )
      {
        return false;
      }

      return true;
    }

    public int GetRegionSize(int[][] grid, int maX, int maxY, int x, int y)
    {
      var queue = new Queue<Tuple<int, int>>();
      var size = 0;
      queue.Enqueue(Tuple.Create(x, y));

      while (queue.Any())
      {
        var point = queue.Dequeue();
        x = point.Item1;
        y = point.Item2;

        if (IsEdgeOfBasin(grid, maX, maxY, x, y))
        {
          size++;
          grid[y][x] = 9;

          queue.Enqueue(Tuple.Create(x + 1, y));
          queue.Enqueue(Tuple.Create(x - 1, y));
          queue.Enqueue(Tuple.Create(x, y + 1));
          queue.Enqueue(Tuple.Create(x, y - 1));
        }
      }

      return size;
    }

    public List<string> ParseInput(string input)
    {
      return new List<string>(input.Trim().Split("\n"));
    }
  }
}
