using System;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocationItem : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Button _buttonComponent;
    [SerializeField] private GameObject _lockPanel;
    [SerializeField] private TextMeshProUGUI _moneyToUnlockText;

    private string _locationName;
    
    public event Action<string> Clicked;

    private void OnEnable()
    {
        _buttonComponent.onClick.AddListener(OnButtonClicked);
    }
    
    private void OnDisable()
    {
        _buttonComponent.onClick.RemoveListener(OnButtonClicked);
    }

    public void DisplayLocked(BigInteger requiredMoney)
    {
        _lockPanel.SetActive(true);
        _moneyToUnlockText.text = requiredMoney.ToShortValue();
    }
    
    public void DisplayUnlocked()
    {
        _lockPanel.SetActive(false);
    }

    public void DisplayInfo(Sprite icon, string locationName)
    {
        _icon.sprite = icon;
        _nameText.text = locationName;
        _locationName = locationName;
    }
    
    private void OnButtonClicked()
    {
        Clicked?.Invoke(_locationName);
    }
}