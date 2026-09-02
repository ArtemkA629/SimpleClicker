using System.Numerics;

public class PassiveIncomeModel
{
    private readonly BuildingsConfig _buildingsConfig;
    private readonly BuildingsDatabase _buildingsDatabase;
    private readonly GemsModel _gemsModel;
    
    public PassiveIncomeModel(ISaveSystem saveSystem, IConfigProvider configProvider, GemsModel gemsModel)
    {
        _buildingsConfig = configProvider.Get<BuildingsConfig>();
        _buildingsDatabase = saveSystem.Load(SavingConstants.BoughtBuildingsId, _buildingsConfig.GetDefaultDatabase());
        _gemsModel = gemsModel;
    }

    public BigInteger TotalIncome
    {
        get
        {
            BigInteger totalIncome = 0;
            
            foreach (BuildingData data in _buildingsDatabase.BuildingsData)
            {
                totalIncome += _buildingsConfig.GetBuildingInfo(data.ID).IncomePerSecond.Multiply(data.Count);
            }
            
            totalIncome += totalIncome.Multiply(_gemsModel.Amount / 100f);
            
            return totalIncome;
        }
    }

    public void AddBuilding(string buildingName)
    {
        bool buildingFound = false;
        
        foreach (BuildingData data in _buildingsDatabase.BuildingsData)
        {
            if (data.ID == buildingName)
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
    
    public void RemoveAll()
    {
        _buildingsDatabase.Clear();
    }
}