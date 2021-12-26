using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode._2021
{
  class Advent2021_19 : IAdventProblem
  {
    public string RawInput
    {
      get;
      set;
    }

    public object Part1(string input, bool isTestData)
    {
      var sensorsInput = ParseInput(input, isTestData);
     
      var sensors = new List<Sensor>();

      foreach( var sensorInput in sensorsInput)
      {
        List<string> sensorData = null;
        if( isTestData )
        {
          sensorData = sensorInput.Split("\r\n").ToList();
        }
        else
        {
          sensorData = sensorInput.Split("\n").ToList();
        }


        var sensor = new Sensor();
        for (int i = 1; i < sensorData.Count; i++)
        {
          var coords = sensorData[i].Split(",").Select( c => Convert.ToInt32(c.Trim())).ToList();
          sensor.RawBeacons.Add(new Vector3(coords[0],coords[1], coords[2]));
        }

        sensor.ComputeInternalDistances();
        sensors.Add(sensor);
      }

      // Pin the first sensor as the source of truth
      var correctSensors = new List<Sensor>();
      correctSensors.Add(sensors[0]);
      sensors.RemoveAt(0);

      while( sensors.Count > 0 )
      {
        // Find the best match
        var index = FindIndexOfBestMatch(correctSensors[0], sensors);

        // Transform best match to line up with the correct sensor
        var transformedSensor = Transform(correctSensors[0], sensors[index]);
        correctSensors.Add(transformedSensor);

        foreach ( var beacon in transformedSensor.RawBeacons)
        {
          try
          {
            sensors[0].RawBeacons.Add(beacon);
          }
          catch { }
        }

        correctSensors.Add(transformedSensor);
      }

      return 0;
    }

    public int FindIndexOfBestMatch(Sensor correct, List<Sensor> pool)
    {
      var bestMatch = 0;
      var indexOfBestMatch = -1;

      for( int i = 0; i < pool.Count; i++)
      {
        var matches = 0;
        foreach( var d in correct.InternalDistances.Keys )
        {
          foreach( var pd in pool[i].InternalDistances.Keys )
          {
            if( Math.Abs( d - pd ) < 0.0001 )
            {
              matches++;
            }
          }
        }

        if (matches > bestMatch)
        {
          bestMatch = matches;
          indexOfBestMatch = i;
        }
      }

      return indexOfBestMatch;
    }

    public Sensor Transform(Sensor correct, Sensor bestMatch)
    {
      var transformedSensor = new Sensor();

      return null;

    }

    public object Part2(string input, bool isTestData)
    {
      return 0;
    }

    public List<string> ParseInput(string input, bool isTestData)
    {
      RawInput = input.Trim();

      if (isTestData)
      {
        return new List<string>(input.Trim().Split("\r\n\r\n"));
      }

      return new List<string>(input.Trim().Split("\n\n"));
    }
  }

  public class Sensor
  {
    #region Properties

    public Vector3 Position
    {
      get;
      set;
    }

    public List<Vector3> RawBeacons
    {
      get;
      set;
    } = new List<Vector3>();

    public Dictionary<double, Tuple<Vector3, Vector3>> InternalDistances
    {
      get;
      set;
    } = new Dictionary<double, Tuple<Vector3, Vector3>>();

    #endregion
    public Sensor()
    {

    }

    public void ComputeInternalDistances()
    {
      var combinations = RawBeacons.SelectMany((x, i) => RawBeacons.Skip(i + 1), (x, y) => Tuple.Create(x, y));
      foreach( var combination in combinations )
      {
        var distance = Vector3.Distance(combination.Item1, combination.Item2);
        try
        {
          InternalDistances.Add(distance, combination);
        }
        catch
        {

        }
      }
    }
  }
}
