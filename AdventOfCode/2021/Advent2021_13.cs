using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode._2021
{
  class Advent2021_13 : IAdventProblem
  {
    public string RawInput
    {
      get;
      set;
    }

    public object Part1(string input, bool isTestData)
    {
      var lines = ParseInput(input, isTestData);

      var grid = new int[1500,1500];

      var maxX = 1500;
      var maxY = 1500;
      bool gridParsed = false;

      for (int i = 0; i < lines.Count; i++)
      {
        if( lines[i] == string.Empty)
        {
          continue;
        }

        if( lines[i].Contains(","))
        {
          var coord = lines[i].Split(",");
          var x = Convert.ToInt32(coord[1]);
          var y = Convert.ToInt32(coord[0]);
          //maxX = x > maxX ? x : maxX;
          //maxY = y > maxY ? y : maxY;

          grid[Convert.ToInt32(coord[1]), Convert.ToInt32(coord[0])] = 1;
          continue;
        }

        if( lines[i].Contains("x="))
        {
          if(!gridParsed)
          {
            gridParsed = true;
            Console.WriteLine($"Original");
            //Print(grid, 15, 15);
          }
          var val = Convert.ToInt32(lines[i].Replace("fold along x=", ""));
          Fold(grid, maxX, maxY, val, 0);
          maxX = val+1;
          Console.WriteLine($"Fold X {val}");
          var count = Print(grid, maxX, maxY);
        }

        if (lines[i].Contains("y="))
        {
          if (!gridParsed)
          {
            gridParsed = true;
            Console.WriteLine($"Original");
            //Print(grid, 15, 15);
          }
          var val = Convert.ToInt32(lines[i].Replace("fold along y=", ""));
          Fold(grid, maxX, maxY, 0, val);
          maxY = val+1;
          Console.WriteLine($"Fold Y {val}");
          var count = Print(grid, maxX, maxY);
        }
      }

      return 0;
    }

    public void Fold( int[,] grid, int maxX, int maxY, int foldX, int foldY)
    {
      if( foldX > 0 || foldY > 0)
      {
        for(int x = foldX; x < maxX; x++)
        {
          for (int y = foldY; y < maxY; y++)
          {
            if (grid[y, x] > 0)
            {
              var newX = x;
              var newY = y;

              if (foldX > 0)
              {
                newX = foldX - (x - foldX);
              }

              if (foldY > 0)
              {
                newY = foldY - (y - foldY);
              }

              if (newX >= 0 && newY >= 0)
              {
                grid[newY, newX] += grid[y, x];
              }
              else
              {

              }
            }
          }
        }
      }
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
