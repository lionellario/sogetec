namespace Sogetec.Chassis.Utilities;

public static class FileExtensions
{
    public static bool IsValidJson(string filePath)
    {
        try
        {
            string content = File.ReadAllText(filePath);
            using (JsonDocument doc = JsonDocument.Parse(content))
            {
                return true;
            }
        }
        catch (JsonException)
        {
            return false;
        }
    }

    public static bool IsValidCsv(string filePath, char delimiter = ',')
    {
        try
        {
            var lines = File.ReadLines(filePath).Take(5).ToList(); // Check first 5 lines
            if (lines.Count == 0) return false;

            int columnCount = lines[0].Split(delimiter).Length;

            // Ensure all sampled lines have the same number of columns
            return lines.All(line => line.Split(delimiter).Length == columnCount && columnCount > 1);
        }
        catch
        {
            return false;
        }
    }
}
