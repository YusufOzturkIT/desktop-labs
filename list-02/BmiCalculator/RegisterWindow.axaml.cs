using Avalonia.Controls;
using Avalonia.Interactivity;

namespace BmiCalculator;

public partial class RegisterWindow : Window
{
    public RegisterWindow()
    {
        InitializeComponent();
    }

    private void OnTermsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        RegisterButton.IsEnabled = TermsCheckBox.IsChecked == true;
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private void OnRegisterClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}