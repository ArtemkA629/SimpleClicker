using System;
using System.Collections.Generic;
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
        _presenter.AllBuildingsRemoved += OnAllBuildingsRemoved;
    }

    public void Dispose()
    {
        _presenter.BuildingBought -= OnBuildingBought;
        _presenter.AllBuildingsRemoved -= OnAllBuildingsRemoved;
    }

    private void OnBuildingBought(BuildingData data)
    {
        BuildingsDatabase buildingsDatabase = _saveSystem.Load(SavingConstants.BoughtBuildingsId, _buildingsConfig.GetDefaultDatabase());
        BuildingData currentData = buildingsDatabase.BuildingsData.FirstOrDefault(d => d.ID == data.ID);
        _passiveIncomeController.AddIncome(currentData.ID);
        UpdateSaveDataByAdding(currentData, buildingsDatabase);
    }

    private void OnAllBuildingsRemoved()
    {
        _passiveIncomeController.RemoveAllIncome();
        UpdateSaveDataByRemoving();
    }
    
    private void UpdateSaveDataByAdding(BuildingData buildingData, BuildingsDatabase buildingsDatabase)
    {
        buildingData?.Add();
        _saveSystem.Save(SavingConstants.BoughtBuildingsId, buildingsDatabase);
    }
    
    private void UpdateSaveDataByRemoving()
    {
        _saveSystem.Save(SavingConstants.BoughtBuildingsId, _buildingsConfig.GetDefaultDatabase());
    }
}