using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
  class Advent2020_07 : IAdventProblem
  {
    public Dictionary<string, Bag> Bags = new Dictionary<string, Bag>();

    public object Part1(string input)
    {
      ParseInput(input);

      var canContain = new HashSet<Bag>();

      CanContain(canContain, Bags["shiny gold"]);

      return canContain.Count;
    }

    public object Part2(string input)
    {
      var contains = new List<Bag>();

      TotalBags(contains, Bags["shiny gold"]);

      return contains.Count;
    }

    public void ParseInput( string input )
    {
      var inputStrings = input.Trim().Split("\n");

      foreach ( var bagInfo in inputStrings)
      {
        var bagInfoParts = bagInfo.Split(" bags contain ");
        var bagDef = bagInfoParts[0].Split(" ");

        Bag bag = null;
        var bagName = $"bagDef[0] bagDef[1]";
        if ( Bags.ContainsKey(bagName))
        {
          bag = Bags[bagName];
        }
        else
        {
          bag = new Bag(bagDef[0], bagDef[1]);
          Bags.Add(bag.ToString(), bag);
        }

        // Regex for contained definitions
        MatchCollection matches = Regex.Matches(bagInfoParts[1], $"(\\d+) (\\w+) (\\w+) bag");
        foreach( Match match in matches)
        { 
          bag.Container.Add($"{match.Groups[2]} {match.Groups[3]}", Convert.ToInt32(match.Groups[1].ToString()));
        }
      }
    }

    public void CanContain(HashSet<Bag> bags, Bag targetBag )
    {
      foreach( var bagEntry in Bags )
      {
        if( bagEntry.Value.Container.ContainsKey(targetBag.ToString()))
        {
          if( !bags.Contains(bagEntry.Value))
          {
            bags.Add(bagEntry.Value);

            CanContain(bags, bagEntry.Value);
          }
        }
      }
    }

    public void TotalBags(List<Bag> bags, Bag targetBag)
    {
      foreach (var bagEntry in targetBag.Container)
      {
        for( int i = 0; i < bagEntry.Value; i++)
        {
          bags.Add(Bags[bagEntry.Key]);
          TotalBags(bags, Bags[bagEntry.Key]);
        }
      }
    }

    public class Bag
    {
      public string Adjective
      {
        get;
        set;
      }

      public string Color
      {
        get;
        set;
      }

      public Dictionary<string, int> Container
      {
        get;
        set;
      } = new Dictionary<string, int>();

      public Bag( string adjective, string color)
      {
        Adjective = adjective;
        Color = color;
      }

      public override string ToString()
      {
        return $"{Adjective} {Color}";
      }
    }
  }
}
