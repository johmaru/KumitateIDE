using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using KumitateIDE.Libs;

namespace KumitateIDE.ViewModels;

public partial  class MainWindowViewModel : ViewModelBase
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
     
     private string _options = string.Empty;
     
        [LanguageController.Localized("Options")]
        public string Options
        {
            get => _options;
            set => SetProperty(ref _options, value);
        }
     
     private string _exit = string.Empty;
     [LanguageController.Localized("Exit")]
        public string Exit
        {
            get => _exit;
            set => SetProperty(ref _exit, value);
        }
        
        public ICommand ExitCommand { get; }
        
        public MainWindowViewModel()
        {
            ExitCommand = new RelayCommand(OnExit);
        }

        private string _files = string.Empty;
        
        [LanguageController.Localized("Files")]
        public string Files
        {
            get => _files;
            set => SetProperty(ref _files, value);
        }
    
        
}