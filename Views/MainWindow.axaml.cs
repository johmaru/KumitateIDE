using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using KumitateIDE.Libs;
using KumitateIDE.ViewModels;
using Newtonsoft.Json;

namespace KumitateIDE.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        _ = Initialize();
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
            
            Console.WriteLine(LanguageController.CurrentLanguage);

            var mainWindowViewModel = new MainWindowViewModel();
            
            DataContext = mainWindowViewModel;
            LanguageController.InitializeLanguageData(mainWindowViewModel);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}