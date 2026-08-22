using System;

public class DailyRewardPresenterEventsHandler : IDisposable
{
    private readonly DailyRewardPresenter _presenter;
    private readonly ISaveSystem _saveSystem;
    
    public DailyRewardPresenterEventsHandler(DailyRewardPresenter presenter, ISaveSystem saveSystem)
    {
        _presenter = presenter;
        _saveSystem = saveSystem;
    }
    
    public void Initialize()
    {
        _presenter.DayRewardClaimed += RewardClaimed;
    }

    public void Dispose()
    {
        _presenter.DayRewardClaimed -= RewardClaimed;
    }
    
    private void RewardClaimed(int day)
    {
        _saveSystem.Save(SavingConstants.LastLoginTimeId, DateTime.UtcNow.ToString("o"));
    }
}