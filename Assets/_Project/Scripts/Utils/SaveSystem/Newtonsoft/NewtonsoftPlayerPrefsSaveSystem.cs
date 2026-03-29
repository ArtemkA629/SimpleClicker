using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class NewtonsoftPlayerPrefsSaveSystem : ISaveSystem
{
    private const string SaveKey = "SAVE_DATA";

    private Dictionary<string, object> _data;

    private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.Auto,
        Formatting = Formatting.None
    };

    public NewtonsoftPlayerPrefsSaveSystem()
    {
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
        string json = JsonConvert.SerializeObject(_data, _settings);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }
    
    private void LoadAll()
    {
        if (PlayerPrefs.HasKey(SaveKey) == false)
        {
            _data = new Dictionary<string, object>();
            return;
        }

        try
        {
            string json = PlayerPrefs.GetString(SaveKey);
            _data = JsonConvert.DeserializeObject<Dictionary<string, object>>(json, _settings)
                    ?? new Dictionary<string, object>();
        }
        catch
        {
            _data = new Dictionary<string, object>();
        }
    }
}