using UnityEngine;

public class ClickerPresenter
{
    private readonly ClickerModel _model;

    public ClickerPresenter(ClickerModel model)
    {
        _model = model;
    }

    public void OnClick()
    {
        _model.AddClick();
    }

    public void OnBuyUpgrade(int multiplyRatio, int price, BuyUpgradeButton clickedButton)
    {
        _model.ApplyUpgrade(multiplyRatio, price, clickedButton);
    }

    public void OnLoad(SaveData saveData)
    {
        _model.LoadData(saveData);
    }
}
