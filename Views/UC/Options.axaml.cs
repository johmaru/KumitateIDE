using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using KumitateIDE.Libs;
using KumitateIDE.ViewModels.UC;
using KumitateIDE.Views.UC.OptionsView;
using Newtonsoft.Json;

namespace KumitateIDE.Views.UC;

public partial class Options : UserControl
{

    
    public Options()
    {
        InitializeComponent();
        DataContext ??= new OptionsUserControlViewModel();
        Init();
        
    }
    
    private OptionsUserControlViewModel ViewModel => (OptionsUserControlViewModel)DataContext!;
    
    private async void Init()
    {
       ViewModel.CurrentSelectOption = new GeneralOption();
        LanguageController.InitializeLanguageData(ViewModel);
    }

    private void OptionsItem_OnTapped(object? sender, TappedEventArgs e)
    {
        
    }
}