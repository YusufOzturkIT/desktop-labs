using System;

namespace PersonalBudgetManager.Models;

public class TransactionItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public string Type { get; set; } = "Expense";
}
