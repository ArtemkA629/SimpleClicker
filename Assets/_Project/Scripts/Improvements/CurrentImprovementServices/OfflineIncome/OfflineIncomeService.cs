using System;
using System.Linq;

public class OfflineIncomeService
{
    private readonly LastLoginTimeSaver _lastLoginTimeSaver;
    private readonly MoneyController _moneyController;
    private readonly ImprovementsModel _improvementsModel;
    private readonly ImprovementConfigInfo _offlineIncomeConfigInfo;
    private readonly PassiveIncomeModel _passiveIncomeModel;
    
    public OfflineIncomeService(LastLoginTimeSaver lastLoginTimeSaver, MoneyController moneyController,
        ImprovementsModel improvementsModel, PassiveIncomeModel passiveIncomeModel, IConfigProvider configProvider)
    {
        _lastLoginTimeSaver = lastLoginTimeSaver;
        _moneyController = moneyController;
        _improvementsModel = improvementsModel;
        _passiveIncomeModel = passiveIncomeModel;
        var offlineIncomeConfig = configProvider.Get<ImprovementsConfig>();
        
        _offlineIncomeConfigInfo = offlineIncomeConfig.ImprovementsInfo
            .First(i => i.TypeConfig.Type == ImprovementType.OfflineIncome);
    }

    public void AddOfflineIncome()
    {
        ImprovementSaveData saveData = _improvementsModel
            .GetImprovementData(_offlineIncomeConfigInfo.TypeConfig.Name);
        
        if (saveData == null || saveData.Level == 0)
            return;
        
        DateTime lastLoginDateTime = _lastLoginTimeSaver.Load();
        DateTime currentDateTime = DateTime.UtcNow;
        double daysPast = (currentDateTime - lastLoginDateTime).TotalDays;
        int currentImprovementLevel = saveData.Level;
        var levelInfoConfig = (IImprovementLevelInfoConfig)_offlineIncomeConfigInfo.LevelInfoConfig;
        var levelInfo = (OfflineIncomeLevelInfo)levelInfoConfig.GetLevelInfo(currentImprovementLevel);
        int offlineIncomePercent = levelInfo.TotalIncomePercentPerDay;
        int income = (int)(daysPast * _passiveIncomeModel.TotalIncome * offlineIncomePercent / 100f);
        _moneyController.AddMoney(income);
    }
}