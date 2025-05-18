using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class BuyStyleButton : CustomButton<int>
{
    private static Color _disabledButtonColor = new(1f, 0f, 0f);
    private static Color _enabledButtonColor = new(0f, 1f, 0f);
    private static string _activeText = "Active";
    private static string _inactiveText = "Inactive";
    private static BuyStyleButton _activeButton;

    [SerializeField] private Coins _coins;
    [SerializeField] private Image _rainDropImage;
    [SerializeField] private Image _background;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private Image _buttonImageComponent;
    [SerializeField] private Button _buttonComponent;
    [SerializeField] private Sprite _backgroundSprite;
    [SerializeField] private Sprite _rainDropSprite;
    [SerializeField] private GameObject _rainDrops;
    [SerializeField] private int _number;

    public bool _bought;

    public event Action<int> Bought;
    public event Action<int> Chosen;

    protected override void OnClick()
    {
        if (!_bought)
        {
            if (_coins.CanPay(_argument) == false)
                return;

            _coins.PayUpgrade(_argument);
            _bought = true;
            Bought?.Invoke(_number);
        }

        ActivateStyle();
    }

    public void MakeInactive()
    {
        _buttonImageComponent.color = _disabledButtonColor;
        _buttonText.text = _inactiveText;
        _buttonComponent.enabled = true;
    }

    public void ActivateStyle()
    {
        _background.sprite = _backgroundSprite;
        _rainDropImage.sprite = _rainDropSprite;
        _buttonImageComponent.color = _enabledButtonColor;
        _buttonText.text = _activeText;
        _buttonComponent.enabled = false;

        _activeButton?.MakeInactive();
        _activeButton = this;

        foreach (var drop in _rainDrops.GetComponentsInChildren<Image>(true))
            drop.sprite = _rainDropSprite;
        Chosen?.Invoke(_number);
    }

    public void InactivateStyle()
    {
        _buttonImageComponent.color = _disabledButtonColor;
        _buttonText.text = _inactiveText;
        _buttonComponent.enabled = true;
    }

    public void SetBoughtState(bool bought, List<int> boughtStyles)
    {
        bool isNumberInBoughtStyles = boughtStyles.Contains(_number);
        if ((!isNumberInBoughtStyles && bought) || (isNumberInBoughtStyles && !bought))
            throw new Exception("Invalid bought state");

        _bought = bought;
    }
}
