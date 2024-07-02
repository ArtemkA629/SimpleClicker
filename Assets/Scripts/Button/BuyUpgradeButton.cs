using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class BuyUpgradeButton : CustomButton<int, int>
{
    private static Color _disabledButtonColor = new(244f, 244f, 244f);

    [SerializeField] private ClickerView _clickerView;
    [SerializeField] private Image _buttonImageComponent;
    [SerializeField] private Button _buttonComponent;
    [SerializeField] private int _number;

    public event Action<int> Disabled;

    protected override void OnClick()
    {
        _clickerView.OnBuyUpgrade(_argument1, _argument2, this);
    }

    public void Disable()
    {
        _buttonImageComponent.color = _disabledButtonColor;
        _buttonComponent.enabled = false;
        Disabled?.Invoke(_number);
    }
}
