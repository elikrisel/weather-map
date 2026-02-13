namespace weather_map.Models;

public class Statistics
{
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public double AverageTemperature { get; set; }
    public double AverageHumidity { get; set; }
    public double AverageMold { get; set; }
}