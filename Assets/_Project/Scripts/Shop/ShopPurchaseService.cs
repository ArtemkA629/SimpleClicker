using System.Numerics;

public class ShopPurchaseService
{
    private readonly MoneyController _moneyController;

    public ShopPurchaseService(MoneyController moneyController)
    {
        _moneyController = moneyController;
    }

    public void Purchase(ShopItemData itemData)
    {
        CompletePurchase(itemData);
    }

    private void CompletePurchase(ShopItemData itemData)
    {
        switch (itemData.ItemType)
        {
            case ShopItemType.Coins:
                _moneyController.AddMoney(new BigInteger(itemData.Amount));
                break;
        }
    }
}
