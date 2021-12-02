using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
  class Advent2021_02 : IAdventProblem
  {
    public string RawInput
    {
      get;
      set;
    }

    public object Part1(string input)
    {
      var lines = ParseInput(input);

      var x = 0;
      var y = 0;

      for ( int i = 0; i < lines.Count; i++)
      {
        var contents = lines[i].Split(" ");
        var value = Convert.ToInt32(contents[1]);

        switch (contents[0])
        {
          case "forward":
            x += value;
            break;

          case "back":
            x -= value;
            break;

          case "down":
            y += value;
            break;

          case "up":
            y -= value;
            break;
        }
      }

      return x * y;
    }

    public object Part2(string input)
    {
      var lines = ParseInput(input);

      var x = 0;
      var y = 0;
      var aim = 0;

      for (int i = 0; i < lines.Count; i++)
      {
        var contents = lines[i].Split(" ");
        var value = Convert.ToInt32(contents[1]);

        switch (contents[0])
        {
          case "forward":
            x += value;
            y += aim * value;
            break;

          case "back":
            x -= value;
            break;

          case "down":
            aim += value;
            break;

          case "up":
            aim -= value;
            break;
        }
      }

      return x * y;
    }

    public List<string> ParseInput(string input)
    {
      RawInput = input;
      return new List<string>(input.Trim().Split("\n"));
    }
  }
}
