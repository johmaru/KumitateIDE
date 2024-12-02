using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using KumitateIDE.Libs;
using KumitateIDE.ViewModels;
using Newtonsoft.Json;

namespace KumitateIDE.Views;

public partial class MainWindow : Window
{
    private MainWindowViewModel? _mainWindowViewModel;
    public MainWindow()
    {
        InitializeComponent();

       
            InitializeAsync();
       
    }

    private async void InitializeAsync()
    {
        try
        {
            await Initialize();

            await Task.Delay(5000);
            
            _mainWindowViewModel.Greeting = LanguageController.GetLanguageData("Hello");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async ValueTask Initialize()
    {
        try
        {
            await Settings.InitializeJsonData();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
            
        try
        {
            var settings = Settings.ReadJsonData().Result;
            var settingsData = JsonConvert.DeserializeObject<Settings>(settings);
            LanguageController.CurrentLanguage = settingsData.Language switch
            {
                "English" => LanguageController.Language.English,
                "Japanese" => LanguageController.Language.Japanese,
                _ => throw new NotInitializedLanguageError()
            };
            _mainWindowViewModel = new MainWindowViewModel();

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                DataContext = _mainWindowViewModel;
                LanguageController.InitializeLanguageData(_mainWindowViewModel);
            });


        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}