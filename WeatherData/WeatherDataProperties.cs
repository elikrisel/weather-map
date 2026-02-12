namespace weather_map;


public class WeatherDataProperties
{
    public DateTime DateAndTime { get; set; }
    public double Temperature { get; set; }
    public string Location { get; set; }
    public int Humidity { get; set; }
    
    public double MoldRisk
    {
        get
        {
            if (Temperature <= 0 || Temperature >= 50 || Humidity <= 78)
            {
                return 0;
            }
            double risk = (Humidity - 78) / 22d * 100;

            return (int)Math.Min(risk, 100);
        }
    }
}
