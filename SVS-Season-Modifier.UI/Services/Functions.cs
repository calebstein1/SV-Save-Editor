using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using SVS_Season_Modifier.UI.Models;

namespace SVS_Season_Modifier.UI.Services;

internal enum OperatingSystem
{
    Windows,
    Mac,
    Linux
}

internal static class Functions
{
    private static void GetOperatingSystem(ref OperatingSystem os)
    {
        os = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? OperatingSystem.Windows :
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? OperatingSystem.Mac :
            OperatingSystem.Linux;
    }
    
    internal static void FindSaveFiles(ref ObservableCollection<SaveFile> saveFiles)
    {
        OperatingSystem os = 0;
        List<string> searchPaths = [];
        
        GetOperatingSystem(ref os);

        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (os)
        {
            case OperatingSystem.Windows:
                searchPaths.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StardewValley", "Saves"));
                break;
            case OperatingSystem.Mac:
                searchPaths.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config", "StardewValley", "Saves"));
                break;
            case OperatingSystem.Linux:
                searchPaths.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config", "StardewValley", "Saves"));
                searchPaths.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".var", "app", "com.valvesoftware.Steam", ".config", "StardewValley", "Saves"));
                searchPaths.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "snap", "steam", "common", ".config", "StardewValley", "Saves"));
                break;
        }
        
    }
}