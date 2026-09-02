using System;

public class LocationsChangerEventsHandler : IDisposable
{
    private readonly LocationsChanger _locationsChanger;
    private readonly ISaveSystem _saveSystem;
    
    private LocationItem[] _items;
    private LocationsDatabase _database;
    
    public LocationsChangerEventsHandler(LocationsChanger locationsChanger, ISaveSystem saveSystem)
    {
        _locationsChanger = locationsChanger;
        _saveSystem = saveSystem;
    }

    public void Initialize(LocationItem[] items, LocationsDatabase database)
    {
        _items = items;
        _database = database;
        
        _locationsChanger.LocationAdded += OnLocationAdded;
        _locationsChanger.LocationChanged += OnLocationChanged;
    }

    public void Dispose()
    {
        _locationsChanger.LocationAdded -= OnLocationAdded;
        _locationsChanger.LocationChanged -= OnLocationChanged;
    }
    
    private void OnLocationAdded(string id)
    {
        foreach (LocationItem item in _items)
        {
            if (item.Id == id)
            {
                item.DisplayUnlocked();
                break;
            }
        }
        
        _saveSystem.Save(SavingConstants.LocationsId, _database);
    }

    private void OnLocationChanged(string name)
    {
        _saveSystem.Save(SavingConstants.SelectedLocationId, name);
    }
}