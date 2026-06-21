using System;
using System.Numerics;

public class RebirthPresenter
{
    private readonly RebirthModel _model;
    private readonly MoneyController _moneyController;
    private readonly GemsController _gemsController;
    private readonly BuildingsPresenter _buildingsPresenter;
    private readonly ImprovementsPresenter _improvementsPresenter;
    private readonly RebirthConfig _config;

    public event Action RebirthPerformed;

    public RebirthPresenter(RebirthModel model, MoneyController moneyController, GemsController gemsController, 
        BuildingsPresenter buildingsPresenter, ImprovementsPresenter improvementsPresenter, 
        IConfigProvider configProvider)
    {
        _model = model;
        _moneyController = moneyController;
        _gemsController = gemsController;
        _buildingsPresenter = buildingsPresenter;
        _improvementsPresenter = improvementsPresenter;
        _config = configProvider.Get<RebirthConfig>();
    }
    
    public bool TryGetRequiredMoneyForRebirth(out BigInteger price)
    {
        return _config.TryGetRequiredMoneyForLevel(_model.RebirthLevel + 1, out price);
    }

    public bool CanPerformRebirth()
    {
        return TryGetRequiredMoneyForRebirth(out BigInteger price) && _moneyController.Amount >= price;
    }

    public void TryPerformRebirth()
    {
        if (CanPerformRebirth() == false)
            return;

        _model.IncrementRebirthLevel();
        _moneyController.TrySubtractMoney(_moneyController.Amount);
        _gemsController.AddGems(_config.GetGemsRewardByLevel(_model.RebirthLevel));
        _buildingsPresenter.ResetProgress();
        _improvementsPresenter.ResetProgress();
        RebirthPerformed?.Invoke();
    }
}