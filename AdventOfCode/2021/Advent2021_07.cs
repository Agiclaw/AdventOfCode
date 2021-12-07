using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2021
{
  class Advent2021_07 : IAdventProblem
  {
    public object Part1(string input)
    {
      var lines = ParseInput(input);

      long minDistance = Int64.MaxValue;

      for (int i = 0; i < lines.Count; i++)
      {
        var distance = lines.Select( x => Math.Abs(x-i)).Sum();
        minDistance = distance < minDistance ? distance : minDistance;
      }

      return minDistance;
    }

    public object Part2(string input)
    {
      var lines = ParseInput(input);

      long minDistance = Int64.MaxValue;

      for (int i = 0; i < lines.Count; i++)
      {
        var distance = lines.Select(x => (Math.Abs(x - i) * (Math.Abs(x - i) + 1)) / 2).Sum();
        minDistance = distance < minDistance ? distance : minDistance;
      }

      return minDistance;
    }

      public List<long> ParseInput(string input)
    {
      return new List<long>(input.Trim().Split(",").Select(s => Convert.ToInt64(s)).ToList());
    }
  }
}
