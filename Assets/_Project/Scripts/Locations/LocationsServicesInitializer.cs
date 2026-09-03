using UnityEngine;

public class LocationsServicesInitializer
{
    private readonly LocationsChanger _locationsChanger;
    private readonly LocationsChangerEventsHandler _locationsChangerEventsHandler;
    private readonly LocationsFactory _factory;
    private readonly LocationItemsEventsHandler _locationItemsEventsHandler;
    private readonly LocationsDatabase _database;
    private readonly MoneyModel _moneyModel;
    private readonly string _lastOwnedLocationName;
    
    public LocationsServicesInitializer(LocationsChanger locationsChanger, 
        LocationsChangerEventsHandler locationsChangerEventsHandler, LocationsFactory factory,
        LocationItemsEventsHandler locationItemsEventsHandler, IConfigProvider configProvider, 
        MoneyModel moneyModel, ISaveSystem saveSystem)
    {
        _locationsChanger = locationsChanger;
        _locationsChangerEventsHandler = locationsChangerEventsHandler;
        _factory = factory;
        _locationItemsEventsHandler = locationItemsEventsHandler;
        _moneyModel = moneyModel;
        LocationsConfig config = configProvider.Get<LocationsConfig>();
        _database = saveSystem.Load(SavingConstants.LocationsId, config.GetDefaultDatabase());
        Debug.Log(_database == null);
        _lastOwnedLocationName = saveSystem.Load(SavingConstants.SelectedLocationId, _database.UnlockedLocationNames[0]);
    }
        
    public void Initialize()
    {
        _locationsChanger.Initialize(_database, _lastOwnedLocationName);
        LocationItem[] items = _factory.CreateItems(_database);
        _locationsChangerEventsHandler.Initialize(items, _database);
        _locationItemsEventsHandler.Initialize(items);
        _locationsChanger.TryUnlockLocations(_moneyModel.Amount);
    }
}