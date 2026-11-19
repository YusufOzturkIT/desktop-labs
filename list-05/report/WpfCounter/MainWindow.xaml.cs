using System.Windows;

namespace WpfCounter
{
    public partial class MainWindow : Window
    {
        private int _count = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _count++;
            CountText.Text = $"Count: {_count}";
        }
    }
}
