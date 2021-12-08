using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
  class Advent2021_04 : IAdventProblem
  {
    public object Part1(string input, bool isTestData)
    {
      var lines = ParseInput(input);

      var balls = lines[0];

      // Populate board data
      var boards = new List<int[][]>();
      for( int i = 1; i < lines.Count; i++ )
      {
        var board = lines[i].Split('\n').Select( row => row.Replace( "  ", " ").Trim().Split(' ').Select( num => Convert.ToInt32(num)).ToArray()).ToArray();

        boards.Add(board);
      }

      // draw balls
      foreach( var ball in balls.Split(',') )
      {
        var intBall = Convert.ToInt32(ball);

        // Mark board with sentinel
        foreach( var board in boards )
        {
          for (int i = 0; i < board.Length; i++)
          {
            for (int j = 0; j < board.Length; j++)
            {
              if (board[i][j] == intBall)
              {
                board[i][j] = -1;
              }
            }
          }

          var winner = Evaluate(board);
          if (winner != null)
          {
            var sum = 0;
            foreach (var element in winner)
            {
              if (element != -1)
              {
                sum += element;
              }
            }

            return intBall * sum;
          }
        }
      }

      return 0;
    }

    public object Part2(string input, bool isTestData)
    {
      var lines = ParseInput(input);

      var balls = lines[0];

      // Populate board data
      var boards = new List<int[][]>();
      for (int i = 1; i < lines.Count; i++)
      {
        var board = lines[i].Split('\n').Select(row => row.Replace("  ", " ").Trim().Split(' ').Select(num => Convert.ToInt32(num)).ToArray()).ToArray();

        boards.Add(board);
      }

      // draw balls
      var winners = new List<int>();
      var splitBalls = balls.Split(',');
      var lastWinningScore = 0;
      for (var ballIndex = 0; ballIndex < splitBalls.Count(); ballIndex++)
      {
        var intBall = Convert.ToInt32(splitBalls[ballIndex]);

        // Mark board with sentinel
        for (var boardIndex = 0; boardIndex < boards.Count(); boardIndex++)
        {
          for (int i = 0; i < boards[boardIndex].Length; i++)
          {
            for (int j = 0; j < boards[boardIndex].Length; j++)
            {
              if (boards[boardIndex][i][j] == intBall)
              {
                boards[boardIndex][i][j] = -1;
              }
            }
          }

          if( !winners.Contains(boardIndex))
          {
            var winner = Evaluate(boards[boardIndex]);
            if (winner != null)
            {
              winners.Add(boardIndex);
              lastWinningScore = Score(boards[boardIndex], intBall);
            }
          }
        }
      }

      return lastWinningScore;
    }

    public List<int> Evaluate(int[][] board )
    {
      var rowCount = 0;
      var colCount = 0;

      for (int i = 0; i < 5; i++)
      {
        for (int j = 0; j < 5; j++)
        {
          rowCount = board[i][j] == -1 ? rowCount + 1 : rowCount;
          colCount = board[j][i] == -1 ? colCount + 1 : colCount;
        }

        if (rowCount == 5 || colCount == 5)
        {
          return board.SelectMany(s => s).ToList<int>();
        }

        rowCount = 0;
        colCount = 0;
      }
      
    
      return null;
    }

    public int Score(int[][] board, int ballValue )
    {
      var sum = 0;
      foreach (var element in board.SelectMany(s => s).ToList<int>())
      {
        if (element != -1)
        {
          sum += element;
        }
      }

      return ballValue * sum;
    }

    public List<string> ParseInput(string input)
    {
      return new List<string>(input.Trim().Split("\n\n"));
    }
  }
}
