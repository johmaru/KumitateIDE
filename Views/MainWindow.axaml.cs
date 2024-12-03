using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Styling;
using Avalonia.Threading;
using KumitateIDE.Libs;
using KumitateIDE.ViewModels;
using KumitateIDE.Views.UC;
using Newtonsoft.Json;

namespace KumitateIDE.Views;

public partial class MainWindow : Window
{
    public static MainWindowViewModel? MainWindowViewModel;
    private static Window _window;
    public MainWindow()
    {
        InitializeComponent();

       
            InitializeAsync();
            
            _window = this;
    }

    private async void InitializeAsync()
    {
        try
        {
            await Initialize();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public static void WindowSizeChange(double width, double height)
    {
        _window.Width = width;
        _window.Height = height;
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
            var settings = await Settings.ReadJsonData();
            
            var settingsData = JsonConvert.DeserializeObject<Settings>(settings);
            var control = new Settings();
            await control.CheckJsonData(settingsData);
            this.Width = settingsData.WindowSize.X;
            this.Height = settingsData.WindowSize.Y;

            Dispatcher.UIThread.InvokeAsync(() =>
            {
                App.SetTheme(this, settingsData.Theme switch
                {
                    "Light" => App.Theme.Light,
                    "Dark" => App.Theme.Dark,
                    _ => throw new ArgumentException("Invalid theme setting")
                });
            });
            
            LanguageController.CurrentLanguage = settingsData.Language switch
            {
                "English" => LanguageController.Language.English,
                "Japanese" => LanguageController.Language.Japanese,
                _ => throw new NotInitializedLanguageError()
            };
            MainWindowViewModel = new MainWindowViewModel();

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                DataContext = MainWindowViewModel;
                LanguageController.InitializeLanguageData(MainWindowViewModel);
                MainWindowViewModel.NavigateToView(new Main());
            });


        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to initialize the settings. .{e.Message}");
            throw;
        }
    }
    
      public static void SetThemeThis(App.Theme theme)
        {
            var themeVariant = theme switch
            {
                App.Theme.Light => ThemeVariant.Light,
                App.Theme.Dark => ThemeVariant.Dark,
                _ => throw new ArgumentOutOfRangeException(nameof(theme), theme, null)
            };
            Application.Current!.RequestedThemeVariant = themeVariant;
            _window.RequestedThemeVariant = themeVariant;
        }
}