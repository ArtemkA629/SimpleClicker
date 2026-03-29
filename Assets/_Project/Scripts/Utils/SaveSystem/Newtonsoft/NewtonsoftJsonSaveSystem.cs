using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class NewtonsoftJsonSaveSystem : ISaveSystem
{
    private readonly string _filePath;
    private Dictionary<string, object> _data;

    private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.Auto,
        Formatting = Formatting.Indented
    };

    public NewtonsoftJsonSaveSystem()
    {
        string directory = Path.Combine(Application.persistentDataPath, "Saves");

        if (Directory.Exists(directory) == false)
        {
            Directory.CreateDirectory(directory);
        }

        _filePath = Path.Combine(directory, "save.json");
        LoadAll();
    }

    public void Save<T>(string key, T value)
    {
        _data[key] = value;
        SaveAll();
    }

    public T Load<T>(string key, T defaultValue = default)
    {
        if (_data.ContainsKey(key) == false)
            return defaultValue;

        try
        {
            string json = JsonConvert.SerializeObject(_data[key], _settings);
            return JsonConvert.DeserializeObject<T>(json, _settings);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Load failed for key '{key}': {e}");
            return defaultValue;
        }
    }

    public bool HasKey(string key) => _data.ContainsKey(key);

    public void Delete(string key)
    {
        if (_data.Remove(key))
            SaveAll();
    }

    private void SaveAll()
    {
        try
        {
            string json = JsonConvert.SerializeObject(_data, _settings);
            File.WriteAllText(_filePath, json);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"SaveAll failed: {e}");
        }
    }

    private void LoadAll()
    {
        if (File.Exists(_filePath) == false)
        {
            _data = new Dictionary<string, object>();
            return;
        }

        try
        {
            string json = File.ReadAllText(_filePath);
            _data = JsonConvert.DeserializeObject<Dictionary<string, object>>(json, _settings)
                    ?? new Dictionary<string, object>();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"LoadAll failed: {e}");
            _data = new Dictionary<string, object>();
        }
    }
}