using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class ImprovementItem : MonoBehaviour, ICustomButton
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Image _lockImage;
    [SerializeField] private Button _buyButton;

    private ILocalizationService _localizationService;
    
    public string Id { get; private set; }

    [Inject]
    private void Construct(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    
    public void SetInfo(Sprite icon, string id, string description, int level, BigInteger price)
    {
        Id = id;
        _icon.sprite = icon;
        _nameText.text = _localizationService.GetText(id);
        UpdateDescription(description);
        UpdateLevel(level);
        UpdatePrice(price);
    }

    public void UpdateDescription(string description)
    {
        _descriptionText.text = description;
    }

    public void UpdateLevel(int level)
    {
        _levelText.text = level == 0 ? "" : $"{_localizationService.GetText(LocalizationConstants.Level)} {level}";
    }

    public void UpdatePrice(BigInteger price)
    {
        _priceText.text = price.ToShortValue();
    }

    public void UpdateCanBuyState(bool canBuy)
    {
        _priceText.color = canBuy ? Color.green : Color.red;
        _lockImage.gameObject.SetActive(canBuy == false);
    }

    public void UpdateMaxLevelReachedState()
    {
        _priceText.text = ImprovementsConstants.MaxLevelReachedText;
        _priceText.color = Color.red;
        _lockImage.gameObject.SetActive(true);
    }
    
    public void AddListener(UnityAction action)
    {
        _buyButton.onClick.AddListener(action);
    }

    public void RemoveListener(UnityAction action)
    {
        _buyButton.onClick.RemoveListener(action);
    }
}