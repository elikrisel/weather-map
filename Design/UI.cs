
namespace weather_map;


public class UI
{
    public static void DrawUI()
    {
        Console.Clear();
        string weatherMap = "Väderkartan";
        Console.WriteLine("==========================");
        Console.WriteLine($"{weatherMap,18}");
        Console.WriteLine("==========================");

        Console.WriteLine("1) Inomhus");
        Console.WriteLine("2) Utomhus");
        Console.WriteLine("3) Avsluta");
        Console.Write("\nvälj ett alternativ: ");
    }
    
}