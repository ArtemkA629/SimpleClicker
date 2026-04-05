using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingsServicesInitializer
{
    private readonly BuildingsView _view;
    private readonly BuildingsPresenter _presenter;
    private readonly BuildingItemsFactory _factory;
    private readonly BuildingsPresenterEventHandler _presenterEventsHandler;
    private readonly PassiveIncomeController _passiveIncomeController;
    private readonly BuildingsConfig _config;
    private readonly BuildingsDatabase _buildingsDatabase;
    private readonly int _moneyCount;
    
    public BuildingsServicesInitializer(BuildingsView view, BuildingsPresenter presenter, BuildingItemsFactory factory,
        BuildingsPresenterEventHandler presenterEventsHandler, PassiveIncomeController passiveIncomeController, 
        IConfigProvider configProvider, ISaveSystem saveSystem)
    {
        _view = view;
        _presenter = presenter;
        _factory = factory;
        _presenterEventsHandler = presenterEventsHandler;
        _passiveIncomeController = passiveIncomeController;
        _config = configProvider.Get<BuildingsConfig>();
        _buildingsDatabase = saveSystem.Load(SavingConstants.BoughtBuildingsId, _config.GetDefaultDatabase());
        _moneyCount = saveSystem.Load<int>(SavingConstants.MoneyId);
    }

    public void Initialize()
    {
        List<BuildingItem> buildingItems = CreateBuildingItems();
        _view.Initialize(_presenter, buildingItems);
        _presenterEventsHandler.Initialize();
        _passiveIncomeController.Initialize();
    }

    private List<BuildingItem> CreateBuildingItems()
    {
        var buildingItems = new List<BuildingItem>();
        
        foreach (var buildingInfo in _config.BuildingsInfo)
        {
            BuildingData buildingData = _buildingsDatabase.BuildingsData.FirstOrDefault(d => d.Name == buildingInfo.Name);
            int buildingCount = buildingData == null ? 0 : buildingData.Count;
            int buildingPrice = (int)(buildingInfo.StartPrice * Mathf.Pow(_config.PriceMultiplier, buildingCount));
            bool canBuy = buildingPrice <= _moneyCount;
            
            _factory.SetBuildingInfo(buildingInfo.Name, buildingInfo.Icon, buildingPrice, canBuy, buildingCount);
            BuildingItem buildingItem = _factory.Create();
            buildingItems.Add(buildingItem);
        }
        
        return buildingItems;
    }
}