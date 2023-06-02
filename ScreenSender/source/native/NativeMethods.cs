using System.Runtime.InteropServices;

namespace ScreenSender.Native;

public partial class NativeMethods
{
#if DEBUG
    [LibraryImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool AllocConsole();
#endif

    [LibraryImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial int GetAsyncKeyState(int vKey);
}
