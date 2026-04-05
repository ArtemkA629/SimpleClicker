using System;
using System.Linq;

public class BuildingsPresenterEventHandler : IDisposable
{
    private readonly BuildingsPresenter _presenter;
    private readonly PassiveIncomeController _passiveIncomeController;
    private readonly BuildingsConfig _buildingsConfig;
    private readonly ISaveSystem _saveSystem;
    
    public BuildingsPresenterEventHandler(BuildingsPresenter presenter, PassiveIncomeController passiveIncomeController, 
        IConfigProvider configProvider, ISaveSystem saveSystem)
    {
        _presenter = presenter;
        _passiveIncomeController = passiveIncomeController;
        _buildingsConfig = configProvider.Get<BuildingsConfig>();
        _saveSystem = saveSystem;
    }

    public void Initialize()
    {
        _presenter.BuildingBought += OnBuildingBought;
    }

    public void Dispose()
    {
        _presenter.BuildingBought -= OnBuildingBought;
    }

    private void OnBuildingBought(BuildingData data)
    {
        BuildingsDatabase buildingsDatabase = _saveSystem.Load(SavingConstants.BoughtBuildingsId, _buildingsConfig.GetDefaultDatabase());
        BuildingData currentData = buildingsDatabase.BuildingsData.FirstOrDefault(d => d.Name == data.Name);
        _passiveIncomeController.UpdateIncome(currentData.Name);
        UpdateSavingData(currentData, buildingsDatabase);
    }

    private void UpdateSavingData(BuildingData buildingData, BuildingsDatabase buildingsDatabase)
    {
        buildingData?.Add();
        _saveSystem.Save(SavingConstants.BoughtBuildingsId, buildingsDatabase);
    }
}