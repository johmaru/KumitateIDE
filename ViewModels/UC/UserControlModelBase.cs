using CommunityToolkit.Mvvm.ComponentModel;
using KumitateIDE.Libs;

namespace KumitateIDE.ViewModels.UC;

public class UserControlModelBase : ObservableObject
{
    protected UserControlModelBase()
    {
        LanguageController.RegisterViewModel(this);
    }
}