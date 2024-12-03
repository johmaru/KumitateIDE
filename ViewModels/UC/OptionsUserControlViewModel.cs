using KumitateIDE.Libs;
using KumitateIDE.Views.UC.OptionsView;

namespace KumitateIDE.ViewModels.UC;

public class OptionsUserControlViewModel : UserControlModelBase
{
    private string _video_options = string.Empty;
    [LanguageController.Localized("VideoOptions")]
    public string VideoOptions
    {
        get => _video_options;
        set => SetProperty(ref _video_options, value);
    }
    private string _general_options = string.Empty;
    [LanguageController.Localized("GeneralOptions")]
    public string GeneralOptions
    {
        get => _general_options;
        set => SetProperty(ref _general_options, value);
    }
    
    private string _language = string.Empty;
    [LanguageController.Localized("Language")]
    public string Language
    {
        get => _language;
        set => SetProperty(ref _language, value);
    }
    
    private string _japanese = string.Empty;
    [LanguageController.Localized("Japanese")]
    public string Japanese
    {
        get => _japanese;
        set => SetProperty(ref _japanese, value);
    }
    
    private string _english = string.Empty;
    [LanguageController.Localized("English")]
    public string English
    {
        get => _english;
        set => SetProperty(ref _english, value);
    }
    
    private object _currentSelectOption;
    public object CurrentSelectOption
    {
        get => _currentSelectOption;
        set => SetProperty(ref _currentSelectOption, value);
    }
    
    public void SetCurrentSelectOption(string option)
    {
        CurrentSelectOption = option;
    }

}