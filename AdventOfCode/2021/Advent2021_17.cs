using PInvoke;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows;

namespace AdventOfCode._2021
{
  class Advent2021_17 : IAdventProblem
  {
    public string RawInput
    {
      get;
      set;
    }

    public object Part1(string input, bool isTestData)
    {
      var lines = ParseInput(input, isTestData);

      /*string binarystring = String.Join(String.Empty,
        RawInput.Select(
          c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0'))
        );*/

      var target = lines[0].Replace("target area: ", "").Replace("y=","").Replace("x=","").Split(", ");
      var xs = target[0].Split("..");
      var ys = target[1].Split("..");

      var x1num = Convert.ToInt32(xs[0]);
      var y1num = Convert.ToInt32(ys[0]);
      var x2num = Convert.ToInt32(xs[1]);
      var y2num = Convert.ToInt32(ys[1]);

      var minX = x1num < x2num ? x1num : x2num;
      var maxX = x1num > x2num ? x1num : x2num;
      var minY = y1num < y2num ? y1num : y2num;
      var maxY = y1num > y2num ? y1num : y2num;

      var bestHeight = 0;

      for( int initYVel = -500; initYVel < 500; initYVel++ )
      {
        for (int initXVel = -500; initXVel < 500; initXVel++)
        {
          var curXVel = initXVel;
          var curYVel = initYVel;

          var x = 0;
          var y = 0;

          var maxHeight = 0;
          for (int time = 0; time < 500; time++)
          {
            x += curXVel;
            y += curYVel;

            curXVel = curXVel - 1;
            if (curXVel < 0 )
            {
              curXVel = 0;
            }

            curYVel = curYVel - 1;

            maxHeight = y > maxHeight ? y : maxHeight;

            if( x >= x1num && x <= x2num && y >= y1num && y <= y2num )
            {
              bestHeight = maxHeight > bestHeight ? maxHeight : bestHeight;
              break;
            }
          }
        }
      }

      return bestHeight;
    }

    public object Part2(string input, bool isTestData)
    {
      var lines = ParseInput(input, isTestData);
      var count = 0;

      var target = lines[0].Replace("target area: ", "").Replace("y=", "").Replace("x=", "").Split(", ");
      var xs = target[0].Split("..");
      var ys = target[1].Split("..");

      var x1num = Convert.ToInt32(xs[0]);
      var y1num = Convert.ToInt32(ys[0]);
      var x2num = Convert.ToInt32(xs[1]);
      var y2num = Convert.ToInt32(ys[1]);

      var minX = x1num < x2num ? x1num : x2num;
      var maxX = x1num > x2num ? x1num : x2num;
      var minY = y1num < y2num ? y1num : y2num;
      var maxY = y1num > y2num ? y1num : y2num;

      var bestHeight = 0;

      for (int initXVel = -500; initXVel < 500; initXVel++)
      {
        for (int initYVel = -500; initYVel < 255000; initYVel++)
        {
          var curXVel = initXVel;
          var curYVel = initYVel;

          var x = 0;
          var y = 0;

          var maxHeight = 0;
          for (int time = 0; time < 500; time++)
          {
            x += curXVel;
            y += curYVel;

            curXVel = curXVel - 1;
            if (curXVel < 0)
            {
              curXVel = 0;
              if( x < x1num && x > x2num )
              {
                break;
              }
            }

            curYVel = curYVel - 1;

            maxHeight = y > maxHeight ? y : maxHeight;

            if (x >= x1num && x <= x2num && y >= y1num && y <= y2num)
            {
              //Console.WriteLine($"{initXVel},{initYVel}\t");
              count++;
              bestHeight = maxHeight > bestHeight ? maxHeight : bestHeight;
              break;
            }
          }
        }
      }

      return count;
    }

    
    public List<string> ParseInput(string input,bool isTestData= false)
    {
      RawInput = input.Trim();

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
