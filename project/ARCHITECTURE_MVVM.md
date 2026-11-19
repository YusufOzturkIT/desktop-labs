# Architecture & MVVM Documentation

**Application:** Personal Budget & Inventory Manager  
**Pattern:** Model-View-ViewModel (MVVM) via `CommunityToolkit.Mvvm`  

## 1. Architectural Layers
The project adheres to strict separation of concerns, divided into four primary modules:

* **Models (`/Models`):** Contains pure domain data structures (e.g., `TransactionItem`). Holds no business or presentation logic.
* **Services (`/Services`):** Implements business logic, calculation engines, file I/O operations (CSV import/export), and background directory monitoring (`FileSystemWatcher`).
* **ViewModels (`/ViewModels`):** Handles presentation logic, exposes state properties using `[ObservableProperty]`, and encapsulates user actions via `[RelayCommand]`.
* **Views (`/Views`):** Pure Avalonia XAML controls bound declaratively to ViewModels. Zero code-behind business logic.

## 2. Navigation Mechanism
Multi-screen navigation is orchestrated using `MainViewModel` and dynamic content resolution:
* `MainViewModel` holds instances of sub-ViewModels (`DashboardViewModel`, `InventoryViewModel`, `DataLogViewModel`).
* `CurrentView` property updates trigger Avalonia `DataTemplate` switching in `MainWindow.axaml`.

## 3. OS Integration & File Operations
* **CSV Import/Export:** Structured stream reading and writing for transaction logging.
* **FileSystemWatcher:** Background thread service monitoring a target directory for automated ingestion of bank statement CSV files.
