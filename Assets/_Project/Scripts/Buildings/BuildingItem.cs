using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildingItem : MonoBehaviour, ICustomButton
{
    [SerializeField] private Button _buttonComponent;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _lock;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _countText;

    public string Name { get; private set; }
    
    public void AddListener(UnityAction action)
    {
        _buttonComponent.onClick.AddListener(action);
    }
    
    public void RemoveListener(UnityAction action)
    {
        _buttonComponent.onClick.RemoveListener(action);
    }
    
    public void SetInfo(Sprite icon, string headerName, int price, bool canBuy, int count)
    {
        _icon.sprite = icon;
        _nameText.text = headerName;
        Name = headerName;
        
        UpdateCount(count);
        UpdatePrice(price, canBuy);
    }

    public void UpdateCount(int count)
    {
        if (count < 0)
        {
            Debug.LogError("Count can't be less than zero");
            return;
        }
        
        _countText.text = count == 0 ? "" : "x" + count;
    }

    public void UpdatePrice(int price, bool canBuy)
    {
        if (price < 0)
        {
            Debug.LogError("Price can't be less than zero");
            return;
        }
        
        _priceText.text = price.ToString();
        UpdateCanBuyState(canBuy);
    }
    
    private void UpdateCanBuyState(bool canBuy)
    {
        _priceText.color = canBuy ? Color.green : Color.red;
        _lock.gameObject.SetActive(canBuy == false);
    }
}