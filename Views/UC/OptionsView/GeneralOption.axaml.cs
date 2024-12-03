using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using KumitateIDE.Libs;
using Newtonsoft.Json;

namespace KumitateIDE.Views.UC.OptionsView;

public partial class GeneralOption : UserControl
{
    public GeneralOption()
    {
        InitializeComponent();
    }

    private async void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var senderControl = (SelectingItemsControl)sender!;
        if(senderControl.SelectedIndex == 0)
        {
           var setting = await  Settings.ReadJsonData();
          var json =  JsonConvert.DeserializeObject<Settings>(setting);
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
}