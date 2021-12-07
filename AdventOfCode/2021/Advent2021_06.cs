using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2021
{
  class Advent2021_06 : IAdventProblem
  {
    public object Part1(string input)
    {
      var lines = ParseInput(input);

      var values = lines[0].Trim().Split(",").Select(s => Convert.ToInt32(s)).ToList();

      var population = values.GroupBy(x => x).ToDictionary(x => x.Key, x => Convert.ToInt64(x.Count()));

      return TotalPopulationAtGeneration(population, 80);
    }

    public object Part2(string input)
    {
      var lines = ParseInput(input);

      var values = lines[0].Trim().Split(",").Select(s => Convert.ToInt32(s)).ToList();

      var population = values.GroupBy(x => x).ToDictionary(x => x.Key, x => Convert.ToInt64(x.Count()));

      return TotalPopulationAtGeneration(population, 6025);
    }

    public long TotalPopulationAtGeneration( Dictionary<int, long> population, int generation )
    {
      for (int n = 0; n < 9; n++)
      {
        long temp = 0;
        if (!population.TryGetValue(n, out temp))
        {
          population[n] = 0;
        }
      }

      for (int i = 0; i < generation; i++)
      {
        var temp = population[0];
        population[0] = population[1];
        population[1] = population[2];
        population[2] = population[3];
        population[3] = population[4];
        population[4] = population[5];
        population[5] = population[6];
        population[6] = population[7] + temp;
        population[7] = population[8];
        population[8] = temp;
      }

      return population.Values.Sum();
    }

    public List<string> ParseInput(string input)
    {
      return new List<string>(input.Trim().Split("\n"));
    }
  }
}
