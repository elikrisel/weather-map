namespace weather_map;

public class ReadAndWriteFiles
{
    private static string filePath = "../../../Files/";
    
    public static void ReadAllFiles(string path)
    {
        try
        {
            using (StreamReader reader = new StreamReader(filePath + path))
            {
                string line = reader.ReadLine();
                int rowCount = 0;
                while (line != null)
                {
                    Console.WriteLine($"{rowCount} {line}");
                    rowCount++;
                    line = reader.ReadLine();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message + "textfile does not exist.");
        }
        
    }

    public static async Task WriteAllFiles(string path, string text)
    {
        using (StreamWriter writer = new StreamWriter(filePath + path, true))
        {
            
            await writer.WriteLineAsync(text);
        }
    }
}