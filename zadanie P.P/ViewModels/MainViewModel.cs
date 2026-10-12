using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using zadanie_P_P.Models;
using zadanie_P_P.Services;
using zadanie_P_P.ViewModels.Base;

namespace zadanie_P_P.ViewModels;

public class MainViewModel : ViewModelBase
{
    // Serwis wejscia i wyjscia do pliku json
    private readonly FileService _fileService = new();

    // Glowna kolekcja i jej widok filtrowany/sortowany
    private ObservableCollection<Item> _items;
    private ICollectionView _itemsView;

    private Item? _selectedItem;
    private string _searchText = string.Empty;

    public MainViewModel()
    {
        // Wczytanie z pliku json 
        _items = new ObservableCollection<Item>(_fileService.LoadItems());

        // Konfiguracja filtrowania i sortowania 
        _itemsView = CollectionViewSource.GetDefaultView(_items);
        _itemsView.Filter = itemObj => itemObj is Item i &&
            (string.IsNullOrWhiteSpace(SearchText) || i.Name.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase));
        _itemsView.SortDescriptions.Add(new SortDescription(nameof(Item.Name), ListSortDirection.Ascending));

        // przyciski edytuj/usun aktywne tylko gdy zaznaczono wiersz
        AddItemCommand = new RelayCommand(AddItem);
        EditItemCommand = new RelayCommand(EditItem, _ => SelectedItem != null);
        DeleteItemCommand = new RelayCommand(DeleteItem, _ => SelectedItem != null);
    }

    // Kolekcja i widok powiązane z DataGrid w widoku XAML 
    public ObservableCollection<Item> Items => _items;
    public ICollectionView ItemsView => _itemsView;

    // znaznaczony  elemnt w tablicy
    public Item? SelectedItem
    {
        get => _selectedItem;
        set { _selectedItem = value; OnPropertyChanged(); }
    }

    // tekst wyszukiwarki
    public string SearchText
    {
        get => _searchText;
        set { _searchText = value; OnPropertyChanged(); _itemsView.Refresh(); }
    }

    // Komendy dla przyciskow
    public ICommand AddItemCommand { get; }
    public ICommand EditItemCommand { get; }
    public ICommand DeleteItemCommand { get; }

    // dodawanie
    private void AddItem(object? p)
    {
        int nextId = _items.Any() ? _items.Max(i => i.Id) + 1 : 1;
        _items.Add(new Item { Id = nextId, Name = "Nowy towar", Category = "Bluzy", Size = "M" });
        _fileService.SaveItems(_items); // Zapis do pliku json
    }

    //edycja
    private void EditItem(object? p)
    {
        _fileService.SaveItems(_items);
        _itemsView.Refresh();
    }

    //usuwanie + okienko potwierdzania
    private void DeleteItem(object? p)
    {
        if (SelectedItem == null) return;

        var answer = MessageBox.Show($"Czy usunąć: \"{SelectedItem.Name}\"?", "Potwierdzenie",
            MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (answer == MessageBoxResult.Yes)
        {
            _items.Remove(SelectedItem);
            _fileService.SaveItems(_items);
        }
    }
}