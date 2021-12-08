using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
  interface IAdventProblem
  {
    object Part1(string input, bool isTestData = false);
    object Part2(string input, bool isTestData = false);
  }
}
