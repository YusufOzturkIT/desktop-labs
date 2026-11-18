using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaCounter.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private int _count = 0;

    [RelayCommand]
    private void Increment()
    {
        Count++;
    }
}
