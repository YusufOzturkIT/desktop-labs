# Project Card: 3M Demand Planner & Safety Stock Analyzer

**Developer:** Yusuf Öztürk & Partner  
**Instructor:** Krzysztof Osada  
**Course:** Desktop Application Development  

## 1. Topic & Overview
A specialized desktop application tailored for demand planning, safety stock calculation, and inventory risk analytics. The tool enables planners to import historical sales and demand data from ERP/CSV exports, calculate dynamic reorder points, visualize safety stock requirements based on targeted service levels, monitor forecast errors (MAD, RMSE), and automate input file processing.

## 2. Key Features (Mandatory Requirements Fit)
1. **MVVM Architecture:** Strict separation using `CommunityToolkit.Mvvm` with zero UI code in code-behind.
2. **Multi-Screen Navigation:** 
   * **Dashboard View:** Executive summary of inventory metrics, critical stock alerts, and forecast accuracy KPIs.
   * **Safety Stock & SKU Detail View:** Interactive calculator for individual SKUs (Lead Time, Service Level %, Unit Cost).
   * **Data Management View:** ERP export importer, file watcher configuration, and historical calculation logs.
3. **Data Validation:** Strict input rules on lead times (> 0), service levels (50% - 99.9%), and unit costs.
4. **File Operations:** File picker for CSV/JSON imports and export of calculated parameters.
5. **OS Integration:** Integrated `FileSystemWatcher` to automatically detect and ingest new ERP export CSV files dropped into a monitored input folder.
6. **Data Persistence:** Local SQLite database storing SKU master records, safety stock runs, and configuration settings.
7. **Layered Structure:** Distinct `Models`, `Services`, `ViewModels`, and `Views` layers with service interfaces.
8. **Unit Testing:** Comprehensive xUnit suite (minimum 8 passing tests) covering inventory formulas and forecasting accuracy metrics.

## 3. Rough Sketch of Main Screens
cd /Users/apple/desktop-labs
mkdir -p project

cat << 'EOF' > project/README.md
# Project Card: 3M Demand Planner & Safety Stock Analyzer

**Developer:** Yusuf Öztürk & Partner  
**Instructor:** Krzysztof Osada  
**Course:** Desktop Application Development  

## 1. Topic & Overview
A specialized desktop application tailored for demand planning, safety stock calculation, and inventory risk analytics. The tool enables planners to import historical sales and demand data from ERP/CSV exports, calculate dynamic reorder points, visualize safety stock requirements based on targeted service levels, monitor forecast errors (MAD, RMSE), and automate input file processing.

## 2. Key Features (Mandatory Requirements Fit)
1. **MVVM Architecture:** Strict separation using `CommunityToolkit.Mvvm` with zero UI code in code-behind.
2. **Multi-Screen Navigation:** 
   * **Dashboard View:** Executive summary of inventory metrics, critical stock alerts, and forecast accuracy KPIs.
   * **Safety Stock & SKU Detail View:** Interactive calculator for individual SKUs (Lead Time, Service Level %, Unit Cost).
   * **Data Management View:** ERP export importer, file watcher configuration, and historical calculation logs.
3. **Data Validation:** Strict input rules on lead times (> 0), service levels (50% - 99.9%), and unit costs.
4. **File Operations:** File picker for CSV/JSON imports and export of calculated parameters.
5. **OS Integration:** Integrated `FileSystemWatcher` to automatically detect and ingest new ERP export CSV files dropped into a monitored input folder.
6. **Data Persistence:** Local SQLite database storing SKU master records, safety stock runs, and configuration settings.
7. **Layered Structure:** Distinct `Models`, `Services`, `ViewModels`, and `Views` layers with service interfaces.
8. **Unit Testing:** Comprehensive xUnit suite (minimum 8 passing tests) covering inventory formulas and forecasting accuracy metrics.

## 3. Rough Sketch of Main Screens
+-----------------------------------------------------------------------+
|  3M DEMAND PLANNER                [Dashboard]  [SKU Calc]  [Data/Log] |
+-----------------------------------------------------------------------+
|  STATISTICS: Total SKUs: 1,420 | Avg Service Level: 98.2%             |
|  ALERTS: 3 SKUs Below Reorder Point!                                  |
|                                                                       |
|  [ Import CSV ]  [ Set Auto-Folder Watcher ]                          |
|                                                                       |
|  SKU Code | Name          | LeadTime | Target SL% | Safety Stock | ROP|
|  ---------+---------------+----------+------------+--------------+----+
|  3M-8810  | Thermal Tape  | 14 days  | 99.0%      | 450 units    | 1.2k|
|  3M-9080  | Double Tape   | 7 days   | 95.0%      | 420 units    | 400|
+-----------------------------------------------------------------------+
|  STATUS: System Ready | Monitored Folder: /Users/3M/ERP_Exports       |
+-----------------------------------------------------------------------+


## 4. Division of Work
* **Yusuf Öztürk:** Domain Models (`SKU`, `ForecastRun`), Safety Stock & Forecasting Calculation Engine, SQLite Repository Service, Layered Architecture Interfaces, and xUnit Test Suite.
* **Partner Name:** Avalonia Views (XAML layout & styling), Navigation Service, `FileSystemWatcher` Service, System Tray Alerts, and Data Validation Rules.
