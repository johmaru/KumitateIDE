using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KumitateIDE.Libs;

partial struct  Settings
{
    public Settings()
    {
        Language = "English";
    }

    public string Language { get; set; } = "English";
    
    public ValueTask CheckJsonData()
    {
        Language ??= "English";
        return new ValueTask(Task.CompletedTask);
    }
    
    public static ValueTask<string> ReadJsonData()
    {
        if (!File.Exists(FileLocation.Settings)) File.Create(FileLocation.Settings).Close();
        string json = File.ReadAllText(FileLocation.Settings);
        return new ValueTask<string>(json);
    }
    
    public static async ValueTask  WriteJsonData(Settings settings)
    {
        string json = JsonConvert.SerializeObject(settings);
       await File.WriteAllTextAsync(FileLocation.Settings, json);
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

public class FilesController
{
    
}