using System.Collections.Generic;
using System;

public class ShopServicesInitializer : IDisposable
{
    private readonly IShopPurchaseService _purchaseService;
    private readonly ShopItemsFactory _itemsFactory;
    
    private List<ShopItem> _createdItems;

    public ShopServicesInitializer(IShopPurchaseService purchaseService, ShopItemsFactory itemsFactory)
    {
        _purchaseService = purchaseService;
        _itemsFactory = itemsFactory;
    }

    public void Initialize()
    {
        _createdItems = _itemsFactory.CreateShopItems();

        foreach (var item in _createdItems)
        {
            item.PurchaseRequested += _purchaseService.Purchase;
        }
    }

    public void Dispose()
    {
        if (_createdItems == null)
            return;

        foreach (var item in _createdItems)
        {
            if (item != null)
                item.PurchaseRequested -= _purchaseService.Purchase;
        }

        _createdItems = null;
    }
}
