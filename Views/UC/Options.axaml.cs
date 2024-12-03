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

namespace KumitateIDE.Views.UC;

public partial class Options : UserControl
{
    public static OptionsUserControlViewModel? OptionsUserControlViewModel;
    
    public Options()
    {
        InitializeComponent();

        OptionsUserControlViewModel = new OptionsUserControlViewModel();
        DataContext = OptionsUserControlViewModel;

        OptionsUserControlViewModel.CurrentSelectOption = new GeneralOption();
        LanguageController.InitializeLanguageData(OptionsUserControlViewModel);
    }

    private void OptionsItem_OnTapped(object? sender, TappedEventArgs e)
    {
        
    }
}