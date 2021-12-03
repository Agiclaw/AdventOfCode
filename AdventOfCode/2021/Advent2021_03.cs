using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
  class Advent2021_03 : IAdventProblem
  {
    public object Part1(string input)
    {
      var lines = ParseInput(input);

      StringBuilder gamma = new StringBuilder("000000000000");
      StringBuilder epsilon = new StringBuilder("000000000000");

      var count0 = 0;
      var count1 = 0;

      for (int i = 0; i < 12; i++)
      {
        for (int j = 0; j < lines.Count; j++)
        {
          var character = lines[j][i];
          if (character == '0')
          {
            count0++;
          }
          else
          {
            count1++;
          }
        }

        if( count0 > count1)
        {
          gamma[i] = '0';
          epsilon[i] = '1';
        }
        else
        {
          gamma[i] = '1';
          epsilon[i] = '0';
        }

        count0 = 0;
        count1 = 0;
      }

      return Convert.ToInt32(gamma.ToString(), 2) * Convert.ToInt32(epsilon.ToString(), 2);
    }

    public object Part2(string input)
    {
      var lines = ParseInput(input);

      var oxValue = 0;
      var c02Value = 0;

      var workingList = new List<string>(lines);

      for (int i = 0; i < 12; i++)
      {
        var common = FindMostCommon(workingList, i);

        workingList = workingList.Where(l => l[i] == common).ToList<string>();
        if (workingList.Count == 1)
        {
          oxValue = Convert.ToInt32(workingList[0].ToString(), 2);
          break;
        }
      }

      workingList = new List<string>(lines);
      for (int i = 0; i < 12; i++)
      {
        var common = FindLeastCommon(workingList, i);

        workingList = workingList.Where(l => l[i] == common).ToList<string>();
        if (workingList.Count == 1)
        {
          c02Value = Convert.ToInt32(workingList[0].ToString(), 2);
          break;
        }
      }

      return oxValue * c02Value;
    }

    public char FindMostCommon( List<string> list, int index)
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
        return '1';
      }

      return dictionary.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
    }

    public char FindLeastCommon(List<string> list, int index)
    {
      var dictionary = new Dictionary<char, int>();

      list.ForEach(s =>
      {
        int currentCount = 0;
        dictionary.TryGetValue(s[index], out currentCount);
        dictionary[s[index]] = currentCount + 1;
      });

      if (dictionary['0'] == dictionary['1'])
      {
        return '0';
      }

      return dictionary.Aggregate((x, y) => x.Value < y.Value ? x : y).Key;
    }

    public List<string> ParseInput(string input)
    {
      return new List<string>(input.Trim().Split("\n"));
    }
  }
}
