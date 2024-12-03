using System;
using System.Globalization;
using System.Numerics;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Avalonia.Threading;
using KumitateIDE.Libs;
using KumitateIDE.ViewModels.UC;
using Newtonsoft.Json;

namespace KumitateIDE.Views.UC.OptionsView;

public partial class GeneralOption : UserControl
{
    private bool _isProcessing;
    public GeneralOption()
    {
        InitializeComponent();
    }

    private async void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
       if (_isProcessing) return;
       try
       {
           var senderControl = (SelectingItemsControl)sender!;
           if(senderControl.SelectedIndex == 0)
           {
              
                   var setting = await Settings.ReadJsonData();
                   var json = JsonConvert.DeserializeObject<Settings>(setting);
                   json.Language = "English";
                   await Settings.WriteJsonData(json);

                   LanguageController.CurrentLanguage = LanguageController.Language.English;
                   LanguageController.UpdateAllViewModels();
           }
           else if(senderControl.SelectedIndex == 1)
           {
             
                    var setting = await  Settings.ReadJsonData();
                    var json =  JsonConvert.DeserializeObject<Settings>(setting);
                    json.Language = "Japanese";
                    await Settings.WriteJsonData(json);
            
                    LanguageController.CurrentLanguage = LanguageController.Language.Japanese;
                    LanguageController.UpdateAllViewModels();
           }
       }
       finally
       {
           _isProcessing = false;
       }
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
       MainWindow.MainWindowViewModel?.ForwardOptions();
    }

    private async void WidthTextBlock_OnLoaded(object? sender, RoutedEventArgs e)
    {
      if (_isProcessing) return;
        try
        {
         
              var setting = await Settings.ReadJsonData();
              var json =  JsonConvert.DeserializeObject<Settings>(setting);
                json = json;
          await  Dispatcher.UIThread.InvokeAsync(() =>
          {
              ((TextBox)sender!).Text = json.WindowSize.X.ToString(CultureInfo.InvariantCulture);
          });
        }
        finally
        {
            _isProcessing = false;
        }
    }

    private async void HeightTextBlock_OnLoaded(object? sender, RoutedEventArgs e)
    {
        if (_isProcessing) return;
        try
        {
           
                var setting = await Settings.ReadJsonData();
                var json =  JsonConvert.DeserializeObject<Settings>(setting);
                json = json;
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                ((TextBox)sender!).Text = json.WindowSize.Y.ToString(CultureInfo.InvariantCulture);
            });
        }
        finally
        {
            _isProcessing = false;
        }
    }

    private async void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
      if (_isProcessing) return;
        try
        {
        
                var setting = await Settings.ReadJsonData();
                var json =  JsonConvert.DeserializeObject<Settings>(setting);
            await  Dispatcher.UIThread.InvokeAsync(() =>
            {
                if(json.Language == "English")
                {
                    ((SelectingItemsControl)sender!).SelectedIndex = 0;
                }
                else if(json.Language == "Japanese")
                {
                    ((SelectingItemsControl)sender!).SelectedIndex = 1;
                }
            });
        }
        finally
        {
            _isProcessing = false;
        }
    }

    private async void ThemeComboBox_OnLoaded(object? sender, RoutedEventArgs e)
    {
        if (_isProcessing) return;
        try
        {
                var setting = await Settings.ReadJsonData();
                var json =  JsonConvert.DeserializeObject<Settings>(setting);
     
            await   Dispatcher.UIThread.InvokeAsync(() =>
            {
                ((ComboBox)sender!).SelectedIndex = json.Theme switch
                {
                    "Light" => 0,
                    "Dark" => 1,
                    _ => ((ComboBox)sender!).SelectedIndex
                };
            });
        }
        finally
        {
            _isProcessing = false;
        }
    }


    private async void SaveClick_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            var widthTextBox = this.FindControl<TextBox>("WidthTextBox");
            var heightTextBox = this.FindControl<TextBox>("HeightTextBox");

            if (string.IsNullOrEmpty(widthTextBox?.Text) ||
                string.IsNullOrEmpty(heightTextBox?.Text))
                return;

            var setting = await Settings.ReadJsonData();
            var json = JsonConvert.DeserializeObject<Settings>(setting);

            json.WindowSize = new Vector2(
                float.Parse(widthTextBox.Text, CultureInfo.InvariantCulture),
                float.Parse(heightTextBox.Text, CultureInfo.InvariantCulture)
            );

            await Settings.WriteJsonData(json);

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                MainWindow.WindowSizeChange(json.WindowSize.X, json.WindowSize.Y);
            });

            MainWindow.MainWindowViewModel?.ForwardOptions();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

 
    
    private bool _isFirst = true;

    private async void ThemeComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (_isProcessing) return;
        if (_isFirst)
        {
            _isFirst = false;
            return;
        }
        try
        {
            var senderControl = (SelectingItemsControl)sender!;
            if(senderControl.SelectedIndex == 0)
            {
                var setting = await  Settings.ReadJsonData();
                var json =  JsonConvert.DeserializeObject<Settings>(setting);
                json.Theme = "Light";
                await Settings.WriteJsonData(json);
            
                MainWindow.SetThemeThis(App.Theme.Light);
            }
            else if(senderControl.SelectedIndex == 1)
            {
                var setting = await  Settings.ReadJsonData();
                var json =  JsonConvert.DeserializeObject<Settings>(setting);
                json.Theme = "Dark";
                await Settings.WriteJsonData(json);
            
                MainWindow.SetThemeThis(App.Theme.Dark);
            }
        }
        finally
        {
            _isProcessing = false;
        }
    }
}