using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode._2021
{
  class Advent2021_22 : IAdventProblem
  {
    public string RawInput
    {
      get;
      set;
    }

    public object Part1(string input, bool isTestData)
    {
      var lines = ParseInput(input, isTestData);

      bool[,,] grid = new bool[105, 105, 105];

      var offset = 50;

      foreach (var line in lines)
      {
        var switchTo = line.StartsWith("on");
        var parts = line.Replace("on ", "").Replace("off ", "").Replace("x=", "").Replace("y=", "").Replace("z=", "").Trim().Split(",");
        var xRange = parts[0].Split("..");
        var yRange = parts[1].Split("..");
        var zRange = parts[2].Split("..");

        var xMin = Convert.ToInt32(xRange[0]);
        var xMax = Convert.ToInt32(xRange[1]);
        var yMin = Convert.ToInt32(yRange[0]);
        var yMax = Convert.ToInt32(yRange[1]);
        var zMin = Convert.ToInt32(zRange[0]);
        var zMax = Convert.ToInt32(zRange[1]);

        if (Math.Abs(xMin) > 50 || Math.Abs(xMax) > 50)
        {
          break;
        }

        for (int x = xMin; x <= xMax; x++)
        {
          for (int y = yMin; y <= yMax; y++)
          {
            for (int z = zMin; z <= zMax; z++)
            {
              grid[x + offset, y + offset, z + offset] = switchTo;
            }
          }
        }


      }

      var count = 0;
      for (int x = 0; x <= 100; x++)
      {
        for (int y = 0; y <= 100; y++)
        {
          for (int z = 0; z <= 100; z++)
          {
            if (grid[x, y, z])
            {
              count++;
            }
          }
        }
      }

      return count;
    }


    public object Part2(string input, bool isTestData)
    {
      var lines = ParseInput(input, isTestData);

      bool[,,] grid = new bool[105, 105, 105];

      var offset = 50;
      var count = 0;

      var startingCuboids = new List<Tuple<int, int, int, int, int, int, bool>>();
      var cuboids = new List<Tuple<int, int, int, int, int, int, bool>>();

      foreach (var line in lines)
      {
        var turnOn = line.StartsWith("on");
        var parts = line.Replace("on ", "").Replace("off ", "").Replace("x=", "").Replace("y=", "").Replace("z=", "").Trim().Split(",");
        var xRange = parts[0].Split("..");
        var yRange = parts[1].Split("..");
        var zRange = parts[2].Split("..");

        var xMin = Convert.ToInt32(xRange[0]);
        var xMax = Convert.ToInt32(xRange[1]);
        var yMin = Convert.ToInt32(yRange[0]);
        var yMax = Convert.ToInt32(yRange[1]);
        var zMin = Convert.ToInt32(zRange[0]);
        var zMax = Convert.ToInt32(zRange[1]);

        if (Math.Abs(xMin) > 50 || Math.Abs(xMax) > 50 ||
            Math.Abs(yMin) > 50 || Math.Abs(yMax) > 50 ||
            Math.Abs(zMin) > 50 || Math.Abs(zMax) > 50
          )
        {
          startingCuboids.Add(Tuple.Create(xMin, xMax, yMin, yMax, zMin, zMax, turnOn));
          continue;
        }

        for (int x = xMin; x <= xMax; x++)
        {
          for (int y = yMin; y <= yMax; y++)
          {
            for (int z = zMin; z <= zMax; z++)
            {
              grid[x + offset, y + offset, z + offset] = turnOn;
            }
          }
        }
      }

      return count;
    }

    public List<string> ParseInput(string input, bool isTestData)
    {
      RawInput = input.Trim();

      if (isTestData)
      {
        return new List<string>(input.Trim().Split("\r\n"));
      }
      
      return new List<string>(input.Trim().Split("\n"));
    }
  }
}
