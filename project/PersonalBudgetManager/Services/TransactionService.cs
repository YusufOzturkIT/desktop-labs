using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
}
