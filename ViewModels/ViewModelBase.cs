using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using KumitateIDE.Libs;

namespace KumitateIDE.ViewModels;

public class ViewModelBase : ObservableObject
{
    protected virtual void OnExit()
    {
        System.Environment.Exit(0);
    }
    
    protected ViewModelBase()
    {
       LanguageController.RegisterViewModel(this);
    }
}
