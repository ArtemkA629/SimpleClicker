using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ShopConfig", fileName = "ShopConfig")]
public class ShopConfig : ScriptableObject
{
    [SerializeField] private List<ShopItemData> _shopItems;
    [SerializeField] private ShopItemPrefabMapping[] _itemPrefabs;

    public List<ShopItemData> ShopItems => _shopItems;

    public ShopItem GetPrefabForType(ShopItemType itemType)
    {
        foreach (var mapping in _itemPrefabs)
        {
            if (mapping.ItemType == itemType)
                return mapping.Prefab;
        }
        
        return null;
    }

    public ShopItemData GetDataByInAppId(string id)
    {
        return _shopItems.Find(x => x.InAppId == id);
    }
}

[Serializable]
public class ShopItemPrefabMapping
{
    public ShopItemType ItemType;
    public ShopItem Prefab;
}

[Serializable]
public class ShopItemData
{
    public ShopItemType ItemType;
    public int Amount;
    public string InAppId;
}

public enum ShopItemType
{
    Coins
}
