using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using KumitateIDE.Libs;

namespace KumitateIDE.ViewModels;

public partial  class MainWindowViewModel : ViewModelBase
{
     
     
    private string _greeting =string.Empty;
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