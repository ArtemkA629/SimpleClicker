using System;

public class PagesStateHandler
{
    private const string RebirthId = "Rebirth";
    private const string BuildingsId = "Buildings";
    private const string ImprovementsId = "Improvements";
    private const string LocationsId = "Locations";
    
    private readonly BuildingsConfig _buildingsConfig;
    private readonly ImprovementsConfig _improvementsConfig;
    private readonly LocationsConfig _locationsConfig;
    private readonly RebirthConfig _rebirthConfig;
    private readonly MoneyModel _moneyModel;
    private readonly ISaveSystem _saveSystem;
    
    private PagesDatabase _database;
    
    public event Action<string> OnPageUnlocked;

    public PagesStateHandler(IConfigProvider configProvider, MoneyModel moneyModel, ISaveSystem saveSystem)
    {
        _buildingsConfig = configProvider.Get<BuildingsConfig>();
        _improvementsConfig = configProvider.Get<ImprovementsConfig>();
        _locationsConfig = configProvider.Get<LocationsConfig>();
        _rebirthConfig = configProvider.Get<RebirthConfig>();
        _moneyModel = moneyModel;
        _saveSystem = saveSystem;
    }

    public void Initialize(PagesDatabase database)
    {
        _database = database;
    }
    
    public bool GetPageLockedState(string id)
    {
        bool isLockedByCondition = false;
        
        switch (id)
        {
            case RebirthId:
                isLockedByCondition = _moneyModel.Amount < _rebirthConfig.FirstRebirthPrice;
                break;
            case BuildingsId:
                isLockedByCondition = _moneyModel.Amount < _buildingsConfig.FirstBuildingPrice;
                break;
            case ImprovementsId:
                isLockedByCondition = _moneyModel.Amount < _improvementsConfig.FirstImprovementPrice;
                break; 
            case LocationsId:
                isLockedByCondition = _moneyModel.Amount < _locationsConfig.FirstLocationPrice;
                break;
        }
        
        bool wasUnlockedBefore = _database.Contains(id);
        return isLockedByCondition && wasUnlockedBefore == false;
    }

    public void SaveUnlocked(string id)
    {
        if (_database.Contains(id)) 
            return;
        
        _database.Add(id);
        _saveSystem.Save(SavingConstants.UnlockedPagesId, _database);
        OnPageUnlocked?.Invoke(id);
    }
}