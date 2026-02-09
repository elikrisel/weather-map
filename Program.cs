using System.Text.RegularExpressions;

namespace weather_map;

class Program
{
    static async Task Main(string[] args)
    {
        // while (true)
        // {
        //      ReadAndWriteFiles.ReadAllFiles("test.txt");
        //      Console.Write("Lägg till i listan: \t");
        //      string text = Console.ReadLine();
        //      await ReadAndWriteFiles.WriteAllFiles("list.txt", text);
        //      string text = "2016-05-31 13:58:30,Inne,24.8,42";
        //      RegexTest.TemperatureTest(text);      
        //      
        //      
        // }


        
         bool isRunning = true;
         while (isRunning)
         {
               UI.DrawUI();
               string input = Console.ReadLine();
              
               switch (input)
               {
                   case "1":
                       Console.WriteLine("Du är inomhus");
                       Console.ReadKey();
                       break;
                   case "2":
                       Console.WriteLine("Du är utomhus");
                       Console.ReadKey();
                       break;
                   case "3":
                       isRunning = !isRunning;
                       break;
                       
               }
                
              
         }
         
         
         
    
    
    }
    
}