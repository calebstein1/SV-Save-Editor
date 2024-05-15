using System;
using System.Threading.Tasks;
using ReactiveUI;
using SVS_Season_Modifier.UI.ViewModels;

namespace SVS_Season_Modifier.UI.Models;

internal class SaveFile(MainWindowViewModel? vm)
{
    internal MainWindowViewModel? Vm = vm;
    internal string? FilePath { get; set; }
    internal string? SaveId { get; set; }
    internal string? Name { get; set; }
    internal string? Farmer { get; set; }
    internal Season CurSeason { get; set; }
    internal Season CurSeasonByDay { get; set; }

    internal ReactiveCommand<SaveFile, Task> SelectCommand { get; } =
        ReactiveCommand.Create<SaveFile, Task>(SelectFile);

    private static async Task SelectFile(SaveFile saveFile)
    {
        await Task.Run(() => saveFile.Vm.Selected = saveFile);
    }
}