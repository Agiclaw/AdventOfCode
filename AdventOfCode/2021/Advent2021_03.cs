using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
  class Advent2021_03 : IAdventProblem
  {
    public object Part1(string input, bool isTestData)
    {
      var lines = ParseInput(input);

      StringBuilder gamma = new StringBuilder("000000000000");
      StringBuilder epsilon = new StringBuilder("000000000000");

      for (int i = 0; i < lines[0].Length; i++)
      {
        gamma[i] = Commonality(lines, i);
        epsilon[i] = gamma[i] == '0' ? '1' : '0';
      }

      return Convert.ToInt32(gamma.ToString(), 2) * Convert.ToInt32(epsilon.ToString(), 2);
    }

    public object Part2(string input, bool isTestData)
    {
      var lines = ParseInput(input);

      var oxValue = 0;
      var c02Value = 0;

      var workingList = new List<string>(lines);
      for (int i = 0; i < lines[0].Length; i++)
      {
        var common = Commonality(workingList, i, true, '1');

        workingList = workingList.Where(l => l[i] == common).ToList<string>();
        if (workingList.Count == 1)
        {
          oxValue = Convert.ToInt32(workingList[0].ToString(), 2);
          break;
        }
      }

      workingList = new List<string>(lines);
      for (int i = 0; i < lines[0].Length; i++)
      {
        var uncommon = Commonality(workingList, i, false, '0');

        workingList = workingList.Where(l => l[i] == uncommon).ToList<string>();
        if (workingList.Count == 1)
        {
          c02Value = Convert.ToInt32(workingList[0].ToString(), 2);
          break;
        }
      }

      return oxValue * c02Value;
    }

    public char Commonality( List<string> list, int index, bool mostCommon = true, char defaultChar = '1')
    {
      var dictionary = new Dictionary<char, int>();

      list.ForEach(s =>
       {
         int currentCount = 0;
         dictionary.TryGetValue(s[index], out currentCount);
         dictionary[s[index]] = currentCount + 1;
       });

      if( dictionary['0'] == dictionary['1'])
      {
        return defaultChar;
      }

      return dictionary.Aggregate((x, y) => mostCommon ? (x.Value > y.Value ? x : y) : (x.Value < y.Value ? x : y)).Key;
    }

    public List<string> ParseInput(string input)
    {
      return new List<string>(input.Trim().Split("\n"));
    }
  }
}
