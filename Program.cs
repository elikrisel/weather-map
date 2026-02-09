namespace weather_map;

class Program
{
    static async Task Main(string[] args)
    {
        while (true)
        {
             ReadAndWriteFiles.ReadAllFiles("test.txt");
             //Console.Write("Lägg till i listan: \t");
             //string text = Console.ReadLine();
             //await ReadAndWriteFiles.WriteAllFiles("list.txt", text);
        }
        
        
    }
}