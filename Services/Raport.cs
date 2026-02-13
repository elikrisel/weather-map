using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weather_map
{
    internal class Raport
    {
        
        private static string filePath = "../../../Files/report.txt";

        public static void GenerateReport(List<WeatherDataProperties> data)
        {
            var outdoor = data.Where(x => x.Location == "Ute").ToList();
            var indoor = data.Where(x => x.Location == "Inne").ToList();

            var monthlyOutdoor = outdoor
                .GroupBy(x => new { x.DateAndTime.Year, x.DateAndTime.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    AvgTemp = g.Average(x => x.Temperature),
                    AvgHumidity = g.Average(x => x.Humidity),
                    AvgMold = g.Average(x => x.MoldRisk)
                })
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToList();

            var monthlyIndoor = indoor
                .GroupBy(x => new { x.DateAndTime.Year, x.DateAndTime.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    AvgTemp = g.Average(x => x.Temperature),
                    AvgHumidity = g.Average(x => x.Humidity),
                    AvgMold = g.Average(x => x.MoldRisk)
                })
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToList();

            DateTime? fallDate = GetFall(outdoor);
            DateTime? winterDate = GetWinter(outdoor);


            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine("====== VÄDERRAPPORT ======\n");

                sw.WriteLine("UTOMHUS PER MÅNAD:");
                foreach (var m in monthlyOutdoor)
                {
                    sw.WriteLine($"{m.Year}-{m.Month} | Temp:{m.AvgTemp}°C | Fukt:{m.AvgHumidity}% | Mögel:{m.AvgMold}");
                }

                sw.WriteLine("\nINOMHUS PER MÅNAD:");
                foreach (var m in monthlyIndoor)
                {
                    sw.WriteLine($"{m.Year}-{m.Month} | Temp:{m.AvgTemp}°C | Fukt:{m.AvgHumidity}% | Mögel:{m.AvgMold}");
                }

                sw.WriteLine("\n====== HÖST & VINTER 2016 ======");
                sw.WriteLine($"Meteorologisk höst: {(fallDate.HasValue ? fallDate.Value.ToString("yyyy-MM-dd") : "Ej inträffat")}");
                sw.WriteLine($"Meteorologisk vinter: {(winterDate.HasValue ? winterDate.Value.ToString("yyyy-MM-dd") : "Ej inträffat")}");

                sw.WriteLine("\n====== MÖGELALGORITM ======");
                sw.WriteLine("Mögelrisk = 0.5 * f(fukt) + 0.5 * f(temp)");
            }

            Console.WriteLine("Rapport skapad!");
            Console.ReadKey();
        }

        private static DateTime? GetFall(List<WeatherDataProperties> data)
        {
            var dailyAvg = data
                .GroupBy(x => x.DateAndTime.Date)
                .Select(g => new { Date = g.Key, AvgTemp = g.Average(x => x.Temperature) })
                .OrderBy(x => x.Date)
                .ToList();

            int consecutive = 0;
            for (int i = 0; i < dailyAvg.Count; i++)
            {
                if (dailyAvg[i].AvgTemp < 10.0 && dailyAvg[i].Date >= new DateTime(2016, 8, 1))
                    consecutive++;
                else
                    consecutive = 0;

                if (consecutive == 5)
                    return dailyAvg[i - 4].Date;
            }

            //om höst inte inträffar, returnera närmast
            var closest = dailyAvg.Where(d => d.Date >= new DateTime(2016, 8, 1))
                                  .OrderBy(d => Math.Abs(d.AvgTemp - 10))
                                  .FirstOrDefault();
            return closest?.Date;
        }
        private static DateTime? GetWinter(List<WeatherDataProperties> data)
        {
            var dailyAvg = data
                .GroupBy(x => x.DateAndTime.Date)
                .Select(g => new { Date = g.Key, AvgTemp = g.Average(x => x.Temperature) })
                .OrderBy(x => x.Date)
                .ToList();

            int consecutive = 0;
            for (int i = 0; i < dailyAvg.Count; i++)
            {
                if (dailyAvg[i].AvgTemp <= 0)
                    consecutive++;
                else
                    consecutive = 0;

                if (consecutive == 5)
                    return dailyAvg[i - 4].Date;
            }

            //om vinter inte inträffar, returnera närmast
            var closest = dailyAvg.OrderBy(d => Math.Abs(d.AvgTemp)).FirstOrDefault();
            return closest?.Date;
        }
    }
}
