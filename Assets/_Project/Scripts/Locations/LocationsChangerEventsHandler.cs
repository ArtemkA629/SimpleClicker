using System;

public class LocationsChangerEventsHandler : IDisposable
{
    private readonly LocationsChanger _locationsChanger;
    private readonly ISaveSystem _saveSystem;
    
    private LocationsDatabase _database;
    
    public LocationsChangerEventsHandler(LocationsChanger locationsChanger, ISaveSystem saveSystem)
    {
        _locationsChanger = locationsChanger;
        _saveSystem = saveSystem;
    }

    public void Initialize(LocationsDatabase database)
    {
        _database = database;
        _locationsChanger.LocationAdded += OnLocationAdded;
        _locationsChanger.LocationChanged += OnLocationChanged;
    }

    public void Dispose()
    {
        _locationsChanger.LocationAdded -= OnLocationAdded;
        _locationsChanger.LocationChanged -= OnLocationChanged;
    }
    
    private void OnLocationAdded()
    {
        _saveSystem.Save(SavingConstants.LocationsId, _database);
    }

    private void OnLocationChanged(string name)
    {
        _saveSystem.Save(SavingConstants.SelectedLocationId, name);
    }
}