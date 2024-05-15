using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SVS_Season_Modifier.UI.Models;
using SVS_Season_Modifier.UI.ViewModels;

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
    
    internal static void FindSaveFiles(ref ObservableCollection<SaveFile> saveFiles, MainWindowViewModel vm)
    {
        XmlDocument reader = new();
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

        // Rider suggested this monstrosity...
        foreach (var newSave in from path in searchPaths from save in Directory.GetDirectories(path) select new SaveFile(vm)
                 {
                     FilePath = save,
                     SaveId = Path.GetFileName(save)
                 })
        {
            var savePath = newSave.FilePath ?? throw new InvalidOperationException();
            var saveId = newSave.SaveId ?? throw new InvalidOperationException();
            
            reader.Load(Path.Combine(savePath, saveId));
            newSave.CurSeason = reader.DocumentElement.SelectSingleNode("currentSeason").InnerText;
            // Do CurSeasonByDay

            reader.Load(Path.Combine(savePath, "SaveGameInfo"));
            newSave.Name = reader.DocumentElement.SelectSingleNode("farmName").InnerText;
            newSave.Farmer = reader.DocumentElement.SelectSingleNode("name").InnerText;

            saveFiles.Add(newSave);
        }
    }
}