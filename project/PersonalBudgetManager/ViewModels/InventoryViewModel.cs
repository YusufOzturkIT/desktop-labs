using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PersonalBudgetManager.Models;
using PersonalBudgetManager.Services;

namespace PersonalBudgetManager.ViewModels;

public partial class InventoryViewModel : ViewModelBase
{
    private readonly ITransactionService _transactionService;

    public ObservableCollection<TransactionItem> Items { get; }

    [ObservableProperty]
    private string _newTitle = string.Empty;

    [ObservableProperty]
    private string _newCategory = "Groceries";

    [ObservableProperty]
    private string _newAmountText = string.Empty;

    [ObservableProperty]
    private string _newType = "Expense";

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    public InventoryViewModel(ITransactionService transactionService)
    {
        _transactionService = transactionService;
        Items = new ObservableCollection<TransactionItem>(_transactionService.GetAll());
    }

    [RelayCommand]
    private void AddTransaction()
    {
        ErrorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(NewTitle))
        {
            ErrorMessage = "Title cannot be empty!";
            return;
        }

        if (!decimal.TryParse(NewAmountText, out var amount) || amount <= 0)
        {
            ErrorMessage = "Amount must be a positive number!";
            return;
        }

        var item = new TransactionItem
        {
            Title = NewTitle,
            Category = NewCategory,
            Amount = amount,
            Type = NewType,
            Date = DateTime.Now
        };

        _transactionService.Add(item);
        Items.Add(item);

        NewTitle = string.Empty;
        NewAmountText = string.Empty;
    }

    [RelayCommand]
    private void RemoveTransaction(TransactionItem? item)
    {
        if (item != null)
        {
            _transactionService.Remove(item);
            Items.Remove(item);
        }
    }
}
