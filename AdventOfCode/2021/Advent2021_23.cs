using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode._2021
{
  class Advent2021_23 : IAdventProblem
  {
    public string RawInput
    {
      get;
      set;
    }

    /*
     * #############
       #...........#
       ###D#D#A#A###
         #C#C#B#B#
         #########
    */
    public object Part1(string input, bool isTestData)
    {
      var lines = ParseInput(input, isTestData);

      var board = new List<string>();
      /*
      board.Add(".");
      board.Add(".");
      board.Add(".DC");
      board.Add(".");
      board.Add(".DC");
      board.Add(".");
      board.Add(".AB");
      board.Add(".");
      board.Add(".AB");
      board.Add(".");
      board.Add(".");
      */

      /*
      board.Add(".");
      board.Add(".");
      board.Add("A.A");
      board.Add(".");
      board.Add(".BB");
      board.Add(".");
      board.Add(".CC");
      board.Add(".");
      board.Add(".DD");
      board.Add(".");
      board.Add(".");
      */

      
      board.Add(".");
      board.Add(".");
      board.Add(".DA");
      board.Add(".");
      board.Add(".BB");
      board.Add(".");
      board.Add(".CC");
      board.Add(".");
      board.Add(".AD");
      board.Add(".");
      board.Add(".");
      

      Move(board, 0, 0, -1, -1);

      return MinSolution;
    }

    public List<Tuple<int,int,int>> GetPossibleMoves(List<string> board, int x, int y)
    {
      var possibleMoves = new List<Tuple<int, int,int>>();

      // Must exist and be a letter
      char letter = '?';
      try
      {
        letter = board[x][y];
        if (letter == '.')
        {
          return possibleMoves;
        }
      }
      catch
      {
        return possibleMoves;
      }
      
      // Check destinations
      for(int destY = 0; destY < 3; destY++)
      {
        for (int destX = 0; destX < board.Count; destX++)
        {
          if (y > 0)
          {
            if( x == 2 && letter == 'A' )
            {
              if( y == 2 || board[x][2] == 'A')
              {
                continue;
              }
            }
            if (x == 4 && letter == 'B')
            {
              if (y == 2 || board[x][2] == 'B')
              {
                continue;
              }
            }
            if (x == 6 && letter == 'C')
            {
              if (y == 2 || board[x][2] == 'C')
              {
                continue;
              }
            }
            if (x == 8 && letter == 'D')
            {
              if (y == 2 || board[x][2] == 'D')
              {
                continue;
              }
            }
          }

          var cost = CostOfMove(board, x, y, destX, destY);
          if (cost != -1)
          {
            possibleMoves.Add(Tuple.Create(destX, destY, cost));
          }
        }
      }


      return possibleMoves;
    }

    public int MinSolution
    {
      get;
      set;
    } = int.MaxValue;

    public HashSet<string> HashSet
    {
      get;
      set;
    } = new HashSet<string>();


    public void Move( List<string> board, int depth, int total, int prevX, int prevY )
    {
      if (IsSolved(board))
      {
        MinSolution = total < MinSolution ? total : MinSolution;
      }

      depth++;

      if( depth > 6 || (MinSolution != int.MaxValue && total > MinSolution))
      {
        return;
      }

      for (int y = 0; y < 3; y++)
      {
        for (int x = 0; x < board.Count; x++)
        {
          foreach( var move in GetPossibleMoves(board, x, y))
          {
            if( x == prevX && y == prevY)
            {
              continue;
            }

            var board2 = new List<string>(board);
            board2[move.Item1] = board2[move.Item1].Remove(move.Item2,1).Insert(move.Item2, board[x][y].ToString());
            board2[x] = board2[x].Remove(y,1).Insert(y, ".");


            var key = String.Join("", board2).ToString();
            if( HashSet.Contains(key))
            {
              continue;
            }
            else
            {
              HashSet.Add(key);
            }

            Move(board2, depth, total + move.Item3, move.Item1, move.Item2);
          }
        }
      }
    }

    public int CostOfMove( List<string> board, int x, int y, int destX, int destY )
    {
      if( CanMove(board, x, y, destX, destY) )
      {
        var character = board[x][y];
        var value = 0;
        switch( character)
        {
          case 'A':
            value = 1;
            break;

          case 'B':
            value = 10;
            break;

          case 'C':
            value = 100;
            break;

          case 'D':
            value = 1000;
            break;
        }

        var steps = Math.Abs(x - destX) + y + destY;
        return steps * value;
      }

      return -1;
    }

    public bool CanMove(List<string> board, int x, int y, int destX, int destY)
    {
      // Check existence
      try
      {
        if( board[destX][destY] != '.' )
        {
          return false;
        }
      }
      catch
      {
        return false;
      }

      // Destination is valid
      if (y > 0)
      {
        // Validate depth
        for (int i = 0; i <= destY; i++)
        {
          if (board[destX][i] != '.')
          {
            return false;
          }
        }

        // Validate it is the deepest item
        for (int i = board[destX].Length - 1; i > 0; i--)
        {
          if (board[destX][i] == '.' && destY != i)
          {
            return false;
          }
        }

        // Validate it is the correct index
        if (!(destX == 2 && board[x][y] == 'A' ||
              destX == 4 && board[x][y] == 'B' ||
              destX == 6 && board[x][y] == 'C' ||
              destX == 8 && board[x][y] == 'D' ))
        {

        }
        else
        {
          return false;
        }
      }

      // Validate that the path is clear
      var minX = x < destX ? x : destX;
      
      for (int i = minX; i < Math.Abs(destX-x); i++)
      {
        if (board[i][0] != '.')
        {
          return false;
        }
      }

      return true;
    }

    public bool IsSolved( List<string> board )
    {
      return board[2] == ".AA" &&
             board[4] == ".BB" &&
             board[6] == ".CC" &&
             board[8] == ".DD";
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
        return new List<string>(input.Trim().Split("\r\n"));
      }
      
      return new List<string>(input.Trim().Split("\n"));
    }
  }
}
