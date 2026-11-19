using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PersonalBudgetManager.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentView;

    public DashboardViewModel DashboardVM { get; } = new();
    public InventoryViewModel InventoryVM { get; } = new();
    public DataLogViewModel DataLogVM { get; } = new();

    public MainViewModel()
    {
        _currentView = DashboardVM;
    }

    [RelayCommand]
    private void NavigateToDashboard() => CurrentView = DashboardVM;

    [RelayCommand]
    private void NavigateToInventory() => CurrentView = InventoryVM;

    [RelayCommand]
    private void NavigateToDataLog() => CurrentView = DataLogVM;
}
