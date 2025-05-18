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

    public void OnBuyUpgrade(int multiplyRatio, BuyUpgradeButton clickedButton)
    {
        _model.ApplyUpgrade(multiplyRatio, clickedButton);
    }
}
