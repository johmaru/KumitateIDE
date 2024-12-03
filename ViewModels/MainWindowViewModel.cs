using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using KumitateIDE.Libs;
using KumitateIDE.Views;
using KumitateIDE.Views.UC;

namespace KumitateIDE.ViewModels;

public partial  class MainWindowViewModel : ViewModelBase
{
    
    private object _currentView;
    public object CurrentView
    {
        get => _currentView;
        set => SetProperty(ref _currentView, value);
    }

    public void NavigateToView(object view)
    {
        CurrentView = view;
    }

    public void ForwardOptions()
    {
        if (CurrentView is Main)
        {
            NavigateToView(new Options());
        }
        else if (CurrentView is Options)
        {
            NavigateToView(new Main());
        }
    }
    
    public object GetView()
    {
        return CurrentView;
    }
     
     
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

        private string _files = string.Empty;
        
        [LanguageController.Localized("Files")]
        public string Files
        {
            get => _files;
            set => SetProperty(ref _files, value);
        }
        
        public ICommand ExitCommand { get; }
        
        public ICommand OpenOptionsCommand { get; }
        
        public MainWindowViewModel()
        {
            ExitCommand = new RelayCommand(OnExit);
            OpenOptionsCommand = new RelayCommand(ForwardOptions);
        }
    
        
}