using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace zadanie_P_P.ViewModels.Base;

// Laczy przycisk z widoku z metoda w ViewModelu
public class RelayCommand : ICommand
{
    private readonly Action<object?> _execute; // co ma sie stac po kliknieciu
    private readonly Predicate<object?>? _canExecute; // czy przycisk ma byc aktywny

    public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    //ecyduje czy przycisk da się kliknąć
    public bool CanExecute(object? parameter) => _canExecute == null || _canExecute(parameter);

    // Odpala wlasciwa metode po kliknieciu
    public void Execute(object? parameter) => _execute(parameter);

    // sprawdza czy przycisk powininen sie wyszarzyc
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}