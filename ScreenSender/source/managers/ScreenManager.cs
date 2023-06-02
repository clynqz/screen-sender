using System.Drawing.Imaging;

namespace ScreenSender.Managers;

public static class ScreenManager
{
    public static event Action? ScreenshotTaken;

    public static void TakeScreenshot(string filename)
    {
        var screen = Screen.PrimaryScreen!;

        using var bitmap = new Bitmap(screen.Bounds.Width, screen.Bounds.Height, PixelFormat.Format32bppArgb);
        using var graphics = Graphics.FromImage(bitmap);

        graphics.CopyFromScreen(screen.Bounds.X, screen.Bounds.Y, 0, 0, screen.Bounds.Size, CopyPixelOperation.SourceCopy);

        SaveScreenshot(bitmap, ImageFormat.Jpeg, filename);
    }

    private static void SaveScreenshot(Bitmap bitmap, ImageFormat format, string filepath)
    {
        bitmap.Save(filepath, format);

        ScreenshotTaken?.Invoke();
    }
}
