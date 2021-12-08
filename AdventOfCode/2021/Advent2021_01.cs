using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
  class Advent2021_01 : IAdventProblem
  {
    public string RawInput
    {
      get;
      set;
    }

    public object Part1(string input, bool isTestData)
    {
      var hor = 0;
      var depth = 0;

      var ints = ParseInput(input).Select(s => Convert.ToInt32(s)).ToList();
      int previous = Int32.MaxValue;
      int count = 0;

      foreach (var value in ints)
      {
        if (value > previous )
        {
          count++;
        }
        previous = value;
      }

      return count;
    }

    public object Part2(string input, bool isTestData)
    {
      var ints = ParseInput(input).Select(s => Convert.ToInt32(s)).ToList();
      int previous = Int32.MaxValue;
      int count = 0;

      for ( int i = 3; i < ints.Count; i++ )
      {
        var total = ints[i] + ints[i-1] + ints[i-2];
        if (total > previous)
        {
          count++;
        }

        previous = total;
      }

      return count;
    }

    public List<string> ParseInput(string input)
    {
      RawInput = input;
      return new List<string>(input.Trim().Split("\n"));
    }
  }
}
