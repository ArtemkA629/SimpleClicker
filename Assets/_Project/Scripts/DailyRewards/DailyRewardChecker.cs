using System;

public class DailyRewardChecker
{
    private readonly DailyRewardConfig _config;
    private readonly LastLoginTimeSaver _lastLoginTimeSaver;
    private readonly ISaveSystem _saveSystem;
    
    private DailyRewardSaveData _saveData;
    
    public DailyRewardChecker(IConfigProvider configProvider, LastLoginTimeSaver lastLoginTimeSaver, 
        ISaveSystem saveSystem)
    {
        _config = configProvider.Get<DailyRewardConfig>();
        _lastLoginTimeSaver = lastLoginTimeSaver;
        _saveSystem = saveSystem;
    }

    public void Initialize(DailyRewardSaveData saveData)
    {
        _saveData = saveData;
        CheckDayChange();
    }
    
    public void CheckDayChange()
    {
        DateTime lastClaimTime = _lastLoginTimeSaver.Load();
        DateTime currentTime = DateTime.UtcNow;
        
        if (currentTime.Date > lastClaimTime.Date)
        {
            int daysPassed = (currentTime.Date - lastClaimTime.Date).Days;
            HandleDayChange(daysPassed);
        }
    }
    
    public bool CanClaimReward(int day)
    {
        if (_saveData.IsDayClaimed(day))
            return false;
        
        DateTime lastClaimTime = _lastLoginTimeSaver.Load();
        DateTime currentTime = DateTime.UtcNow;
        int daysPassed = (currentTime.Date - lastClaimTime.Date).Days;
        return daysPassed >= 1 || day == 1;
    }
    
    private void HandleDayChange(int daysPassed)
    {
        if (_saveData.AllRewardsClaimed)
        {
            return;
        }
        
        if (daysPassed >= 2)
        {
            ResetProgress();
        }
        else
        {
            IncrementDay();
        }
        
        Save();
    }
    
    private void ResetProgress()
    {
        _saveData.ResetProgress();
    }
    
    private void IncrementDay()
    {
        if (_saveData.CurrentDay < _config.TotalDays)
        {
            _saveData.IncrementDay();
        }
        else
        {
            _saveData.MarkAllRewardsClaimed();
        }
    }
    
    private void Save()
    {
        _saveSystem.Save(SavingConstants.DailyRewardId, _saveData);
    }
}
