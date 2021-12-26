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
  class Advent2021_18 : IAdventProblem
  {
    public string RawInput
    {
      get;
      set;
    }

    public object Part1(string input, bool isTestData)
    {
      if( !isTestData )
      {
        return 0;
      }

      var lines = ParseInput(input, isTestData);

      bool hasExploded = false;
      bool hasSplit = false;

      string result = lines[0];
      for (int i = 1; i < lines.Count; i++)
      {
        Console.WriteLine($"\n\n\nADD:\n{result}{lines[i]}");
        result = Add(result, lines[i]);

        var wasModified = false;
        do
        {
          wasModified = false;
          do
          {
            // Explode
            hasExploded = false;
            var afterExplode = Explode(result);
            if (afterExplode != result)
            {
              hasExploded = true;
              wasModified = true;
              Console.WriteLine(afterExplode);
            }
            result = afterExplode;
          } while (hasExploded);


          // Split
          hasSplit = false;
          var afterSplit = Split(result);
          if (afterSplit != result)
          {
            hasSplit = true;
            wasModified = true;
            Console.WriteLine(afterSplit);
          }

          result = afterSplit;

        } while (wasModified);
      }

      return result;
    }
    public class TreeNode
    {
      public int val;
      public TreeNode left;
      public TreeNode right;
      public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
      {
        this.val = val;
        this.left = left;
        this.right = right;
      }
    }




    public string Add(string line1, string line2)
    {
      return $"[{line1},{line2}]";
    }

    public string Explode( string line )
    {
      var depth = 0;
      var maxDepth = 0;
      bool maxEndFound = false;
      var start = -1;
      var end = -1;
      for (int i = 0; i < line.Length; i++)
      {
        if (line[i] == '[')
        {
          depth++;

          if( depth > maxDepth)
          {
            maxDepth = depth;
            start = i;
            maxEndFound = false;
          }
        }

        if (line[i] == ']')
        {
          if (start != -1 && maxDepth == depth && !maxEndFound )
          {
            end = i;
            maxEndFound = true;
          }

          depth--;
        }
      }

      if (start != -1 && end != -1 && maxDepth > 4)
      {
        var toExplode = line.Substring(start, (end - start) + 1);
        Console.WriteLine($"Explode:{toExplode}");
        var parts = toExplode.Split(",");

        var left = Convert.ToInt32(parts[0].Replace("[", ""));
        var right = Convert.ToInt32(parts[1].Replace("]", ""));

        var newRight = AddRightNumber(line.Substring(end, line.Length - end), right);
        line = line.Remove(end, line.Length - end);
        line = line.Insert(end, newRight);

        line = line.Remove(start, (end - start) + 1);
        line = line.Insert(start, "0");

        var newLeft = AddLeftNumber(line.Substring(0, start), left);
        line = line.Remove(0, start);
        line = line.Insert(0, newLeft);
      }

      return line;
    }

    public string AddLeftNumber(string line, int num )
    {
      var start = -1;
      var end = -1;

      for (int i = line.Length-1; i >= 0; i--)
      {
        if (char.IsDigit(line[i]))
        {
          if (start == -1)
          {
            start = i;
          }

          end = i;
        }
        else
        {
          if (end != -1)
          {
            break;
          }
        }
      }

      if (start == -1 )
      {
        return line;
      }

      var nextNumber = Convert.ToInt32(line.Substring(end, (start - end) + 1));
      nextNumber += num;

      line = line.Remove(end, (start - end) + 1);
      line = line.Insert(end, nextNumber.ToString());
      return line;
    }

    public string AddRightNumber(string line, int num)
    {
      var start = -1;
      var end = -1;

      for (int i = 0; i < line.Length; i++)
      {
        if(char.IsDigit(line[i]))
        {
          if( start == -1 )
          {
            start = i;
          }

          end = i;
        }
        else
        {
          if( end != -1)
          {
            break;
          }
        }
      }

      if (start == -1)
      {
        return line;
      }

      var nextNumber = Convert.ToInt32(line.Substring(start, (end-start)+1));
      nextNumber += num;

      line = line.Remove(start, (end - start)+1);
      line = line.Insert(start, nextNumber.ToString());
      return line;
    }

    public string Split(string line )
    {
      var start = -1;
      var end = -1;

      var trackingNum = false;
      for (int i = 0; i < line.Length; i++)
      {
        if (char.IsDigit(line[i]))
        {
          if (!trackingNum)
          {
            start = i;
            trackingNum = true;
          }
          end = i;
        }
        else
        {
          trackingNum = false;
          if ( (end-start)+1 > 1)
          {
            break;
          }
        }
      }

      if ((end - start) + 1 > 1)
      {
        var value = Convert.ToInt32(line.Substring(start, (end - start) + 1));
        Console.WriteLine($"Split:{value}");
        var newLeft = Math.Floor(value / 2.0f);
        var newRight = Math.Floor(value / 2.0f) + 1;

        line = line.Remove(start, (end - start) + 1);
        line = line.Insert(start, $"[{newLeft},{newRight}]");
      }

      return line;

    }
    public object Part2(string input, bool isTestData)
    {
      return 0;
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
