using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KumitateIDE.Libs;

internal struct  Settings
{
    public Settings()
    {
        Language = "English";
        Theme = "Light";
        WindowSize = new Vector2(800, 600);
    }

    public string Language { get; set; } = "English";
    public string Theme { get; set; } = "Light";
    public Vector2 WindowSize { get; set; } = new(800, 600);
    
    public ValueTask CheckJsonData(Settings settings)
    {
        Language ??= settings.Language;
        Theme ??= settings.Theme;
        if (WindowSize == new Vector2(0, 0)) WindowSize = settings.WindowSize;
        
        return  WriteJsonData(this);
    }
    private static readonly SemaphoreSlim Semaphore = new(1, 1);
    public async static ValueTask<string> ReadJsonData()
    {
        await Semaphore.WaitAsync();
        try
        {
            if (!File.Exists(FileLocation.Settings)) 
                File.Create(FileLocation.Settings).Close();
            
            return await File.ReadAllTextAsync(FileLocation.Settings);
        }
        finally
        {
            Semaphore.Release();
        }
    }
    
    public static async ValueTask  WriteJsonData(Settings settings)
    {
        await Semaphore.WaitAsync();
        try
        {
            string json = JsonConvert.SerializeObject(settings);
            await File.WriteAllTextAsync(FileLocation.Settings, json);
        }
        finally
        {
            Semaphore.Release();
        }
    }
    
    public static async ValueTask InitializeJsonData()
    {
        if (!File.Exists(FileLocation.Settings))
        {
            Settings settings = new();
           await WriteJsonData(settings);
        }
    }


} 

public struct FileLocation
{
    public const string Settings = "settings.json";
}