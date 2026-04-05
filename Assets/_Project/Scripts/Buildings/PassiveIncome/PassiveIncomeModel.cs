public class PassiveIncomeModel
{
    private readonly BuildingsConfig _buildingsConfig;
    private readonly BuildingsDatabase _buildingsDatabase;
    
    public PassiveIncomeModel(ISaveSystem saveSystem, IConfigProvider configProvider)
    {
        _buildingsConfig = configProvider.Get<BuildingsConfig>();
        _buildingsDatabase = saveSystem.Load(SavingConstants.BoughtBuildingsId, _buildingsConfig.GetDefaultDatabase());
    }

    public int TotalIncome
    {
        get
        {
            int totalIncome = 0;
            
            foreach (BuildingData data in _buildingsDatabase.BuildingsData)
            {
                totalIncome += _buildingsConfig.GetBuildingInfo(data.Name).IncomePerSecond * data.Count;
            }
            
            return totalIncome;
        }
    }

    public void AddBuilding(string buildingName)
    {
        bool buildingFound = false;
        
        foreach (BuildingData data in _buildingsDatabase.BuildingsData)
        {
            if (data.Name == buildingName)
            {
                data.Add();
                buildingFound = true;
                break;
            }
        }
        
        if (buildingFound)
            return;
        
        _buildingsDatabase.BuildingsData.Add(new BuildingData(buildingName, 1));
    }
}