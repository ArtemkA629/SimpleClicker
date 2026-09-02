using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShopItemsFactory
{
    private readonly Transform _itemsContainer;
    private readonly IShopPurchaseService _shopPurchaseService;
    private readonly ShopConfig _config;
    private readonly DiContainer _container;

    public ShopItemsFactory(Transform itemsContainer, IShopPurchaseService shopPurchaseService,
        IConfigProvider configProvider, DiContainer container)
    {
        _itemsContainer = itemsContainer;
        _shopPurchaseService = shopPurchaseService;
        _config = configProvider.Get<ShopConfig>();
        _container = container;
    }

    public List<ShopItem> CreateShopItems()
    {
        List<ShopItem> items = new();

        foreach (var itemData in _config.ShopItems)
        {
            ShopItem item = CreateShopItem(itemData);
            items.Add(item);
        }

        return items;
    }

    private ShopItem CreateShopItem(ShopItemData itemData)
    {
        ShopItem prefab = _config.GetPrefabForType(itemData.ItemType);
        ShopItem item = _container.InstantiatePrefabForComponent<ShopItem>(prefab, _itemsContainer);
        string price = _shopPurchaseService.GetPriceText(itemData);
        item.Initialize(itemData, price);
        return item;
    }
}
