# Project Card: Personal Budget & Inventory Manager

**Developer:** Yusuf Öztürk & Partner  
**Instructor:** Krzysztof Osada  
**Course:** Desktop Application Development  

## 1. Topic & Overview
A desktop application built with C# and Avalonia UI designed to help users track personal transactions, manage categorized household inventory, calculate recurring monthly budgets, and analyze spending patterns. The application allows users to import/export transactions via CSV, visualize financial metrics, configure automated file monitoring, and maintain local persistent history.

## 2. Key Features (Mandatory Requirements Fit)
1. **MVVM Architecture:** Strict separation using `CommunityToolkit.Mvvm` with zero business logic in code-behind.
2. **Multi-Screen Navigation:** 
   * **Dashboard View:** Financial summary cards (Total Income, Total Expenses, Net Savings, Budget Limits) and recent activity.
   * **Inventory & Expense Detail View:** Interactive manager for itemized entries with category and price calculators.
   * **Data Management View:** CSV importer/exporter, automated directory watcher configuration, and historical transaction logs.
3. **Data Validation:** Strict validation rules on transaction amounts (> 0), dates, and required category selections.
4. **File Operations:** File picker support for CSV/JSON transaction imports and exporting financial summaries.
5. **OS Integration:** Integrated `FileSystemWatcher` to automatically detect and ingest new bank statement CSV files dropped into a designated inbox folder.
6. **Data Persistence:** Local SQLite database storing transactions, categories, and user preferences across application restarts.
7. **Layered Structure:** Clean architecture dividing `Models`, `Services`, `ViewModels`, and `Views` layers with service interfaces.
8. **Unit Testing:** Comprehensive xUnit test suite (minimum 8 passing tests) covering budget calculation logic and validation rules.

## 3. Rough Sketch of Main Screens
+-----------------------------------------------------------------------+
|  BUDGET & INVENTORY MANAGER        [Dashboard]  [Entries]  [Data/Log] |
+-----------------------------------------------------------------------+
|  SUMMARY: Total Income: $4,500 | Expenses: $2,100 | Savings: $2,400    |
|  ALERTS: Grocery category reached 90% of monthly limit!               |
|                                                                       |
|  [ Import CSV ]  [ Set Auto-Folder Watcher ]                          |
|                                                                       |
|  Date       | Item Name       | Category    | Amount    | Status      |
|  -----------+-----------------+-------------+-----------+-------------+
|  2026-11-01 | Supermarket     | Groceries   | $145.20   | Completed   |
|  2026-11-03 | Electricity Bill| Utilities   | $85.00    | Completed   |
+-----------------------------------------------------------------------+
|  STATUS: System Ready | Monitored Folder: /Users/apple/Transactions   |
+-----------------------------------------------------------------------+


## 4. Division of Work
* **Yusuf Öztürk:** Domain Models (`Transaction`, `CategoryBudget`), Financial Calculation Engine, SQLite Repository Service, Layered Architecture Interfaces, and xUnit Test Suite.
* **Partner Name:** Avalonia Views (XAML layout & styling), Navigation Service, `FileSystemWatcher` Service, System Notifications, and Data Validation Rules.
