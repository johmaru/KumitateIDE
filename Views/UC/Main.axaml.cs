using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using KumitateIDE.Libs;
using KumitateIDE.ViewModels.UC;

namespace KumitateIDE.Views.UC;

public partial class Main : UserControl
{
    public Main()
    {
        InitializeComponent();
        
       InitializeAsync();
    }

    private async void InitializeAsync()
    {
        await Initialize();
    }
    
    private async ValueTask Initialize()
    {
        try
        {
            MainUserControlViewModel mainUserControlViewModel = new();

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                DataContext =mainUserControlViewModel;
                LanguageController.InitializeLanguageData(mainUserControlViewModel);
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}