using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BmiMvvm.Models;

namespace BmiMvvm.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    // ==========================================
    // TASK 1 & TASK 3: BMI CALCULATOR (MVVM)
    // ==========================================

    public string[] WeightUnits { get; } = ["kg", "lb"];
    public string[] HeightUnits { get; } = ["cm", "in"];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BmiResultText))]
    [NotifyPropertyChangedFor(nameof(CategoryColor))]
    [NotifyCanExecuteChangedFor(nameof(SaveToHistoryCommand))]
    public partial double Weight { get; set; } = 70;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BmiResultText))]
    [NotifyPropertyChangedFor(nameof(CategoryColor))]
    [NotifyCanExecuteChangedFor(nameof(SaveToHistoryCommand))]
    public partial double Height { get; set; } = 175;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BmiResultText))]
    [NotifyPropertyChangedFor(nameof(CategoryColor))]
    [NotifyCanExecuteChangedFor(nameof(SaveToHistoryCommand))]
    public partial string SelectedWeightUnit { get; set; } = "kg";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BmiResultText))]
    [NotifyPropertyChangedFor(nameof(CategoryColor))]
    [NotifyCanExecuteChangedFor(nameof(SaveToHistoryCommand))]
    public partial string SelectedHeightUnit { get; set; } = "cm";

    // Hesaplanan Alanlar
    public string BmiResultText
    {
        get
        {
            var res = BmiCalculator.Calculate(Weight, SelectedWeightUnit, Height, SelectedHeightUnit);
            return res == null 
                ? "Please enter valid positive values." 
                : $"BMI: {res.Bmi:F1} ({res.Category})";
        }
    }

    public string CategoryColor
    {
        get
        {
            var res = BmiCalculator.Calculate(Weight, SelectedWeightUnit, Height, SelectedHeightUnit);
            return res?.ColorHex ?? "#000000";
        }
    }

    // Task 3: History (+1 Pt)
    public ObservableCollection<string> History { get; } = new();

    private bool CanSaveToHistory()
    {
        return BmiCalculator.Calculate(Weight, SelectedWeightUnit, Height, SelectedHeightUnit) != null;
    }

    [RelayCommand(CanExecute = nameof(CanSaveToHistory))]
    private void SaveToHistory()
    {
        var res = BmiCalculator.Calculate(Weight, SelectedWeightUnit, Height, SelectedHeightUnit);
        if (res != null)
        {
            string entry = $"{DateTime.Now:HH:mm} - {Weight} {SelectedWeightUnit}, {Height} {SelectedHeightUnit} -> BMI: {res.Bmi:F1}";
            History.Insert(0, entry);
        }
    }


    // ==========================================
    // TASK 2: TEMPERATURE CONVERTER
    // ==========================================

    private bool _isUpdatingTemp = false;

    [ObservableProperty]
    public partial double Celsius { get; set; } = 0;

    [ObservableProperty]
    public partial double Fahrenheit { get; set; } = 32;

    [ObservableProperty]
    public partial double Kelvin { get; set; } = 273.15;

    [ObservableProperty]
    public partial string TempWarning { get; set; } = string.Empty;

    partial void OnCelsiusChanged(double value)
    {
        if (_isUpdatingTemp) return;

        if (value < -273.15)
        {
            TempWarning = "Warning: Temperature is below absolute zero (-273.15 °C)!";
            return;
        }

        TempWarning = string.Empty;
        _isUpdatingTemp = true;
        Fahrenheit = Math.Round((value * 9 / 5) + 32, 2);
        Kelvin = Math.Round(value + 273.15, 2);
        _isUpdatingTemp = false;
    }

    partial void OnFahrenheitChanged(double value)
    {
        if (_isUpdatingTemp) return;

        double c = (value - 32) * 5 / 9;
        if (c < -273.15)
        {
            TempWarning = "Warning: Temperature is below absolute zero (-459.67 °F)!";
            return;
        }

        TempWarning = string.Empty;
        _isUpdatingTemp = true;
        Celsius = Math.Round(c, 2);
        Kelvin = Math.Round(c + 273.15, 2);
        _isUpdatingTemp = false;
    }

    partial void OnKelvinChanged(double value)
    {
        if (_isUpdatingTemp) return;

        if (value < 0)
        {
            TempWarning = "Warning: Temperature is below absolute zero (0 K)!";
            return;
        }

        TempWarning = string.Empty;
        _isUpdatingTemp = true;
        Celsius = Math.Round(value - 273.15, 2);
        Fahrenheit = Math.Round(((value - 273.15) * 9 / 5) + 32, 2);
        _isUpdatingTemp = false;
    }

    [RelayCommand]
    private void ResetTemp()
    {
        Celsius = 0;
    }
}