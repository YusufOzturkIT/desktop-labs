using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace HelloWindow;

public partial class MainWindow : Window
{
    private int _clickCount = 0; // Task 3: Tıklama sayacı

    public MainWindow()
    {
        InitializeComponent();
        Title = $"List 1 Your Name (Clicks: {_clickCount})";
    }

    private void OnHelloClick(object sender, RoutedEventArgs e)
    {
        ProcessGreeting();
    }

    // Task 3: Enter tuşuna basıldığında buton gibi çalışması
    private void OnNameBoxKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            ProcessGreeting();
        }
    }

    private void ProcessGreeting()
    {
        _clickCount++;
        Title = $"List 1 Your Name (Clicks: {_clickCount})";

        string inputName = NameBox.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrEmpty(inputName))
        {
            // Task 2.3: Boşsa kırmızı uyarı
            ResultText.Text = "Please enter your name";
            ResultText.Foreground = Brushes.Red;
        }
        else
        {
            // Task 2.3: Selamlama
            ResultText.Text = $"Hello, {inputName}!";
            ResultText.Foreground = Brushes.Black;
        }
    }
}