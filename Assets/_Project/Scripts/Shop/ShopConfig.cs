using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Shop/ShopConfig", fileName = "ShopConfig")]
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
}

[System.Serializable]
public class ShopItemPrefabMapping
{
    public ShopItemType ItemType;
    public ShopItem Prefab;
}

[System.Serializable]
public class ShopItemData
{
    public ShopItemType ItemType;
    public int Amount;
}

public enum ShopItemType
{
    Coins
}
