
namespace weather_map;


public class UI
{
    public static void DrawUI()
    {
        Console.Clear();
        Console.WriteLine("==========================");
        Console.WriteLine(       "Weather Map");
        Console.WriteLine("==========================");

        Console.WriteLine("\n1 Inomhus");
        Console.WriteLine("2) Utomhus");
        Console.WriteLine("3) Exit");
        Console.Write("\nvälj ett alternativ: ");
    }
    
}