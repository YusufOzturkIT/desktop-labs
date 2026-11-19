using System;
using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PersonalBudgetManager.Services;

namespace PersonalBudgetManager.ViewModels;

public partial class DataLogViewModel : ViewModelBase
{
    private readonly ITransactionService _transactionService;

    public ObservableCollection<string> Logs { get; } = new();

    [ObservableProperty]
    private string _watcherStatus = "Watcher Status: Active (Monitoring /Transactions)";

    public DataLogViewModel(ITransactionService transactionService)
    {
        _transactionService = transactionService;
        Logs.Add("[System] Initialized File Operations & Directory Watcher.");
        Logs.Add("[Log] Pre-loaded transactions from memory.");
    }

    [RelayCommand]
    private void ExportData()
    {
        try
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "export_transactions.csv");
            _transactionService.ExportToCsv(path);
            Logs.Add($"[Export Success] Exported transactions to '{path}'");
        }
        catch (Exception ex)
        {
            Logs.Add($"[Export Error] {ex.Message}");
        }
    }

    [RelayCommand]
    private void SimulateCsvImport()
    {
        try
        {
            string samplePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sample_import.csv");
            File.WriteAllText(samplePath, "Title,Category,Amount,Type,Date\nGym Membership,Entertainment,45,Expense,2026-11-15\nFreelance Design,Income,600,Income,2026-11-16");
            
            int count = _transactionService.ImportFromCsv(samplePath);
            Logs.Add($"[Import Success] Imported {count} items from sample statement file.");
        }
        catch (Exception ex)
        {
            Logs.Add($"[Import Error] {ex.Message}");
        }
    }
}
