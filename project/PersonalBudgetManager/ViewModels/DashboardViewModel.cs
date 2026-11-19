using CommunityToolkit.Mvvm.ComponentModel;
using PersonalBudgetManager.Services;

namespace PersonalBudgetManager.ViewModels;

public partial class DashboardViewModel : ViewModelBase
{
    private readonly ITransactionService _transactionService;

    [ObservableProperty]
    private decimal _totalIncome;

    [ObservableProperty]
    private decimal _totalExpenses;

    [ObservableProperty]
    private decimal _netSavings;

    public DashboardViewModel(ITransactionService transactionService)
    {
        _transactionService = transactionService;
        RefreshSummary();
    }

    public void RefreshSummary()
    {
        TotalIncome = _transactionService.GetTotalIncome();
        TotalExpenses = _transactionService.GetTotalExpenses();
        NetSavings = _transactionService.GetNetSavings();
    }
}
