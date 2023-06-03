using System.Drawing.Imaging;

namespace ScreenSender.Managers;

public static class FileManager
{
    public static void CleanDirectory(DirectoryInfo dirInfo)
    {
        if (!Directory.Exists(dirInfo.FullName))
        {
            throw new DirectoryNotFoundException();
        }

        foreach (var file in Directory.GetFiles(dirInfo.FullName))
        {
            File.Delete(file);
        }
    }

    public static bool CreateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);

            return true;
        }

        return false;
    }

    public static string GetScreenshotFilename(ImageFormat imageFormat)
    {
        string getImageFormatAsString()
        {
            return imageFormat.ToString().ToLowerInvariant();
        };

        var now = DateTime.Now;

        var extensions = getImageFormatAsString();
        var filename = $"screenshot{now.Day}{now.Month}{now.Year}_{now.Hour}{now.Minute}{now.Second}{now.Millisecond}.{extensions}";

        return filename;
    }
}
