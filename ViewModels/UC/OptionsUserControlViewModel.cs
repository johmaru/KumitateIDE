using System;
using KumitateIDE.Libs;
using KumitateIDE.Views.UC.OptionsView;
using Newtonsoft.Json;

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
    private string _theme = string.Empty;
    [LanguageController.Localized("Theme")]
    public string Theme
    {
        get => _theme;
        set => SetProperty(ref _theme, value);
    }
    
    private string _dark = string.Empty;
    [LanguageController.Localized("Dark")]
    public string Dark
    {
        get => _dark;
        set => SetProperty(ref _dark, value);
    }
    
    private string _light = string.Empty;
    [LanguageController.Localized("Light")]
    public string Light
    {
        get => _light;
        set => SetProperty(ref _light, value);
    }
    
    private string _window_size = string.Empty;
    [LanguageController.Localized("WindowSize")]
    public string WindowSize
    {
        get => _window_size;
        set => SetProperty(ref _window_size, value);
    }
    
    private string _width = string.Empty;
    [LanguageController.Localized("Width")]
    public string Width
    {
        get => _width;
        set => SetProperty(ref _width, value);
    }
    
    private string _height = string.Empty;
    [LanguageController.Localized("Height")]
    public string Height
    {
        get => _height;
        set => SetProperty(ref _height, value);
    }
    
    private string _save = string.Empty;
    [LanguageController.Localized("Save")]
    public string Save
    {
        get => _save;
        set => SetProperty(ref _save, value);
    }
    
    private string _back = string.Empty;

    [LanguageController.Localized("Back")]
    public string Back
    {
        get => _back;
        set => SetProperty(ref _back, value);
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