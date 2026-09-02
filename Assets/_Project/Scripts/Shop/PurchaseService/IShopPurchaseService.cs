public interface IShopPurchaseService
{
    void Purchase(ShopItemData shopItemData);
    string GetPriceText(ShopItemData shopItemData);
}