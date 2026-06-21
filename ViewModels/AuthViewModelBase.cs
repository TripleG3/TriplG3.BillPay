using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TripleG3.BillPay.ViewModels;

public abstract class AuthViewModelBase : INotifyPropertyChanged
{
    private bool _isBusy;
    private string _errorMessage = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
        {
            return false;
        }

        storage = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        return true;
    }
}