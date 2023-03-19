using System.Runtime.InteropServices;

if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    Console.WriteLine("Here you have Windows World!");
else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    Console.WriteLine("Here you have Linux World!");
else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
    Console.WriteLine("Here you have macOS World!");