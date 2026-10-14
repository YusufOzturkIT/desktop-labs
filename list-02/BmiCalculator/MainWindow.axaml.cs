using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace BmiCalculator;

public partial class MainWindow : Window
{
    private readonly List<string> _history = new();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnCalculateClick(object? sender, RoutedEventArgs e)
    {
        StatusText.Text = "Ready";

        // Parse inputs with culture invariant support
        string weightInput = (WeightBox.Text ?? "").Replace(',', '.');
        string heightInput = (HeightBox.Text ?? "").Replace(',', '.');

        if (!double.TryParse(weightInput, NumberStyles.Any, CultureInfo.InvariantCulture, out double weight) || weight <= 0)
        {
            StatusText.Text = "Error: Invalid or non-positive weight input!";
            return;
        }

        if (!double.TryParse(heightInput, NumberStyles.Any, CultureInfo.InvariantCulture, out double height) || height <= 0)
        {
            StatusText.Text = "Error: Invalid or non-positive height input!";
            return;
        }

        // Unit Conversions
        double weightKg = WeightUnitBox.SelectedIndex == 1 ? weight * 0.4536 : weight;
        double heightM = HeightUnitBox.SelectedIndex == 1 ? (height * 2.54) / 100.0 : height / 100.0;

        if (heightM == 0)
        {
            StatusText.Text = "Error: Height cannot be zero!";
            return;
        }

        // BMI Calculation
        double bmi = weightKg / (heightM * heightM);

        string category;
        IBrush color;

        if (bmi < 18.5)
        {
            category = "Underweight";
            color = Brushes.Orange;
        }
        else if (bmi < 25.0)
        {
            category = "Normal weight";
            color = Brushes.Green;
        }
        else if (bmi < 30.0)
        {
            category = "Overweight";
            color = Brushes.OrangeRed;
        }
        else
        {
            category = "Obese";
            color = Brushes.Red;
        }

        ResultText.Text = $"BMI: {bmi:F1} {category}";
        ResultText.Foreground = color;

        // Task 3: History Update (Last 5)
        string timeStamp = DateTime.Now.ToString("HH:mm");
        string wUnit = WeightUnitBox.SelectedIndex == 1 ? "lb" : "kg";
        string hUnit = HeightUnitBox.SelectedIndex == 1 ? "in" : "cm";
        string historyEntry = $"{timeStamp} - {weight} {wUnit}, {height} {hUnit} -> {bmi:F1}";

        _history.Insert(0, historyEntry);
        if (_history.Count > 5)
        {
            _history.RemoveAt(5);
        }

        HistoryListBox.ItemsSource = null;
        HistoryListBox.ItemsSource = _history;
    }

    private void OnDescCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if (DescText != null)
        {
            DescText.IsVisible = DescCheckBox.IsChecked == true;
        }
    }

    private void OnRegisterMenuClick(object? sender, RoutedEventArgs e)
    {
        var registerWin = new RegisterWindow();
        registerWin.Show(this);
    }

    private void OnExitMenuClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private void OnAboutMenuClick(object? sender, RoutedEventArgs e)
    {
        StatusText.Text = "BMI Calculator v1.0 - Desktop Application Development";
    }
}
