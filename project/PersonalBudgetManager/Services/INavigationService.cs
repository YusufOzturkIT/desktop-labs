using PersonalBudgetManager.ViewModels;

namespace PersonalBudgetManager.Services;

public interface INavigationService
{
    ViewModelBase CurrentView { get; }
    void NavigateTo<T>() where T : ViewModelBase;
}
