using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Button _buyButton;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _priceText;

    private ShopItemData _itemData;

    public event Action<ShopItemData> PurchaseRequested;

    public void Initialize(ShopItemData itemData, string price)
    {
        _itemData = itemData;
        _titleText.text = itemData.Amount.ToShortValue();
        _priceText.text = price;
        _buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    private void OnBuyButtonClicked()
    {
        PurchaseRequested?.Invoke(_itemData);
    }

    private void OnDestroy()
    {
        _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
    }
}