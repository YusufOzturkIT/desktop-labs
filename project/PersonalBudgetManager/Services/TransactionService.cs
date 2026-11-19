using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using PersonalBudgetManager.Models;

namespace PersonalBudgetManager.Services;

public class TransactionService : ITransactionService
{
    private readonly ObservableCollection<TransactionItem> _items = new();

    public TransactionService()
    {
        _items.Add(new TransactionItem { Title = "Monthly Salary", Category = "Income", Amount = 4500, Type = "Income", Date = DateTime.Now.AddDays(-5) });
        _items.Add(new TransactionItem { Title = "Supermarket Grocery", Category = "Groceries", Amount = 250, Type = "Expense", Date = DateTime.Now.AddDays(-3) });
        _items.Add(new TransactionItem { Title = "Electricity Bill", Category = "Utilities", Amount = 120, Type = "Expense", Date = DateTime.Now.AddDays(-1) });
    }

    public IEnumerable<TransactionItem> GetAll() => _items;
    public void Add(TransactionItem item) => _items.Add(item);
    public void Remove(TransactionItem item) => _items.Remove(item);
    public decimal GetTotalIncome() => _items.Where(i => i.Type == "Income").Sum(i => i.Amount);
    public decimal GetTotalExpenses() => _items.Where(i => i.Type == "Expense").Sum(i => i.Amount);
    public decimal GetNetSavings() => GetTotalIncome() - GetTotalExpenses();

    public void ExportToCsv(string filePath)
    {
        using var writer = new StreamWriter(filePath);
        writer.WriteLine("Title,Category,Amount,Type,Date");
        foreach (var item in _items)
        {
            writer.WriteLine($"{item.Title},{item.Category},{item.Amount},{item.Type},{item.Date:yyyy-MM-dd}");
        }
    }

    public int ImportFromCsv(string filePath)
    {
        if (!File.Exists(filePath)) return 0;

        var lines = File.ReadAllLines(filePath);
        int importedCount = 0;

        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split(',');
            if (parts.Length >= 4 && decimal.TryParse(parts[2], out var amount))
            {
                var item = new TransactionItem
                {
                    Title = parts[0],
                    Category = parts[1],
                    Amount = amount,
                    Type = parts[3],
                    Date = parts.Length >= 5 && DateTime.TryParse(parts[4], out var dt) ? dt : DateTime.Now
                };
                Add(item);
                importedCount++;
            }
        }
        return importedCount;
    }
}
