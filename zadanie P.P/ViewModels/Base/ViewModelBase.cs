using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace zadanie_P_P.ViewModels.Base;

// daje info ze dane sie zmienily
public abstract class ViewModelBase : INotifyPropertyChanged
{
    // zdarzenie ktore odswieza interfejs
    public event PropertyChangedEventHandler? PropertyChanged;

    // aktywuje zdarzenie pobiera nazwe zmienionej wlasciwosci
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}