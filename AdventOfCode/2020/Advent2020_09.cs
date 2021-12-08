using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
  class Advent2020_09 : IAdventProblem
  {
    public object Part1(string input, bool isTestData)
    {
      var lines = ParseInput(input);

      long minDistance = Int64.MaxValue;

      var prevWindow = isTestData ? 5 : 25;
      for (int i = prevWindow; i < lines.Count; i++)
      {
        var previous = lines.GetRange(i- prevWindow, prevWindow);
        bool found = false;
        foreach (var p in previous)
        {
          if (previous.Contains(lines[i] - p))
          {
            found = true;
          }
        }

        if (!found)
        {
          return lines[i];
        }
      }

      return 0;
    }

    public object Part2(string input, bool isTestData)
    {
      var lines = ParseInput(input);

      long minDistance = Int64.MaxValue;

      for (int i = 25; i < lines.Count; i++)
      {
        var previous = lines.GetRange(i - 25, 25);
        bool found = false;
        foreach (var p in previous)
        {
          if (previous.Contains(lines[i] - p))
          {
            found = true;
          }
        }

        if (!found)
        {
          for (int l = 0; l < i; l++)
          {
            for (int r = 1; i + r < lines.Count; r++)
            {
              if (l != r)
              {
                var range = lines.GetRange(l, r);
                if (range.Sum() == lines[i])
                {
                  return range.Min() + range.Max();
                }
              }
            }
          }
        }
      }

      return 0;
    }

      public List<long> ParseInput(string input)
    {
      return new List<long>(input.Trim().Split("\n").Select(s => Convert.ToInt64(s)).ToList());
    }
  }
}
