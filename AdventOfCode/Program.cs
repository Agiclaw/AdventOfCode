using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
  class Program
  {
    static int Main(string[] args)
    {
      var yearOption = new Option<int>("--year");
      yearOption.SetDefaultValue(DateTime.Now.Year);
     
      var dayOption = new Option<int>("--day");
      dayOption.SetDefaultValue(DateTime.Now.Day);

      var rootCommand = new RootCommand("Advent of Code");
      rootCommand.AddOption(yearOption);
      rootCommand.AddOption(dayOption);

      rootCommand.Handler = CommandHandler.Create<int, int>((year, day) =>
      {
        var name = $"{ year }_{ day.ToString("00")}";
        var typeName = $"AdventOfCode._{year}.Advent{name}";
        Type type = Type.GetType(typeName);
        if (type == null)
        {
          throw new Exception($"{typeName} not found.");
        }

        var advent = Activator.CreateInstance(type) as IAdventProblem;

        // GetOrCache Input
        string data = GetOrCacheAdventInput(year, day, true);
        Console.WriteLine($"Advent: {name} Part 1 (test):\t{(data == string.Empty ? "No input data available." : advent.Part1(data,true))}");
        Console.WriteLine($"Advent: {name} Part 2 (test):\t{(data == string.Empty ? "No input data available." : advent.Part2(data,true))}");

        data = GetOrCacheAdventInput(year, day, false);
        Console.WriteLine($"Advent: {name} Part 1 (real):\t{(data == string.Empty ? "No input data available." : advent.Part1(data))}");
        Console.WriteLine($"Advent: {name} Part 2 (real):\t{(data == string.Empty ? "No input data available." : advent.Part2(data))}");
      });

      return rootCommand.Invoke(args);
    }

    public static string GetOrCacheAdventInput(int year, int day, bool fetchTestData)
    {
      var userDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AventOfCode");
      var cacheDirectory = Path.Combine(userDirectory, $"{year}", $"{day}");
      Directory.CreateDirectory(cacheDirectory);

      var filePath = Path.Combine(cacheDirectory, $"{(fetchTestData == true ? "test" : "real")}Data");
      if (File.Exists(filePath))
      {
        return File.ReadAllText(filePath);
      }

      // If the test data doesn't exist, write the stub file and return
      if( !File.Exists(filePath) && fetchTestData)
      {
        File.WriteAllText(filePath, string.Empty);
        return string.Empty;
      }

      try
      {
        var sessionToken = File.ReadAllText(Path.Combine(userDirectory, $"session.token"));

        var site = $"https://adventofcode.com/{year}/day/{day}{(fetchTestData == true ? "" : "/input")}";
        HttpWebRequest request = WebRequest.Create(site) as HttpWebRequest;
        request.Headers.Add(HttpRequestHeader.Cookie, $"session={sessionToken}");

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        WebHeaderCollection header = response.Headers;

        var encoding = ASCIIEncoding.ASCII;
        string responseText = string.Empty;
        using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
        {
          responseText = reader.ReadToEnd();
        }

        File.WriteAllText(filePath, responseText);

        return responseText;
      }
      catch( Exception exception )
      {

      }

      return string.Empty;
    }

    public struct PasswordSet
    {
      public int min;
      public int max;
      public char character;
      public string password;

      public PasswordSet(int Min, int Max, char Character, string Password)
      {
        min = Min;
        max = Max;
        character = Character;
        password = Password;
      }
    }

    public static int Advent02(string inputFile, bool checkInstances)
    {
      var text = File.ReadAllText(inputFile);
      var inputStrings = text.Split("\r\n");

      var passwordSets = new List<PasswordSet>();

      foreach (var inputString in inputStrings)
      {
        var stringComponents = inputString.Split(" ");
        var minMax = stringComponents[0].Split("-");

        passwordSets.Add(new PasswordSet(Convert.ToInt32(minMax[0]), Convert.ToInt32(minMax[1]), Convert.ToChar(stringComponents[1].Substring(0, 1)), stringComponents[2]));
      }

      int validPasswords = 0;

      foreach (var passwordSet in passwordSets)
      {
        if (checkInstances)
        {
          int count = passwordSet.password.Split(passwordSet.character).Length - 1;
          if (count >= passwordSet.min && count <= passwordSet.max)
          {
            validPasswords++;
          }
        }
        else
        {
          if (passwordSet.password[passwordSet.min - 1] == passwordSet.character ^ passwordSet.password[passwordSet.max - 1] == passwordSet.character)
          {
            validPasswords++;
          }
        }
      }

      return validPasswords;
    }

    public static long Advent03(string inputFile, int stepX, int stepY, bool firstRound)
    {
      if( !firstRound )
      {
        long value = (long)
          Advent03(inputFile, 1, 1, true) *
          Advent03(inputFile, 3, 1, true) *
          Advent03(inputFile, 5, 1, true) *
          Advent03(inputFile, 7, 1, true) *
          Advent03(inputFile, 1, 2, true);

          return value;
      }
      
      var text = File.ReadAllText(inputFile);
      var inputStrings = text.Split("\r\n");

      int xCount = 1;
      int collisions = 0;

      for( int yCount = stepY; yCount < inputStrings.Length; yCount += stepY )
      {
        xCount += stepX;

        var row = inputStrings[yCount];

        for( int i = 0; i < stepX * inputStrings.Length; i++ )
        {
          row += inputStrings[yCount];
        }

        if (row[xCount-1] == '#')
        {
          collisions++;
        }
      }

      return collisions;
    }

    public static int Advent04(string inputFile, bool firstRound)
    {
      var text = File.ReadAllText(inputFile);
      var inputStrings = text.Split("\r\n\r\n");

      int valid = 0;
      int valid2 = 0;

      foreach( var passport in inputStrings )
      {
        var regex = new Regex(@"(\w*:[#]?\w*)");
        var modifiedPassport = new string(passport);
        modifiedPassport = modifiedPassport.Replace("\r\n", " ");

        var fields = modifiedPassport.Split(" ");

        bool byr = false;
        bool iyr = false;
        bool eyr = false;
        bool hgt = false;
        bool hcl = false;
        bool ecl = false;
        bool pid = false;

        var dictionary = new Dictionary<string, string>();

        foreach ( var field in fields)
        {
          var fieldHeader = field.Split(":");

          dictionary.Add(fieldHeader[0], fieldHeader[1]);

          switch (fieldHeader[0])
          {
            case "byr":
              byr = true;
              break;

            case "iyr":
              iyr = true;
              break;

            case "eyr":
              eyr = true;
              break;

            case "hgt":
              hgt = true;
              break;

            case "hcl":
              hcl = true;
              break;

            case "ecl":
              ecl = true;
              break;

            case "pid":
              pid = true;
              break;
          }
        }

        if ( byr && iyr && eyr && hgt && hcl && ecl && pid)
        {
          valid++;

          // verification
          try
          {
            var byrVal = Convert.ToInt32(dictionary["byr"]);
            if ( !(byrVal >=1920 && byrVal <= 2002) )
            {
              continue;
            }

            var iyrVal = Convert.ToInt32(dictionary["iyr"]);
            if (!(iyrVal >= 2010 && iyrVal <= 2020))
            {
              continue;
            }

            var eVal = Convert.ToInt32(dictionary["eyr"]);
            if (!(eVal >= 2020 && eVal <= 2030))
            {
              continue;
            }

            //hgt
            var nums = new Regex(@"([0-9]*)");
            if (dictionary["hgt"].EndsWith("cm"))
            {

              var match = nums.Match(dictionary["hgt"]);
              var value = Convert.ToInt32(match.Value);
              if (!(value >= 150 && value <= 193 ))
              {
                continue;
              }
            }
            else if(dictionary["hgt"].EndsWith("in"))
            {
              var match = nums.Match(dictionary["hgt"]);
              var value = Convert.ToInt32(match.Value);
              if (!(value >= 59 && value <= 76))
              {
                continue;
              }
            }
            else
            {
              continue;
            }


            var hairRegex = new Regex(@"#[a-f0-9]{6}");
            if( !hairRegex.IsMatch(dictionary["hcl"]))
            {
              continue;
            }

            if( !( dictionary["ecl"] == "amb" ||
                   dictionary["ecl"] == "blu" ||
                   dictionary["ecl"] == "brn" ||
                   dictionary["ecl"] == "gry" ||
                   dictionary["ecl"] == "grn" ||
                   dictionary["ecl"] == "hzl" ||
                   dictionary["ecl"] == "oth" ) )
            {
              continue;
            }

            var ssnRegex = new Regex(@"[0-9]{9}");
            if (!ssnRegex.IsMatch(dictionary["pid"]))
            {
              continue;
            }

          }
          catch (Exception exception)
          {
            continue;
          }

          valid2++;
        }
      }

      if(!firstRound)
      {
        return valid2;
      }

      return valid;
    }

    public static int Advent05(string inputFile, bool firstRound)
    {
      var inputString = File.ReadAllText(inputFile);

      var seatList = new List<int>();
      for (int i = 0; i < 1000; i++)
      {
        seatList.Add(i);
      }

      var seats = inputString.Split("\r\n");
      var highest = 0;
      foreach (var seat in seats)
      {
        var row = seat.Substring(0, 7).Replace('B', '1').Replace('F', '0');
        var intRow = Convert.ToInt32(row, 2);

        var seatID = seat.Substring(7, 3).Replace('R', '1').Replace('L', '0');
        var seatIDInt = Convert.ToInt32(seatID, 2);

        var cal = (intRow * 8) + seatIDInt;
        seatList.Remove(cal);
        if (highest < cal)
        {
          highest = cal;
        }
      }

      foreach (var availSeat in seatList.OrderBy( s => s))
      {
        Console.WriteLine(availSeat);
      }

      if (firstRound)
      {
        return highest;
      }

      return 0;
    }

    public static int Advent06(string inputFile, bool firstRound)
    {
      var inputString = File.ReadAllText(inputFile);

      var groups = inputString.Split("\r\n\r\n");

      var total = 0;
      foreach( var group in groups )
      {

        var characters = new List<char>();
        var answers = group.Split("\r\n");
        var intersect = "abcdefghijklmnopqrstuvwxyz";

        foreach (var answer in answers )
        {
          foreach (var character in answer)
          {
            characters.Add(character);
          }

          if (!firstRound)
          {
            intersect = intersect.Intersect(answer).ToList().ToString();
            continue;
          }
        }

        if (firstRound)
        {
          total += characters.Distinct().Count();
        }
        else
        {
          total += intersect.Length;
        }
      }

      return total;
    }
  }
}
