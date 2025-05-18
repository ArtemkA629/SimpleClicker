using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class BuyUpgradeButton : CustomButton<int, int>
{
    private static Color _disabledButtonColor = new(244f, 244f, 244f);
    private static Color _enabledButtonColor = new(0.07f, 1f, 0f);

    [SerializeField] private Coins _coins;
    [SerializeField] private ClickerView _clickerView;
    [SerializeField] private Image _buttonImageComponent;
    [SerializeField] private Button _buttonComponent;
    [SerializeField] private int _number;

    public event Action<int> Disabled;

    protected override void OnClick()
    {
        if (_coins.CanPay(_argument2) == false)
            return;

        _coins.PayUpgrade(_argument2);
        _clickerView.OnBuyUpgrade(_argument1, this);
    }

    public void Enable()
    {
        _buttonImageComponent.color = _enabledButtonColor;
        _buttonComponent.enabled = true;
    }

    public void Disable()
    {
        _buttonImageComponent.color = _disabledButtonColor;
        _buttonComponent.enabled = false;
        Disabled?.Invoke(_number);
    }
}
