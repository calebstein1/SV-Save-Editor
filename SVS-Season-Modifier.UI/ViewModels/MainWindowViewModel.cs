using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ReactiveUI;
using SVS_Season_Modifier.UI.Models;
using SVS_Season_Modifier.UI.Services;

namespace SVS_Season_Modifier.UI.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{
    internal MainWindowViewModel()
    {
        Functions.FindSaveFiles(ref _saveFiles, this);
    }

    internal List<Season> Seasons { get; } =
    [
        Season.Spring,
        Season.Summer,
        Season.Winter,
        Season.Fall
    ];

    private SaveFile _selected = new(null)
    {
        Name = "",
        Money = 0,
        CurSeason = 0,
        Farmer = "",
        FilePath = "",
        SaveId = ""
    };
    internal SaveFile Selected
    {
        get => _selected;
        set => this.RaiseAndSetIfChanged(ref _selected, value);
    }
    
    private ObservableCollection<SaveFile> _saveFiles = [];
    internal ObservableCollection<SaveFile> SaveFiles
    {
        get => _saveFiles;
        set => this.RaiseAndSetIfChanged(ref _saveFiles, value);
    }

    private string _status = "SV Save Editor v0.1.0-beta";
    internal string Status
    {
        get => _status;
        set => this.RaiseAndSetIfChanged(ref _status, value);
    }

    internal void ResetStatus()
    {
        Status = "SV Save Editor v0.1.0-beta";
    }
    
    internal ReactiveCommand<MainWindowViewModel, Task> SaveCommand { get; } =
        ReactiveCommand.Create<MainWindowViewModel, Task>(SaveNewData);

    private static async Task SaveNewData(MainWindowViewModel vm)
    {
        if (vm.Selected.FilePath.Length.Equals(0))
        {
            vm.Status = "You must select a save file first";
        }
        else
        {
            // Modify file here
        }
        await Task.Delay(5000);
        vm.ResetStatus();
    }
}