using System;
using System.Numerics;
using YG;using Zenject;

public class YGShopPurchaseService : IInitializable, IDisposable, IShopPurchaseService
{
    private readonly MoneyController _moneyController;
    private readonly ShopConfig _shopConfig;

    public YGShopPurchaseService(MoneyController moneyController, IConfigProvider configProvider)
    {
        _moneyController = moneyController;
        _shopConfig = configProvider.Get<ShopConfig>();
    }

    public void Initialize()
    {
        YG2.onPurchaseSuccess += OnPurchaseSuccess;
    }
    
    public void Dispose()
    {
        YG2.onPurchaseSuccess -= OnPurchaseSuccess;
    }
    
    public void Purchase(ShopItemData itemData)
    {
        YG2.BuyPayments(itemData.InAppId);
    }

    public string GetPriceText(ShopItemData itemData)
    {
        return YG2.PurchaseByID(itemData.InAppId)?.price ?? "--";
    }
    
    private void OnPurchaseSuccess(string id)
    {
        ShopItemData itemData = _shopConfig.GetDataByInAppId(id);
        
        switch (itemData.ItemType)
        {
            case ShopItemType.Coins:
                _moneyController.AddMoney(new BigInteger(itemData.Amount));
                break;
        }
    }
}
