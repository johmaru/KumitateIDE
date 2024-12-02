using System.ComponentModel;
using System.Runtime.CompilerServices;
using KumitateIDE.Libs;

namespace KumitateIDE.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private string _greeting;
    public string Greeting
    {
        get => _greeting;
        set => Set(ref _greeting, value);
    }
    
    public MainWindowViewModel()
    {
        Greeting = "";
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    protected bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}