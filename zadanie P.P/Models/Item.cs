using System;
using System.Collections.Generic;
using System.Text;
using zadanie_P_P.ViewModels.Base;

namespace zadanie_P_P.Models;

public class Item : ViewModelBase
{
    private int _id;
    private string _name = string.Empty;
    private string _category = "Bluzy";
    private string _size = "M";
    private decimal _purchasePrice;
    private decimal _sellingPrice;

    public int Id
    {
        get => _id;
        set { _id = value; OnPropertyChanged(); }
    }

    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }

    public string Category
    {
        get => _category;
        set { _category = value; OnPropertyChanged(); }
    }

    public string Size
    {
        get => _size;
        set { _size = value; OnPropertyChanged(); }
    }

    public decimal PurchasePrice
    {
        get => _purchasePrice;
        set
        {
            _purchasePrice = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(PotentialProfit));
        }
    }

    public decimal SellingPrice
    {
        get => _sellingPrice;
        set
        {
            _sellingPrice = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(PotentialProfit));
        }
    }

    public decimal PotentialProfit => SellingPrice - PurchasePrice;
}