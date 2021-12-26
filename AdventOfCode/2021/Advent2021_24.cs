using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode._2021
{
  class Advent2021_24 : IAdventProblem
  {
    public string RawInput
    {
      get;
      set;
    }

    public object Part1(string input, bool isTestData)
    {
      var instructionSets = ParseInput(input, isTestData);

      var ALUs = new List<ALU>();
      for( int iS = 1; iS < instructionSets.Count; iS++)
      {
        var ALU = new ALU();
        if (isTestData)
        {
          ALU.Instructions = instructionSets[iS].Split("\r\n").ToList();
        }
        else
        {
          ALU.Instructions = instructionSets[iS].Split("\n").ToList();
        }

        ALUs.Add(ALU);
      }

      Run(ALUs);
     
      return 0;
    }

    public void Run( List<ALU> ALUs)
    {
      var currentALU = new ALU();
      for( int i = 0; i < 14; i++ )
      {
        currentALU.Instructions = ALUs[ALUs.Count - i].Instructions;
        for (int n = 1; n < 10 ; n++)
        {
          //currentALU =
          //ALUs[ALUs.Count - i]
        }
      }
    }

    public object Part2(string input, bool isTestData)
    {
      var lines = ParseInput(input, isTestData);



      return 0;
    }

    public List<string> ParseInput(string input, bool isTestData)
    {
      RawInput = input.Trim();

      if (isTestData)
      {
        return new List<string>(input.Trim().Split("inp"));
      }
      
      return new List<string>(input.Trim().Split("inp"));
    }

    public class ALU
    {
      public Dictionary<char, int> Registers
      {
        get;
        set;
      } = new Dictionary<char, int>() {
        { 'w',  0 },
        { 'x',  0 },
        { 'y',  0 },
        { 'z',  0 }
      };

      public List<string> Instructions
      {
        get;
        set;
      } = new List<string>();

      public ALU()
      {

      }

      public ALU( ALU state )
      {
        Registers = new Dictionary<char, int>(state.Registers);
      }

      public bool Run( int input )
      {
        var registersTouched = new StringBuilder();

        var index = 0;
        foreach( var i in Instructions)
        {
          if( i.StartsWith(" "))
          {
            Registers[i[1]] = input;
            index++;
            continue;
          }

          var parts = i.Split(" ");
          var left = 0;
          var right = 0;
          if (Char.IsDigit(parts[1][0]))
          {
            left = Convert.ToInt32(parts[1]);
          }
          else
          {
            left = Registers[parts[1][0]];
          }

          if (Char.IsDigit(parts[2][0]))
          {
            right = Convert.ToInt32(parts[2]);
          }
          else
          {
            right = Registers[parts[2][0]];
          }

          switch ( parts[0] )
          {
            case "add":
              Registers[parts[1][0]] = left + right;
              break;

            case "mul":
              Registers[parts[1][0]] = left * right;
              break;

            case "div":
              Registers[parts[1][0]] = Convert.ToInt32(left / right);
              break;

            case "mod":
              Registers[parts[1][0]] = Convert.ToInt32(left % right);
              break;

            case "eql":
              if( left == right )
              {
                Registers[parts[1][0]] = 1;
              }
              else
              {
                Registers[parts[1][0]] = 0;
              }
              break;
          }
        }

        return false;
      }
    }
  }
}
