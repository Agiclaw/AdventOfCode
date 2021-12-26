using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode._2021
{
  class Advent2021_14 : IAdventProblem
  {
    public string RawInput
    {
      get;
      set;
    }

    public object Part1(string input, bool isTestData)
    {
      var lines = ParseInput(input, isTestData);


      //var lookup = new Lookup<>
      for (int i = 0; i < lines.Count; i++)
      {
      }

      return 0;
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
