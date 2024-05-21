using System.Threading.Tasks;
using ReactiveUI;
using SVS_Season_Modifier.UI.ViewModels;

namespace SVS_Season_Modifier.UI.Models;

internal class SaveFile(MainWindowViewModel? vm)
{
    private readonly MainWindowViewModel? _vm = vm;
    internal string? FilePath { get; init; }
    internal string? SaveId { get; init; }
    internal string? Name { get; set; }
    internal string? Farmer { get; set; }
    internal int Money { get; set; }
    internal Season CurSeason { get; set; }

    internal ReactiveCommand<SaveFile, Task> SelectCommand { get; } =
        ReactiveCommand.Create<SaveFile, Task>(SelectFile);

    private static async Task SelectFile(SaveFile saveFile)
    {
        await Task.Run(() => saveFile._vm.Selected = saveFile);
    }
}