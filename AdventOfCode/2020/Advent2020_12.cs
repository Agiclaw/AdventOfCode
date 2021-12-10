using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
  class Advent2020_12 : IAdventProblem
  {
    public object Part1(string input, bool isTestData)
    {
      var lines = ParseInput(input);

      int x = 0;
      int y = 0;

      var heading = 'E';

      for (int i = 0; i < lines.Count; i++)
      {
        var command = lines[i].AsSpan(0, 1)[0];
        //var length = Convert.ToInt32(lines[i].AsSpan(1, lines[i].Length-1));

        switch( command )
        {
          case 'N':
            break;

          case 'S':
            break;

          case 'E':
            break;

          case 'W':
            break;

        }
      }

      return 0;
    }

    public object Part2(string input, bool isTestData)
    {
      var lines = ParseInput(input);

      for (int i = 0; i < lines.Count; i++)
      {
      
      }

      return 0;
    }

      public List<string> ParseInput(string input)
    {
      return new List<string>(input.Trim().Split("\n"));
    }
  }
}
