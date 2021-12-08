using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
  class Advent2021_05 : IAdventProblem
  {
    public object Part1(string input, bool isTestData)
    {
      var lines = ParseInput(input);

      var grid = new int[1000,1000];

      for (int i = 0; i < lines.Count; i++)
      {
        var line = lines[i].Trim().Split(" -> ");

        var x1 = Convert.ToInt32(line[0].Split(",")[0]);
        var y1 = Convert.ToInt32(line[0].Split(",")[1]);

        var x2 = Convert.ToInt32(line[1].Split(",")[0]);
        var y2 = Convert.ToInt32(line[1].Split(",")[1]);

        // straight lines
        if (x1 == x2)
        {
          var low = y1 < y2 ? y1 : y2;
          var high = y1 > y2 ? y1 : y2;

          for (int y = low; y <= high; y++)
          {
            grid[x1, y] = grid[x1, y] + 1;
          }
        }

        if (y1 == y2)
        {
          var low = x1 < x2 ? x1 : x2;
          var high = x1 > x2 ? x1 : x2;

          for (int x = low; x <= high; x++)
          {
            grid[x, y1] = grid[x, y1] + 1;
          }
        }
      }

      var count = 0;
      for (int x = 0; x < 1000; x++)
      {
        for (int y = 0; y < 1000; y++)
        {
          if (grid[x,y] > 1)
          {
            count++;
          }
        }
      }

      return count;
    }

    public object Part2(string input, bool isTestData)
    {
      var lines = ParseInput(input);

      var grid = new int[1000, 1000];

      for (int i = 0; i < lines.Count; i++)
      {
        var line = lines[i].Trim().Split(" -> ");

        var x1 = Convert.ToInt32(line[0].Split(",")[0]);
        var y1 = Convert.ToInt32(line[0].Split(",")[1]);

        var x2 = Convert.ToInt32(line[1].Split(",")[0]);
        var y2 = Convert.ToInt32(line[1].Split(",")[1]);

        grid[x1, y1] = grid[x1, y1] + 1;

        while (!(x1 == x2 && y1 == y2))
        {
          if ( x1 != x2 )
          {
            x1 = x1 <= x2 ? x1 + 1 : x1 - 1;
          }

          if (y1 != y2)
          {
            y1 = y1 < y2 ? y1 + 1 : y1 - 1;
          }

          grid[x1, y1] = grid[x1, y1] + 1;
        }
      }

      var count = 0;
      for (int y = 0; y < 1000; y++)
      {
        for (int x = 0; x < 1000; x++)
        {
          if (grid[x, y] > 1)
          {
            count++;
          }
        }
      }

      return count;
    }

    public List<string> ParseInput(string input)
    {
      return new List<string>(input.Trim().Split("\n"));
    }
  }
}
