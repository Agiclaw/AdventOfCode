using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode._2019
{
  class Advent2020_04 : IAdventProblem
  {
    public object Part1(string input)
    {
      var count = 0;
      for( int i = 125730; i <= 579381; i++)
      {
        if( IsValid(i))
        {
          count++;
        }
      }

      return 0;
    }

    public object Part2(string input)
    {
      return 0;
    }

    public bool IsValid( int i )
    {

      return true;
    }
  }
}
