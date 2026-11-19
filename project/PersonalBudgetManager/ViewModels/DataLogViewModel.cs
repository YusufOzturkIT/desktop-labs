using System;
using System.Collections.ObjectModel;
using System.IO;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PersonalBudgetManager.Services;

namespace PersonalBudgetManager.ViewModels;

public partial class DataLogViewModel : ViewModelBase
{
    private readonly ITransactionService _transactionService;
    private FileSystemWatcher? _watcher;

    public ObservableCollection<string> Logs { get; } = new();

    [ObservableProperty]
    private string _watcherStatus = "Watcher Status: Active (Monitoring /Transactions Inbox)";

    public DataLogViewModel(ITransactionService transactionService)
    {
        _transactionService = transactionService;
        Logs.Add("[System] Initialized File Operations & Directory Watcher.");
        Logs.Add("[Log] Pre-loaded transactions from memory.");
        
        SetupDirectoryWatcher();
    }

    private void SetupDirectoryWatcher()
    {
        try
        {
            string watchFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TransactionsInbox");
            if (!Directory.Exists(watchFolder))
            {
                Directory.CreateDirectory(watchFolder);
            }

            _watcher = new FileSystemWatcher(watchFolder, "*.csv")
            {
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
            };

            _watcher.Created += (s, e) =>
            {
                Dispatcher.UIThread.Post(() =>
                {
                    int imported = _transactionService.ImportFromCsv(e.FullPath);
                    Logs.Add($"[Auto-Watcher] Detected new CSV file '{e.Name}'. Automatically ingested {imported} transactions.");
                });
            };

            _watcher.EnableRaisingEvents = true;
            Logs.Add($"[Auto-Watcher] Monitoring folder: {watchFolder}");
        }
        catch (Exception ex)
        {
            Logs.Add($"[Watcher Setup Warning] {ex.Message}");
        }
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
