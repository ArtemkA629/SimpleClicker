public class RebirthModel
{
    private readonly RebirthView _view;
    private readonly RebirthConfig _config;
    
    private RebirthDatabase _database;

    public RebirthModel(RebirthView view, IConfigProvider configProvider, ISaveSystem saveSystem)
    {
        _view = view;
        _config = configProvider.Get<RebirthConfig>();
        _database = saveSystem.Load(SavingConstants.RebirthId, _config.GetDefaultDatabase());
    }

    public int RebirthLevel => _database.RebirthData.RebirthLevel;
    public int CurrentRebirthGemsReward => _config.GetGemsRewardByLevel(RebirthLevel + 1);
    public bool IsLevelMax => _config.IsMaxLevel(RebirthLevel);
    
    public void IncrementRebirthLevel()
    {
        _database.RebirthData.IncrementLevel();
        _view.DisplayCurrentLevel(RebirthLevel);
        
        if (IsLevelMax)
        {
            _view.DisplayMaxLevelMode();
        }
        else
        {
            _view.DisplayGemsReward(CurrentRebirthGemsReward);
        }
    }
}
