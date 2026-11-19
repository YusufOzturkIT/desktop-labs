using System.Collections.Generic;
using PersonalBudgetManager.Models;

namespace PersonalBudgetManager.Services;

public interface ITransactionService
{
    IEnumerable<TransactionItem> GetAll();
    void Add(TransactionItem item);
    void Remove(TransactionItem item);
    decimal GetTotalIncome();
    decimal GetTotalExpenses();
    decimal GetNetSavings();
    void ExportToCsv(string filePath);
    int ImportFromCsv(string filePath);
}
