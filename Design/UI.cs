
using weather_map.Helpers;

namespace weather_map;


public class UI
{
    public static void DrawUI()
    {
        Console.Clear();
        string titleName = "Väderkartan";
        Console.WriteLine(Helper.PrintXNumberOfLines(26));
        Console.WriteLine($"{titleName,18}");
        Console.WriteLine(Helper.PrintXNumberOfLines(26));

        Console.WriteLine("1) Inne");
        Console.WriteLine("2) Ute");
        Console.WriteLine("3) Generera Rapport");
        Console.WriteLine("4) Avsluta");
        
        Console.Write("\nvälj ett alternativ: ");
    }

    public static void DrawSubMenu(string location)
    {
        Console.Clear();
        Console.WriteLine($"===Meny för {location}===");
        
        Console.WriteLine("1) Leta efter medeltemperatur för valt datum ");
        Console.WriteLine("2) Sortering av varmast till kallaste dagen enligt medeltemperatur per dag");
        Console.WriteLine("3) Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag");
        Console.WriteLine("4) Sortering av minst till störst risk av mögel");
        
        //Lägger på ytterligare alternativ när användaren väljer utomhus
        if (location == "Ute")
        {
            Console.WriteLine("5) Datum för meteorologisk Höst");
            Console.WriteLine("6) Datum för meteologisk Vinter");    
        }
        
        Console.WriteLine("\n0) Tillbaka");
        Console.WriteLine("\n Välj ett alternativ: ");

    }
    
    
}