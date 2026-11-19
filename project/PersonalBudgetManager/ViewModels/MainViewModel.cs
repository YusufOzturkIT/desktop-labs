using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PersonalBudgetManager.Services;

namespace PersonalBudgetManager.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly ITransactionService _transactionService = new TransactionService();

    [ObservableProperty]
    private ViewModelBase _currentView;

    public DashboardViewModel DashboardVM { get; }
    public InventoryViewModel InventoryVM { get; }
    public DataLogViewModel DataLogVM { get; }

    public MainViewModel()
    {
        DashboardVM = new DashboardViewModel(_transactionService);
        InventoryVM = new InventoryViewModel(_transactionService);
        DataLogVM = new DataLogViewModel(_transactionService);

        _currentView = DashboardVM;
    }

    [RelayCommand]
    private void NavigateToDashboard()
    {
        DashboardVM.RefreshSummary();
        CurrentView = DashboardVM;
    }

    [RelayCommand]
    private void NavigateToInventory() => CurrentView = InventoryVM;

    [RelayCommand]
    private void NavigateToDataLog() => CurrentView = DataLogVM;
}
