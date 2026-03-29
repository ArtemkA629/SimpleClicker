using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonUtilitySaveSystem : ISaveSystem
{
    private readonly string _filePath;

    private SaveFile _saveFile;

    public JsonUtilitySaveSystem()
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
        string json = Serialize(value);
        SetEntry(key, typeof(T).AssemblyQualifiedName, json);
        SaveAll();
    }

    public T Load<T>(string key, T defaultValue = default)
    {
        var entry = GetEntry(key);
        
        if (entry == null)
            return defaultValue;

        try
        {
            return Deserialize<T>(entry.json);
        }
        catch (Exception e)
        {
            Debug.LogError($"Load failed for key '{key}': {e}");
            return defaultValue;
        }
    }

    public bool HasKey(string key) => GetEntry(key) != null;

    public void Delete(string key)
    {
        _saveFile.entries.RemoveAll(e => e.key == key);
        SaveAll();
    }

    private Entry GetEntry(string key)
    {
        return _saveFile.entries.Find(e => e.key == key);
    }

    private void SetEntry(string key, string type, string json)
    {
        var entry = GetEntry(key);

        if (entry == null)
        {
            entry = new Entry { key = key };
            _saveFile.entries.Add(entry);
        }

        entry.type = type;
        entry.json = json;
    }

    private string Serialize<T>(T value)
    {
        if (IsSimple(typeof(T)))
        {
            var wrapper = new Wrapper<T> { value = value };
            return JsonUtility.ToJson(wrapper);
        }

        return JsonUtility.ToJson(value);
    }

    private T Deserialize<T>(string json)
    {
        if (IsSimple(typeof(T)))
        {
            var wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.value;
        }

        return JsonUtility.FromJson<T>(json);
    }

    private bool IsSimple(Type type)
    {
        return type.IsPrimitive || type == typeof(string);
    }

    private void SaveAll()
    {
        try
        {
            string json = JsonUtility.ToJson(_saveFile, true);
            File.WriteAllText(_filePath, json);
        }
        catch (Exception e)
        {
            Debug.LogError($"SaveAll failed: {e}");
        }
    }

    private void LoadAll()
    {
        if (!File.Exists(_filePath))
        {
            _saveFile = new SaveFile();
            return;
        }

        try
        {
            string json = File.ReadAllText(_filePath);
            _saveFile = JsonUtility.FromJson<SaveFile>(json) ?? new SaveFile();
        }
        catch (Exception e)
        {
            Debug.LogError($"LoadAll failed: {e}");
            _saveFile = new SaveFile();
        }
    }
    
    [Serializable]
    private class SaveFile
    {
        public List<Entry> entries = new();
    }

    [Serializable]
    private class Entry
    {
        public string key;
        public string type;
        public string json;
    }
    
    [Serializable]
    private class Wrapper<T>
    {
        public T value;
    }
}