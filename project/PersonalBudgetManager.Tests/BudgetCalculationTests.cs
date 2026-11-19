using PersonalBudgetManager.Models;
using PersonalBudgetManager.Services;
using Xunit;

namespace PersonalBudgetManager.Tests;

public class BudgetCalculationTests
{
    [Fact]
    public void Test_Initial_TotalIncome_Calculates_Correctly()
    {
        var service = new TransactionService();
        Assert.Equal(4500, service.GetTotalIncome());
    }

    [Fact]
    public void Test_Initial_TotalExpenses_Calculates_Correctly()
    {
        var service = new TransactionService();
        Assert.Equal(370, service.GetTotalExpenses());
    }

    [Fact]
    public void Test_Initial_NetSavings_Calculates_Correctly()
    {
        var service = new TransactionService();
        Assert.Equal(4130, service.GetNetSavings());
    }

    [Fact]
    public void Test_AddTransaction_Increases_Count()
    {
        var service = new TransactionService();
        service.Add(new TransactionItem { Title = "Bonus", Amount = 500, Type = "Income" });
        Assert.Equal(5000, service.GetTotalIncome());
    }

    [Fact]
    public void Test_RemoveTransaction_Decreases_Expenses()
    {
        var service = new TransactionService();
        var item = new TransactionItem { Title = "Temporary", Amount = 100, Type = "Expense" };
        service.Add(item);
        service.Remove(item);
        Assert.Equal(370, service.GetTotalExpenses());
    }

    [Fact]
    public void Test_Transaction_Default_Type_Is_Expense()
    {
        var item = new TransactionItem();
        Assert.Equal("Expense", item.Type);
    }

    [Fact]
    public void Test_Transaction_Id_Is_Generated()
    {
        var item = new TransactionItem();
        Assert.NotEqual(System.Guid.Empty, item.Id);
    }

    [Fact]
    public void Test_NetSavings_Can_Be_Negative_When_Expenses_Exceed_Income()
    {
        var service = new TransactionService();
        service.Add(new TransactionItem { Title = "Big Purchase", Amount = 10000, Type = "Expense" });
        Assert.True(service.GetNetSavings() < 0);
    }
}
