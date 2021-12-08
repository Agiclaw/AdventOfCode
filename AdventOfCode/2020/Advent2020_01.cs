using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode._2020
{
  class Advent2020_01 : IAdventProblem
  {
    public object Part1(string input, bool isTestData)
    {
      return FindAndMultiplyComponentsOfSum(2020, 2, parseInput(input));
    }

    public object Part2(string input, bool isTestData)
    {
      return FindAndMultiplyComponentsOfSum(2020, 3, parseInput(input));
    }

    public List<int> parseInput(string input)
    {
      var inputStrings = input.Trim().Split("\n");

      var listInts = new List<int>();
      foreach (var inputString in inputStrings)
      {
        listInts.Add(Convert.ToInt32(inputString));
      }

      return listInts;
    }

    public static int FindAndMultiplyComponentsOfSum(int targetValue, int numberOfComponents, List<int> inputList)
    {
      var componentList = new List<int>();

      foreach (var inputInt in inputList)
      {
        var inputIntDifference = targetValue - inputInt;
        if (numberOfComponents > 2)
        {
          var result = FindAndMultiplyComponentsOfSum(inputIntDifference, numberOfComponents - 1, inputList);
          if (result != 0)
          {
            return inputInt * result;
          }
        }

        // Filter 
        if (numberOfComponents == 2 && (componentList.Contains(inputInt) || componentList.Contains(inputIntDifference)))
        {
          return inputInt * inputIntDifference;
        }

        componentList.Add(inputInt);
        componentList.Add(inputIntDifference);
      }

      return 0;
    }
  }
}
