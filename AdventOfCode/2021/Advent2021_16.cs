using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode._2021
{
  class Advent2021_16 : IAdventProblem
  {
    public string RawInput
    {
      get;
      set;
    }
    public int BestPathRisk
    {
      get;
      set;
    } = Int32.MaxValue;

    public object Part1(string input, bool isTestData)
    {
      VersionTotal = 0;
      var lines = ParseInput(input, isTestData);

      string binarystring = String.Join(String.Empty,
        RawInput.Select(
          c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0'))
        );

      var instruction = 0;
      var index = 0;

      if( !isTestData )
      {
        return 0;
      }
      ProcessPacket(binarystring);

      //var readSize;





      return VersionTotal;
    }

    public int VersionTotal

    {
      get;
      set;
    }

    public int Depth
    {
      get;
      set;
    }

    public int ProcessPacket( string packet )
    {
      Console.WriteLine(packet);
      var index = 0;

      var currentVersion = 0;
      var currentVersionSet = false;
      var currentType = 0;
      var typeSet = false;
      var literalValueBuilder = new StringBuilder();
      var literalValue = 0;

      while (index < packet.Length)
      {
        // Check Version
        if (currentVersionSet == false && index + 3 <= packet.Length)
        {
          currentVersion = Convert.ToInt32(packet.Substring(index, 3),2);
          VersionTotal += currentVersion;
          index += 3;
          currentVersionSet = true;
          continue;
        }

        // Check Type
        if (typeSet == false && index + 3 <= packet.Length)
        {
          currentType = Convert.ToInt32(packet.Substring(index, 3), 2);
          index += 3;
          typeSet = true;
          continue;
        }

        // Literal
        if (currentType == 4 && index + 5 <= packet.Length)
        {
          var literal = packet.Substring(index, 5);
          literalValueBuilder.Append(literal.Substring(1, 4));
          index += 5;
          if ( literal[0] == '0')
          {
            literalValue = Convert.ToInt32(literalValueBuilder.ToString(), 2);
            Console.WriteLine($"Literal packet: ( version:{currentVersion}, type: {currentType}), Literal:{literalValue}");
            return index;
          }

          continue;
        }

        // Operator
        if( currentType != 4 )
        {
          Console.WriteLine($"Operator packet: ( version:{currentVersion}, type: {currentType})");

          if ( packet[index] == '0')
          { 
            // Get sub packet sizes
            index += 1;
            var subPacketSize = packet.Substring(index, 15);
            Console.WriteLine($"Subpackets size;{subPacketSize}");
            var totalSubPacketSizes = Convert.ToInt32(subPacketSize, 2);
            index += 15;
            var subPackets = packet.Substring(index, totalSubPacketSizes);

            var consumed = 0;
            do
            {
              consumed += ProcessPacket(subPackets.Substring(consumed,subPackets.Length-consumed));
              index += consumed;

            } while(consumed != 0 && consumed <= subPackets.Length-consumed);
            return index;
          }
          else if (packet[index] == '1')
          {
            // Get sub packet sizes
            index += 1;
            var numberOfSubPackets = Convert.ToInt32(packet.Substring(index, 11), 2);
            Console.WriteLine($"Subpackets count;{numberOfSubPackets}");
            index += 11;
            var subPackets = packet.Substring(index, packet.Length-index);
            var subPacketCount = 0;

            var subPacketIndex = 0;
            do
            {
              subPacketIndex += ProcessPacket(subPackets.Substring(subPacketIndex, subPackets.Length - subPacketIndex));
              index += subPacketIndex;
              subPacketCount++;

            } while (subPacketCount < numberOfSubPackets);

            return index;
          }
        }

        index++;
      }

      return index;
    }

    public object Part2(string input, bool isTestData)
    {
      var lines = ParseInput(input);

      
      return 0;
    }

    public List<string> ParseInput(string input,bool isTestData= false)
    {
      RawInput = input.Trim();

      if( isTestData )
      {
        return new List<string>(input.Trim().Split("\r\n"));
      }

      return new List<string>(input.Trim().Split("\n"));
    }

    public void Path(HashSet<int> visited, int[][] grid, int x, int y, int totalPath)
    {
      var maxX = grid[0].Length;
      var maxY = grid.Length;

      if (x < 0 || x > grid.Length || y < 0 || y > grid.Length)
      {
        return;
      }

      //var position = (y * maxX) + x;
      //if( visited.Contains(position))
      //{
        //return;
      //}

      //visited.Add(position);
      totalPath += grid[y][x];

      if(BestPathRisk != Int32.MaxValue && totalPath > BestPathRisk )
      {
        return;
      }

      if( x == maxX -1 && y == maxY - 1)
      {
        BestPathRisk = totalPath < BestPathRisk ? totalPath : BestPathRisk;
        return;
      }

      if (x + 1 < maxX)
      {
        Path(visited, grid, x + 1, y, totalPath);
      }

      if (x - 1 > 0)
      {
        //Path(visited, grid, x - 1, y, totalPath);
      }

      if (y + 1 < maxY)
      {
        Path(visited, grid, x, y+1, totalPath);
      }

      if (y - 1 > 0)
      {
        //Path(visited, grid, x, y-1, totalPath);
      }

      return;
    }

    public int Print(int[,] grid, int maxX, int maxY)
    {
      int count = 0;

      var sb = new StringBuilder();
      for (var y = 0; y < maxY; y++)
      {
        for (var x = 0; x < maxX; x++)
        {
          if( grid[y, x] > 0 )
          {
            count++;
            if (maxY < 100 && maxX < 100)
            {
              sb.Append("*");
            }
          }
          else
          {
            if (maxY < 100 && maxX < 100)
            {
              sb.Append(" ");
            }
          }

        }
        sb.Append("\n");
      }

      Console.WriteLine(sb.ToString());

      return count;
    }
  }
}
