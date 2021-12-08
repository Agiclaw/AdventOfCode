using System;
using System.Collections.Generic;

namespace AdventOfCode._2020
{
  class Advent2020_08 : IAdventProblem
  {
    public string RawInput
    {
      get;
      set;
    }

    public int Accumulator
    {
      get;
      set;
    }

    public object Part1(string input, bool isTestData)
    {
      Run(ParseInput(input));
      return Accumulator;
    }

    public object Part2(string input, bool isTestData)
    {
      var instuctions = ParseInput(input);
      for ( int i = 0; i < instuctions.Length; i++ )
      {
        if( instuctions[i].StartsWith("nop") )
        {
          var newInstructions = new string[instuctions.Length];
          instuctions.CopyTo(newInstructions, 0);

          newInstructions[i] = newInstructions[i].Replace("nop", "jmp");

          if( Run(newInstructions) == newInstructions.Length)
          {
            return Accumulator;
          }
        }

        if (instuctions[i].StartsWith("jmp") )
        {
          var newInstructions = new string[instuctions.Length];
          instuctions.CopyTo(newInstructions, 0);

          newInstructions[i] = newInstructions[i].Replace("jmp", "nop");

          if (Run(newInstructions) == newInstructions.Length)
          {
            return Accumulator;
          }
        }

      }

      return -1;
    }

    public string[] ParseInput( string input )
    {
      RawInput = input;
      return input.Split("\n");
    }

    public int Run(string[] instructions)
    {
      Accumulator = 0;
      var seenInstruction = new HashSet<int>();

      int cursor = 0;
      for (; cursor < instructions.Length; )
      {
        //Console.WriteLine($"{cursor}  {instructions[cursor]} -- {Accumulator}");

        if ( seenInstruction.Contains(cursor))
        {
          return cursor;
        }
        else
        {
          seenInstruction.Add(cursor);
        }

        var instruction = instructions[cursor].Split(" ");

        switch (instruction[0])
        {
          case "acc":
            Accumulator += Convert.ToInt32(instruction[1]);
            cursor++;
            continue;

          case "jmp":
            cursor += Convert.ToInt32(instruction[1]);
            continue;

          case "nop":
            cursor++;
            continue;

          default:
            cursor++;
            continue;
        }
      }

      return cursor;
    }
  }
}
