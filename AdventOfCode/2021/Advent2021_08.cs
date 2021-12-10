using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2021
{
  class Advent2021_08 : IAdventProblem
  {
    public object Part1(string input, bool isTestData)
    {
      var lines = ParseInput(input);
      var count = 0;
      foreach( var lin in lines )
      {
        var line = lin.Split("|");
        var t = line[1].Split(" ");
        count += t.Where(s => s.Trim().Length == 2 || s.Trim().Length == 4 || s.Trim().Length == 3 || s.Trim().Length == 7).Count();
      }
      return count;
    
    }

    public object Part2(string input, bool isTestData)
    {
      var lines = ParseInput(input);

      var total = 0;

      foreach (var lin in lines)
      {
        var line = lin.Split("|");
        var data = line[0].Trim().Split(" ").ToList();
        var output = line[1].Trim().Split(" ").ToList();

        var possibilities = new Dictionary<string, List<int>>();

        total += Deduce(data, output);
      }

      return total;

    }

    public int Deduce( List<string> data, List<string> outputs )
    {
      var possibilities = new List<string>();
      var solved = new Dictionary<int, string>();

      for ( int i = 0; i < 10; i++)
      {
        var sorted = String.Concat(data[i].OrderBy(c => c));

        if (sorted.Length == 2 )
        {
          solved.Add(1, sorted);
        }
        else if (sorted.Length == 3)
        {
          solved.Add(7, sorted);
        }
        else if (sorted.Length == 4)
        {
          solved.Add(4, sorted);
        }
        else if(sorted.Length == 7)
        {
          solved.Add(8, sorted);
        }
        else
        {
          possibilities.Add(sorted);
        }
      }

      var _4and7 = (solved[4] + solved[7]).Distinct().ToList();

      // Find 9
      var nine = possibilities.Where(s => s.Contains(_4and7[0]) && s.Contains(_4and7[1]) && s.Contains(_4and7[2]) && s.Contains(_4and7[3]) && s.Length == 6 ).FirstOrDefault();
      solved.Add(9, nine);
      possibilities.Remove(nine);

      // Find 0
      var zero = possibilities.Where(s => s.Contains(solved[1][0]) && s.Contains(solved[1][1]) && s.Length == 6 ).FirstOrDefault();
      solved.Add(0, zero);
      possibilities.Remove(zero);

      // Find 6
      var six = possibilities.Where(s => s.Length == 6).FirstOrDefault();
      solved.Add(6, six);
      possibilities.Remove(six);

      // Find 3
      var three = possibilities.Where(s => s.Contains(solved[1][0]) && s.Contains(solved[1][1])).FirstOrDefault();
      solved.Add(3, three);
      possibilities.Remove(three);

      // Find 2
      var two = possibilities.Where(s => 
          s.Contains(solved[8].Where(m9 => 
            !solved[9].Contains(m9)).FirstOrDefault())).FirstOrDefault();

      solved.Add(2, two);
      possibilities.Remove(two);

      /// 5
      // Find 5
      var five = possibilities.Last();
      solved.Add(5, five);
      possibilities.Remove(five);


      var sb = new StringBuilder();

      foreach( var l in outputs )
      {
        var sorted = String.Concat(l.OrderBy(c => c));
        foreach ( var k in solved)
        {
          if(sorted == k.Value)
          {
            sb.Append(k.Key);
            break;
          }
        }
      }

      return Convert.ToInt32(sb.ToString());
    }

    public List<string> ParseInput(string input)
    {
      return new List<string>(input.Trim().Split("\n"));
    }
  }
}
