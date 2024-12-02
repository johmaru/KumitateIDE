using System;
using KumitateIDE.Libs;

namespace KumitateIDE.ViewModels.UC;

public partial class MainUserControlViewModel : UserControlModelBase
{
    
    private string _greeting =string.Empty;
    
    [LanguageController.Localized("Greeting")]
    public string Greeting
    {
        get => _greeting;
        set
        {
            Console.WriteLine(value);
            SetProperty(ref _greeting, value);
        }
    }
    
}