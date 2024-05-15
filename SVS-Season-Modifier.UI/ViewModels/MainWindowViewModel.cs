using System.Collections.ObjectModel;
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

    private SaveFile _selected = new(null)
    {
        Name = "",
        CurSeason = "",
        CurSeasonByDay = "",
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

    private string _status = "SVS Season Modifier v0.1.0-beta";
    internal string Status
    {
        get => _status;
        set => this.RaiseAndSetIfChanged(ref _status, value);
    }
}