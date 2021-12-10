using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode._2021
{
  class Advent2021_10 : IAdventProblem
  {
    public string RawInput
    {
      get;
      set;
    }

    public object Part1(string input, bool isTestData)
    {
      var lines = ParseInput(input);

      var stack = new Stack<char>();

      var paren = 0;
      var bracket = 0;
      var arrow = 0;
      var cbracket = 0;

      foreach( var line in lines )
      {
        foreach( var c in line.ToCharArray() )
        {
          switch(c)
          {
            case '(':
            case '<':
            case '[':
            case '{':
              stack.Push(c);
              break;

            case ')':
              var s = stack.Pop();
              if( s != '(')
              {
                paren++;
              }
              break;

            case '>':
              var t = stack.Pop();
              if (t != '<')
              {
                arrow++;
              }
              break;

            case ']':
              var u = stack.Pop();
              if (u != ']')
              {
                bracket++;
              }
              break;

            case '}':
              var v = stack.Pop();
              if (v != '{')
              {
                cbracket++;
              }
              break;
          }
        }
      }

      var total = (paren * 3) + (bracket * 57) + (cbracket * 1197) + (arrow * 25137);
      return total;
    }

    public object Part2(string input, bool isTestData)
    {
      var lines = ParseInput(input);

      var stack = new Stack<char>();
      var scores = new List<Int64>();

      foreach (var line in lines)
      {
        foreach (var c in line.Trim().ToCharArray())
        {
          var failed = false;

          switch (c)
          {
            case '(':
            case '<':
            case '[':
            case '{':
              stack.Push(c);
              break;

            case ')':
              if (stack.Count == 0) { break; }
              var s = stack.Pop();
              if (s != '(')
              {
                stack.Clear();
                failed = true;
              }
              break;

            case '>':
              if (stack.Count == 0) { break; }
              var t = stack.Pop();
              if (t != '<')
              {
                stack.Clear();
                failed = true;
              }
              break;

            case ']':
              if (stack.Count == 0) { break; }
              var u = stack.Pop();
              if (u != '[')
              {
                stack.Clear();
                failed = true;
              }
              break;

            case '}':
              if (stack.Count == 0) { break; }
              var v = stack.Pop();
              if (v != '{')
              {
                stack.Clear();
                failed = true;
              }
              break;
          }

          if (failed)
          {
            break;
          }
        }

        // score incompletes
        long score = 0;
        bool newScore = false;
        while (stack.Count != 0)
        {
          newScore = true;
          score = score * 5;
          var k = stack.Pop();

          switch (k)
          {
            case '(':
              score += 1;
              break;

            case '[':
              score += 2;
              break;

            case '{':
              score += 3;
              break;

            case '<':
              score += 4;
              break;
          }
        }

        if (newScore)
        {
          scores.Add(score);
        }
      }

      scores.Sort();
      var middle = scores[(scores.Count / 2)];
      return middle;
    }

    public List<string> ParseInput(string input)
    {
      RawInput = input;
      return new List<string>(input.Trim().Split("\n"));
    }
  }
}
