using weather_map.Helpers;
using weather_map.Models;
using Enum = weather_map.Enums.Enum;

namespace weather_map
{
    internal class Report
    {
        
        private static string filePath = "../../../Files/report.txt";

        public static void GenerateReport(List<WeatherDataProperties> data)
        {
            var outdoor = Helper.GetStatistics(data, "Ute");
            var indoor = Helper.GetStatistics(data, "Inne");
            
            DateTime? fallDate = GetSeason(outdoor, Enum.Season.Fall);
            DateTime? winterDate = GetSeason(outdoor, Enum.Season.Winter);

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine("====== VÄDERRAPPORT ======\n");
                
                WriteMonthlyReport(sw,"UTOMHUS",outdoor);
                WriteMonthlyReport(sw,"INOMHUS",indoor);
                
                sw.WriteLine("====== HÖST & VINTER 2016 ======");
                sw.WriteLine($"Meteorologisk höst: {(fallDate.HasValue ? fallDate.Value.ToString("yyyy-MM-dd") : "Ej inträffat")}");
                sw.WriteLine($"Meteorologisk vinter: {(winterDate.HasValue ? winterDate.Value.ToString("yyyy-MM-dd") : "Ej inträffat")}");
                
                sw.WriteLine("\n====== MÖGELALGORITM ======");
                sw.WriteLine("Mögelrisk = (fuktighet − 78) / 22 * 100");
                
            }
            
            Helper.PressToContinue("Rapport skapad! \nTryck på valfri tangent för att fortsätta...");

        }

        private static void WriteMonthlyReport(StreamWriter sw, string label, List<Statistics> data)
        {
            var monthlyReport = data
                .GroupBy(d => new { d.Date.Year, d.Date.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    Temp = g.Average(x => x.AverageTemperature),
                    Hum = g.Average(x => x.AverageHumidity),
                    Mold = g.Average(x => x.AverageMold)
                }).OrderBy(x => x.Year).ThenBy(x => x.Month);
            
            sw.WriteLine($"{label} PER MÅNAD: ");
            foreach (var m in monthlyReport)
            {
                sw.WriteLine($"{m.Year}-{m.Month} | Temp:{m.Temp:F1}°C | Fukt:{m.Hum:F1}% | Mögel:{m.Mold:F1}");
            }
            sw.WriteLine();
        }
        
        private static DateTime? GetSeason(List<Statistics> data, Enum.Season season)
        {
            double threshold = (season == Enum.Season.Fall) ? 10.0 : 0.0;
            int consecutiveDays = 0;
            foreach (var d in data)
            {
                bool isCold = (season == Enum.Season.Winter)
                    ? d.AverageTemperature <= threshold
                    : d.AverageTemperature < threshold;
                if(isCold)
                    consecutiveDays++;
                else
                    consecutiveDays = 0;

                if (consecutiveDays == 5)
                    return data[data.IndexOf(d) - 4].Date;
            }

            return null;
        }
    }
}
