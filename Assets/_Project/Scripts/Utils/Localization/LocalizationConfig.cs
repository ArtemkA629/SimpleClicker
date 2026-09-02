using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/LocalizationConfig", fileName = "LocalizationConfig")]
public class LocalizationConfig : ScriptableObject
{
    [Serializable]
    public class Item
    {
        public string id;
        public Language[] languages;

        public Item(string id, Language[] languages)
        {
            this.id = id;
            this.languages = languages;
        }
    }

    [Serializable]
    public class Language
    {
        public string languageId;
        public string value;

        public Language(string languageId, string value)
        {
            this.languageId = languageId;
            this.value = value;
        }
    }

    [SerializeField] private Item[] _items;

    public string GetText(string id, string language)
    {
        foreach (var item in _items)
        {
            if (item.id == id)
            {
                if (item.languages == null || item.languages.Length == 0)
                {
                    Debug.LogError($"No languages for id '{id}'");
                    return id;
                }

                foreach (var lang in item.languages)
                {
                    if (lang.languageId == language)
                    {
                        return lang.value;
                    }
                }

                Debug.LogWarning($"Language '{language}' not found for '{id}', using fallback");

                return item.languages[0].value;
            }
        }

        Debug.LogError($"Not exist localization for '{id}'");
        return id;
    }

    public Item GetItem(string id)
    {
        foreach (var item in _items)
        {
            if (item.id == id)
            {
                return item;
            }
        }

        Debug.LogError($"Not exist item '{id}'");
        return null;
    }
}