using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using KumitateIDE.ViewModels;

namespace KumitateIDE.Libs;

public class NotInitializedLanguageError() : Exception("Language is not initialized.");

public class InitializeFailedLanguageError() : Exception("Language initialization failed.");

public class UnknownLanguageError() : Exception("Unknown language.");

public static class LanguageData
{
    public record LocalizedData(string English, string Japanese);
    
    
    public static Dictionary<string, LocalizedData> Data { get; } = new()
    {
        ["Greeting"] = new LocalizedData("Hello, World!", "こんにちは、世界！"),
        ["Options"] = new LocalizedData("Options", "オプション"),
        ["Exit"] = new LocalizedData("Exit", "終了"),
        ["Files"] = new LocalizedData("Files", "ファイル"),
        ["VideoOptions"] = new LocalizedData("Video Options", "ビデオオプション"),
        ["GeneralOptions"] = new LocalizedData("General Options", "一般オプション"),
        ["Language"] = new LocalizedData("Language", "言語"),
        ["Japanese"] = new LocalizedData("Japanese", "日本語"),
        ["English"] = new LocalizedData("English", "英語"),
        ["Theme"] = new LocalizedData("Theme", "テーマ"),
        ["Dark"] = new LocalizedData("Dark", "ダーク"),
        ["Light"] = new LocalizedData("Light", "ライト"),
        ["WindowSize"] = new LocalizedData("WindowSize","ウィンドウサイズ"),
        ["Width"] = new LocalizedData("Width", "幅"),
        ["Height"] = new LocalizedData("Height", "高さ"),
        ["Save"] = new LocalizedData("Save","セーブ"),
        ["Back"] = new LocalizedData("Back","戻る"),
    };
}

public abstract class LanguageController
{
    public static Language? CurrentLanguage { get; set; } = null;
    private static readonly List<WeakReference<object>>_registredViewModels = new();
    
    public static void RegisterViewModel(object viewModel)
    {
        _registredViewModels.Add(new WeakReference<object>(viewModel));
    }
    
    public static void UpdateAllViewModels()
    {
        var aliveViewModels = _registredViewModels
            .Where(wr => wr.TryGetTarget(out _))
            .ToList();
        
        _registredViewModels.RemoveAll(wr => !wr.TryGetTarget(out _));
        
        foreach (var weakRef in aliveViewModels)
        {
            if (weakRef.TryGetTarget(out var viewModel))
            {
                InitializeLanguageData(viewModel);
            }
        }
    }
    public enum Language
    {
        English = 0,
        Japanese = 1,
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class LocalizedAttribute : Attribute
    {
        public string Key { get; }
        public LocalizedAttribute(string key) => Key = key;
    }
    
    
    public static string GetLanguageData(string key)
    {
        if (CurrentLanguage == null) throw new NotInitializedLanguageError();
        return CurrentLanguage switch
        {
            Language.English => LanguageData.Data[key].English,
            Language.Japanese => LanguageData.Data[key].Japanese,
            _ => throw new UnknownLanguageError()
        };
    }
    
    public static void InitializeLanguageData(MainWindowViewModel mainWindowViewModel)
    {
        try
        {
            var type = mainWindowViewModel.GetType();
            var properties = type.GetProperties()
                .Where(p => p.GetCustomAttribute<LocalizedAttribute>() != null);

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<LocalizedAttribute>();
                property.SetValue(mainWindowViewModel, GetLanguageData(attribute.Key));
            }
        }
        catch (Exception e)
        {
            throw new InitializeFailedLanguageError();
        }
    }
    
    public static void InitializeLanguageData<T>(T viewModel)
    {
        try
        {
            var type = viewModel.GetType();
            var properties = type.GetProperties()
                .Where(p => p.GetCustomAttribute<LocalizedAttribute>() != null);

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<LocalizedAttribute>();
                property.SetValue(viewModel, GetLanguageData(attribute.Key));
            }
        }
        catch (Exception e)
        {
            throw new InitializeFailedLanguageError();
        }
    }

}