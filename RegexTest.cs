using System.Text.RegularExpressions;

namespace weather_map;

//TODO: REMOVE CLASS, THIS IS FOR TESTING PURPOSES
public class RegexTest
{
    public static void TemperatureTest(string text)
    {
        #region Regex Pattern without grouping
        //@"^(\d{4}-\d{2}-\d{2}) (\d{2}:\d{2}:\d{2}),([A-ZÅÄÖa-zåäö]+),([\d.+]+),(\d{2})$";
        #endregion
        #region Regex Pattern with grouping
        //@"^(?<date>\d{4}-\d{2}-\d{2}) 
        // (?<time>\d{2}:\d{2}:\d{2}),
        // (?<condition>[A-ZÅÄÖa-zåäö]+),
        // (?<temperature>[\d.+]+),
        // (?<moldlevel>\d{2})";
        #endregion
        
        
        string pattern =
            @"^(?<date>\d{4}-\d{2}-\d{2}) (?<time>\d{2}:\d{2}:\d{2}),(?<condition>[A-ZÅÄÖa-zåäö]+),(?<temperature>[\d.+]+),(?<moldlevel>\d{2})$"; 
        Regex regex = new Regex(pattern); 
        MatchCollection matches = regex.Matches(text);

        foreach (Match match in matches)
        {
            Console.WriteLine(match.Groups["date"].Value);
        }
    }
}