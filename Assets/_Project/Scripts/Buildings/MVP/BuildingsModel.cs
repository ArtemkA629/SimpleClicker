using System.Collections.Generic;
using System.Linq;

public class BuildingsModel
{
    private readonly BuildingsView _view;
    
    private BuildingsDatabase _buildingsDatabase;
    
    public BuildingsModel(BuildingsView view, ISaveSystem saveSystem, IConfigProvider configProvider)
    {
        _view = view;
        
        var buildingsConfig = configProvider.Get<BuildingsConfig>();
        _buildingsDatabase = saveSystem.Load(SavingConstants.BoughtBuildingsId, buildingsConfig.GetDefaultDatabase());
    }

    public BuildingData GetBuildingData(string buildingName)
    {
        return _buildingsDatabase.BuildingsData.FirstOrDefault(d => d.Name == buildingName);
    }
    
    public void AddBuilding(string buildingName)
    {
        BuildingData buildingData = GetBuildingData(buildingName);

        if (buildingData == null)
        {
            buildingData = new BuildingData(buildingName, 1);
            _buildingsDatabase.BuildingsData.Add(buildingData);
        }
        else
        {
            buildingData.Add();
        }
        
        _view.UpdateBuildingCount(buildingData);
    }
}