using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using System.Runtime.InteropServices;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using KumitateIDE.ViewModels;
using KumitateIDE.Views;

namespace KumitateIDE;

public partial class App : Application
{
    public static OSPlatform? Os { get; private set; } 
    
    public override void Initialize()
    {
        Os = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? OSPlatform.Windows : 
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? OSPlatform.Linux : 
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? OSPlatform.OSX : 
            null;
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }

    public enum Theme
    {
        Light,
        Dark,
    }
    public static void SetTheme(Window window,Theme theme)
    {
        var themeVariant = theme switch
        {
            Theme.Light => ThemeVariant.Light,
            Theme.Dark => ThemeVariant.Dark,
            _ => throw new ArgumentOutOfRangeException(nameof(theme), theme, null)
        };
        Application.Current!.RequestedThemeVariant = themeVariant;
        window.RequestedThemeVariant = themeVariant;
    }

    public enum Windows
    {
        MainWindow
    }
  }