using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KumitateIDE.ViewModels;

namespace KumitateIDE.Libs;

public class NotInitializedLanguageError() : Exception("Language is not initialized.");

public abstract class LanguageController
{
    public enum Language
    {
        English = 0,
        Japanese = 1,
    }


    private static Dictionary<string, string[]> LanguageData() => new()
    {
        { "Greeting", ["Welcome to Avalonia!", "Avaloniaへようこそ！"] },
        {"Hello", ["Hello!", "こんにちは！"]},
        {"Goodbye", ["Goodbye!", "さようなら！"]},
    };
    
    public static string GetLanguageData(string key)
    {
        if (CurrentLanguage == null) throw new NotInitializedLanguageError();
        return CurrentLanguage switch
        {
            Language.English => LanguageData()[key][0],
            Language.Japanese => LanguageData()[key][1],
            _ => throw new NotInitializedLanguageError()
        };
    }
    
    public static void InitializeLanguageData(MainWindowViewModel mainWindowViewModel)
    {
         mainWindowViewModel.Greeting = GetLanguageData("Greeting");
    }
    

    public static Language? CurrentLanguage { get; set; } = null;

}