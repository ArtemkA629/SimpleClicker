using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ImprovementItem : MonoBehaviour, ICustomButton
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Image _lockImage;
    [SerializeField] private Button _buyButton;
    
    public string Name { get; private set; }
    
    public void SetInfo(Sprite icon, string improvementName, string description, int level, int price)
    {
        Name = improvementName;
        _icon.sprite = icon;
        _nameText.text = improvementName;
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
        _levelText.text = level == 0 ? "" : $"Lvl {level}";
    }

    public void UpdatePrice(int price)
    {
        _priceText.text = price.ToString();
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