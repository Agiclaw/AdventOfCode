using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode._2021
{
  class Advent2021_11 : IAdventProblem
  {
    public string RawInput
    {
      get;
      set;
    }

    public int Flashes
    {
      get;
      set;
    }

    public object Part1(string input, bool isTestData)
    {
      if(!isTestData)
      {
        return 0;
      }

      var lines = ParseInput(input);

      var grid = lines.Select(s => s.Trim().ToCharArray().Select(c => c - '0').ToArray()).ToArray();

      int generations = 100000;

      Console.WriteLine($"Before Any steps");
      Print(grid, lines[0].Length, lines.Count());
      for (var i = 0; i < 100000; i++)
      {
        var queue = new Queue<Tuple<int,int>>();

        for (var y = 0; y < lines.Count(); y++)
        {
          for (var x = 0; x < lines[0].Length; x++)
          {
            
          }
        }

        while( queue.Count != 0)
        {
          Flashes++;

          var p = queue.Dequeue();
          grid[p.Item2][p.Item1] = -1;

          modifyNeighbors(grid, queue, p.Item1, p.Item2, lines[0].Length, lines.Count());
        }

        // Cleanup
        for (var y = 0; y < lines.Count(); y++)
        {
          for (var x = 0; x < lines[0].Length; x++)
          {
            if (grid[y][x] == -1)
            {
              grid[y][x] = 0;
            }
          }
        }

        var allGood = true;
        for (var y = 0; y < lines.Count(); y++)
        {
          for (var x = 0; x < lines[0].Length; x++)
          {
            if (grid[y][x] != 0)
            {
              allGood = false;
              break;
            }
          }
        }

        if ( allGood )
        {
          return i;
        }
        Console.WriteLine( $"After Step {i+1}: {Flashes} Octopi flashes observed thus far.");
        Print(grid, lines[0].Length, lines.Count());
      }

      return Flashes;
    }

    public void modifyNeighbors( int[][] grid, Queue<Tuple<int,int>> queue, int x, int y, int maxX, int maxY )
    {
      for (int yMod = -1; yMod <= 1; yMod++)
      {
        for (int xMod = -1; xMod <= 1; xMod++)
        {
          var newX = x + xMod;
          var newY = y + yMod;
          if (newX >= 0 && newX < maxX && newY >= 0 && newY < maxY)
          {
            if(grid[newY][newX] != -1)
            {
              grid[newY][newX] = grid[newY][newX] + 1;

              if(grid[newY][newX] == 10)
              {
                grid[newY][newX] = -1;
                queue.Enqueue(new Tuple<int, int>(newX, newY));
              }
            }
          }
        }
      }
    }

    public void Print(int[][] grid, int maxX, int maxY)
    {
      var sb = new StringBuilder();
      for (var y = 0; y < maxY; y++)
      {
        for (var x = 0; x < maxX; x++)
        {
          sb.Append(grid[y][x]);
        }
        sb.Append("\n");
      }

      Console.WriteLine(sb.ToString());
    }

    public object Part2(string input, bool isTestData)
    {
      var lines = ParseInput(input);

      
      return 0;
    }

    public List<string> ParseInput(string input)
    {
      RawInput = input;
      return new List<string>(input.Trim().Split("\r\n"));
    }
  }
}
