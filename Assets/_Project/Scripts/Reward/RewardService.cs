using System;
using System.Numerics;
using YG;

public class RewardService : IDisposable
{
    private readonly RewardConfig _config;
    private readonly MoneyController _moneyController;
    private readonly PassiveIncomeModel _passiveIncomeModel;
    private readonly RewardButton[] _rewardButtons;
    private readonly RewardButtonsLifetimeService _lifetimeService;
    
    public RewardService(IConfigProvider configProvider, MoneyController moneyController, 
        PassiveIncomeModel passiveIncomeModel, RewardButton[] rewardButtons, 
        RewardButtonsLifetimeService lifetimeService)
    {
        _config = configProvider.Get<RewardConfig>();
        _moneyController = moneyController;
        _passiveIncomeModel = passiveIncomeModel;
        _rewardButtons = rewardButtons;
        _lifetimeService = lifetimeService;
    }
    
    public void Initialize()
    {
        foreach (RewardButton button in _rewardButtons)
        {
            button.Clicked += OnAdRewardButtonClicked;
        }
        
        YG2.onRewardAdv += OnRewardedAdShowed;
    }

    public void Dispose()
    {
        foreach (RewardButton button in _rewardButtons)
        {
            button.Clicked -= OnAdRewardButtonClicked;
        }
        
        YG2.onRewardAdv -= OnRewardedAdShowed;
    }

    private void OnAdRewardButtonClicked(RewardButton button)
    {
        if (_lifetimeService.IsButtonAvailable(button) == false)
            return;

        YG2.RewardedAdvShow(button.RewardType.ToString());
    }
    
    private void OnRewardedAdShowed(string id)
    {
        RewardButton button = FindButtonByRewardType(id);
        
        if (button == null)
            return;

        switch (id)
        {
            case nameof(AdRewardType.Money):
                BigInteger partOfMoney = _moneyController.Amount * _config.MoneyRewardPercent / 100;
                BigInteger partOfPassiveIncome = _passiveIncomeModel.TotalIncome * _config.MoneyRewardPercent / 100;
                _moneyController.AddMoney(BigIntegerStatic.Max(partOfMoney, partOfPassiveIncome, _config.BaseAddingMoney));
                _lifetimeService.RegisterClaimedReward(button);
                break;
        }
    }

    private RewardButton FindButtonByRewardType(string rewardTypeStr)
    {
        if (Enum.TryParse(rewardTypeStr, out AdRewardType rewardType) == false)
            return null;

        foreach (RewardButton button in _rewardButtons)
        {
            if (button.RewardType == rewardType)
                return button;
        }
        
        return null;
    }
}
