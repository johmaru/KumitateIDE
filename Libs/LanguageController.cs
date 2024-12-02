using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using KumitateIDE.ViewModels;

namespace KumitateIDE.Libs;

public class NotInitializedLanguageError() : Exception("Language is not initialized.");

public class InitializeFailedLanguageError() : Exception("Language initialization failed.");

public static class LanguageData
{
    public record LocalizedData(string English, string Japanese);
    
    public static Dictionary<string, LocalizedData> Data { get; } = new()
    {
        ["Greeting"] = new LocalizedData("Hello, World!", "こんにちは、世界！"),
        ["Options"] = new LocalizedData("Options", "オプション"),
        ["Exit"] = new LocalizedData("Exit", "終了"),
        ["Files"] = new LocalizedData("Files", "ファイル"),
    };
}

public abstract class LanguageController
{
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
            _ => throw new NotInitializedLanguageError()
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
    

    public static Language? CurrentLanguage { get; set; } = null;

}