using CommunityToolkit.Mvvm.ComponentModel;

namespace Contacts.Models;

public partial class Contact : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string _firstName = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string _lastName = string.Empty;

    [ObservableProperty]
    private string _phone = string.Empty;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private bool _isFavorite = false;

    public string FullName => $"{FirstName} {LastName}".Trim();
}