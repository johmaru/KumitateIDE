using CommunityToolkit.Mvvm.ComponentModel;

namespace KumitateIDE.ViewModels;

public class ViewModelBase : ObservableObject
{
    protected virtual void OnExit()
    {
        System.Environment.Exit(0);
    }
}
