using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class BuildingItem : MonoBehaviour, ICustomButton
{
    [SerializeField] private Button _buttonComponent;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _lock;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _countText;

    private ILocalizationService _localizationService;
    
    public string Id { get; private set; }

    [Inject]
    private void Construct(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    
    public void AddListener(UnityAction action)
    {
        _buttonComponent.onClick.AddListener(action);
    }
    
    public void RemoveListener(UnityAction action)
    {
        _buttonComponent.onClick.RemoveListener(action);
    }
    
    public void SetInfo(Sprite icon, string id, BigInteger price, bool canBuy, int count)
    {
        _icon.sprite = icon;
        _nameText.text = _localizationService.GetText(id);
        Id = id;
        
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
        
        _countText.text = count == 0 ? "" : "x" + count.ToShortValue();
    }

    public void UpdatePrice(BigInteger price, bool canBuy)
    {
        if (price < 0)
        {
            Debug.LogError("Price can't be less than zero");
            return;
        }
        
        _priceText.text = price.ToShortValue();
        UpdateCanBuyState(canBuy);
    }
    
    private void UpdateCanBuyState(bool canBuy)
    {
        _priceText.color = canBuy ? Color.green : Color.red;
        _lock.gameObject.SetActive(canBuy == false);
    }
}