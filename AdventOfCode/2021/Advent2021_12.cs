using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode._2021
{
  class Advent2021_12 : IAdventProblem
  {
    public string RawInput
    {
      get;
      set;
    }

    public List<string> Paths
    {
      get;
      set;
    } = new List<string>();

    public object Part1(string input, bool isTestData)
    {
      var lines = ParseInput(input, isTestData);

      if( !isTestData )
      {
        //return 0;
      }
      var connections = new Dictionary<string, List<string>>();

      foreach( var line in lines )
      {
        var parts = line.Split("-");

        if( !connections.ContainsKey(parts[0]))
        {
          connections.Add(parts[0], new List<string>());
        }

        connections[parts[0]].Add(parts[1]);

        if (!connections.ContainsKey(parts[1]))
        {
          connections.Add(parts[1], new List<string>());
        }

        connections[parts[1]].Add(parts[0]);
      }

      foreach ( var node in connections["start"])
      {
        CollectPaths(node, connections, "start,", false);
      }

      foreach( var path in Paths )
      {
        //Console.WriteLine(path);
      }

      Paths = Paths.Distinct().ToList();
      var total = Paths.Count;
      Paths.Clear();
      return total;
    }

    public void CollectPaths(string node, Dictionary<string, List<string>> connections, string path, bool usedDoubleSmall)
    {
      if(node != "end" && node == node.ToLower() && path.Contains($"{node},"))
      {
        if(usedDoubleSmall)
        {
          return;
        }

        usedDoubleSmall = true;
      }

      if (node == "end")
      {
        Paths.Add(path+"end");
        return;
      }

      path += $"{node},";


      foreach (var internalNode in connections[node])
      {
        if( internalNode == "start")
        {
          continue;
        }

        CollectPaths(internalNode, connections, path, usedDoubleSmall);
      }

      return;
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

    public void Print(int[][] grid, int maxX, int maxY)
    {
      var sb = new StringBuilder();
      for (var y = 0; y < maxY; y++)
      {
        for (var x = 0; x < maxX; x++)
        {
          sb.Append(grid[y][x]);
        }
        sb.Append("\n");
      }

      Console.WriteLine(sb.ToString());
    }
  }
}
