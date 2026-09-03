using Zenject;

public class RewardButtonsLifetimeService : ITickable
{
    private readonly RewardConfig _config;
    private readonly RewardCooldownModel _cooldownModel;
    private readonly RewardButton[] _rewardButtons;

    public RewardButtonsLifetimeService(IConfigProvider configProvider, RewardCooldownModel cooldownModel, 
        RewardButton[] rewardButtons)
    {
        _config = configProvider.Get<RewardConfig>();
        _cooldownModel = cooldownModel;
        _rewardButtons = rewardButtons;
    }
    
    public void Tick()
    {
        UpdateAllButtonStates();
    }

    public void RegisterClaimedReward(RewardButton button)
    {
        string buttonId = GetButtonId(button);
        _cooldownModel.UpdateLastRewardTime(buttonId);
        UpdateButtonState(button);
    }

    public bool IsButtonAvailable(RewardButton button)
    {
        string buttonId = GetButtonId(button);
        return _cooldownModel.HasCooldownPassed(buttonId, _config.CooldownMinutes);
    }

    private void UpdateAllButtonStates()
    {
        foreach (RewardButton button in _rewardButtons)
        {
            UpdateButtonState(button);
        }
    }

    private void UpdateButtonState(RewardButton button)
    {
        bool isAvailable = IsButtonAvailable(button);
        button.SetButtonState(isAvailable);
    }

    private string GetButtonId(RewardButton button)
    {
        return button.RewardType.ToString();
    }
}