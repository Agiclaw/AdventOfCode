using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode._2021
{
  class Advent2021_21 : IAdventProblem
  {
    public string RawInput
    {
      get;
      set;
    }

    public object Part1(string input, bool isTestData)
    {
      var lines = ParseInput(input, isTestData);

      var p1Position = Convert.ToInt32(lines[0].Replace("Player 1 starting position: ", ""));
      var p2Position = Convert.ToInt32(lines[1].Replace("Player 2 starting position: ", ""));

      var p1Score = 0;
      var p2score = 0;

      var p1DieRolls = 0;
      var p2DieRolls = 0;

      var dice = Deterministic100().GetEnumerator();
      dice.MoveNext();

      var loserTotal = 0;

      while ( true )
      {
        // Player 1
        var sb1 = new StringBuilder();
        sb1.Append("Player 1 rolls: ");
        for (int i = 0; i < 3; i++)
        {
          p1Position += dice.Current;
          sb1.Append($"{dice.Current}, ");
          dice.MoveNext();
          p1DieRolls++;
        }
        var mod1 = p1Position % 10;
        mod1 = mod1 == 0 ? 10 : mod1;
        p1Position = mod1;

        p1Score += p1Position;
        sb1.Append($" and moves to space {p1Position} for a total score of {p1Score}");
        //Console.WriteLine(sb1.ToString());

        if (p1Score >= 1000)
        {
          loserTotal = p2score;
          break;
        }

        // Player 2
        for (int j = 0; j < 3; j++)
        {
          p2Position += dice.Current;
          dice.MoveNext();
          p2DieRolls++;
        }

        var mod2 = p2Position % 10;
        mod2 = mod2 == 0 ? 10 : mod2;
        p2Position = mod2;

        p2score += p2Position;

        if (p2score >= 1000)
        {
          loserTotal = p1Score;
          break;
        }
      }



      return ( p1DieRolls + p2DieRolls ) * loserTotal;
    }

 
    public IEnumerable<int> Deterministic100()
    {
      var value = 1;
      while(true)
      {
        yield return value;
        value++;

        if( value > 100 )
        {
          value = 1;
        }
      }
    }

    public object Part2(string input, bool isTestData)
    {
      var lines = ParseInput(input, isTestData);

      var boardSize = 10;
      var p1Position = Convert.ToInt32(lines[0].Replace("Player 1 starting position: ", ""));
      var p2Position = Convert.ToInt32(lines[1].Replace("Player 2 starting position: ", ""));

      var p1Score = 0;
      var p2score = 0;

      var p1DieRolls = 0;
      var p2DieRolls = 0;

      p1Universes = 0;
      p2Universes = 0;

      Dirac(p1Position, p2Position, 0, 0, true, 1);

      Console.WriteLine(p1Universes);
      Console.WriteLine(p2Universes);

      return 0;
    }


    public int[] D3Odds
    {
      get;
      set;
    } = new int[10]{ 0, 0, 0, 1, 3, 6, 7, 6, 3, 1 };

  public BigInteger p1Universes
    {
      get;
      set;
    }

    public BigInteger p2Universes
    {
      get;
      set;
    }

    public void Dirac( int p1Position, int p2Position, int p1Score, int p2Score, bool isP1Turn, long depth )
    {
      if (isP1Turn)
      {
        for (var i = 3; i <= 9; ++i)
        {
          var newp1Position = Move(p1Position, i);
          var newP1Score = p1Score + newp1Position;
          if (newP1Score >= 21)
          {
            p1Universes += depth * D3Odds[i];
          }
          else
          {
            Dirac(newp1Position, p2Position, newP1Score, p2Score, !isP1Turn, depth * D3Odds[i]);
          }
        }
      }
      else
      {
        for (var i = 3; i <= 9; ++i)
        {
          var newp2Position = Move(p2Position, i);
          var newP2Score = p2Score + newp2Position;
          if (newP2Score >= 21)
          {
            p2Universes += depth * D3Odds[i];
          }
          else
          {
            Dirac(p1Position, newp2Position, p1Score, newP2Score, !isP1Turn, depth * D3Odds[i]);
          }
        }
      }
    }

    int Move( int position, int steps)
    {
      position += steps;
      position %= 10;
      position = position == 0 ?  10 : position;
      return position;
    }


    public List<string> ParseInput(string input, bool isTestData)
    {
      RawInput = input.Trim();

      if (isTestData)
      {
        return new List<string>(input.Trim().Split("\r\n"));
      }
      
      return new List<string>(input.Trim().Split("\n"));
    }
  }
}
