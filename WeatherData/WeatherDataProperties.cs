namespace weather_map;


public class WeatherDataProperties
{
    public DateTime DateAndTime { get; set; }
    public double Temperature { get; set; }
    public string Location { get; set; }
    public int Humidity { get; set; }
    
    //TODO: Kalkylera Mögel enligt grafen?
    
    //public double MoldCapacity { get; set; }
}
