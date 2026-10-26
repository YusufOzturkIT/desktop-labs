using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.Models;

namespace Contacts.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly List<Contact> _allContacts = new();

    public string[] SortOptions { get; } = ["First name", "Last name", "Favorites first"];

    [ObservableProperty]
    private string _selectedSortOption = "First name";

    partial void OnSelectedSortOptionChanged(string value) => ApplyFilterAndSort();

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
    [NotifyPropertyChangedFor(nameof(HasSelection))]
    private Contact? _selectedContact;

    public bool HasSelection => SelectedContact != null;

    [ObservableProperty]
    private string _searchText = string.Empty;

    partial void OnSearchTextChanged(string value) => ApplyFilterAndSort();

    public ObservableCollection<Contact> FilteredContacts { get; } = new();

    [ObservableProperty]
    private string _statusText = string.Empty;

    [ObservableProperty]
    private bool _isDarkMode = false;

    partial void OnIsDarkModeChanged(bool value)
    {
        if (Application.Current != null)
        {
            Application.Current.RequestedThemeVariant = value ? ThemeVariant.Dark : ThemeVariant.Light;
        }
    }

    [ObservableProperty]
    private bool _showEmptyState = false;

    [ObservableProperty]
    private string _emptyStateMessage = string.Empty;

    public MainViewModel()
    {
        _allContacts.Add(new Contact { FirstName = "Anna", LastName = "Kowalska", Phone = "555-123-456", Email = "anna@mail.com", IsFavorite = true });
        _allContacts.Add(new Contact { FirstName = "Jan", LastName = "Nowak", Phone = "555-987-654", Email = "jan@mail.com", IsFavorite = false });
        _allContacts.Add(new Contact { FirstName = "Piotr", LastName = "Zieliński", Phone = "555-222-333", Email = "piotr@mail.com", IsFavorite = true });
        _allContacts.Add(new Contact { FirstName = "Maria", LastName = "Wiśniewska", Phone = "555-444-555", Email = "maria@mail.com", IsFavorite = false });
        _allContacts.Add(new Contact { FirstName = "Yusuf", LastName = "Öztürk", Phone = "555-777-888", Email = "yusuf@mail.com", IsFavorite = true });

        foreach (var c in _allContacts)
        {
            c.PropertyChanged += (s, e) => 
            {
                if (e.PropertyName == nameof(Contact.FirstName) || e.PropertyName == nameof(Contact.LastName))
                {
                    OnPropertyChanged(nameof(SelectedContact));
                }
            };
        }

        ApplyFilterAndSort();
    }

    private void ApplyFilterAndSort()
    {
        var query = _allContacts.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            string search = SearchText.Trim().ToLowerInvariant();
            query = query.Where(c => c.FullName.ToLowerInvariant().Contains(search) ||
                                     c.Phone.ToLowerInvariant().Contains(search));
        }

        query = SelectedSortOption switch
        {
            "Last name" => query.OrderBy(c => c.LastName).ThenBy(c => c.FirstName),
            "Favorites first" => query.OrderByDescending(c => c.IsFavorite).ThenBy(c => c.FirstName),
            _ => query.OrderBy(c => c.FirstName).ThenBy(c => c.LastName)
        };

        var resultList = query.ToList();

        FilteredContacts.Clear();
        foreach (var item in resultList)
        {
            FilteredContacts.Add(item);
        }

        StatusText = $"{FilteredContacts.Count} contacts";

        if (FilteredContacts.Count == 0 && !string.IsNullOrWhiteSpace(SearchText))
        {
            ShowEmptyState = true;
            EmptyStateMessage = $"No contacts match '{SearchText}'";
        }
        else
        {
            ShowEmptyState = false;
            EmptyStateMessage = string.Empty;
        }
    }

    [RelayCommand]
    private void Add()
    {
        var newContact = new Contact
        {
            FirstName = "New",
            LastName = "Contact",
            Phone = "000-000-000",
            Email = "new@contact.com"
        };

        newContact.PropertyChanged += (s, e) => ApplyFilterAndSort();

        _allContacts.Add(newContact);
        ApplyFilterAndSort();
        SelectedContact = newContact;
    }

    private bool CanDelete() => SelectedContact != null;

    [RelayCommand(CanExecute = nameof(CanDelete))]
    private void Delete()
    {
        if (SelectedContact != null)
        {
            _allContacts.Remove(SelectedContact);
            SelectedContact = null;
            ApplyFilterAndSort();
        }
    }
}