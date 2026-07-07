using System;
using System.Numerics;
using UnityEngine;

public class LocationsChanger
{
    private readonly LocationView _view;
    private readonly LocationsConfig _config;
    
    private LocationsDatabase _database;

    public event Action LocationAdded;
    public event Action<string> LocationChanged;
    
    public LocationsChanger(LocationView view, IConfigProvider configProvider)
    {
        _view = view;
        _config = configProvider.Get<LocationsConfig>();
    }

    public void Initialize(LocationsDatabase database, string lastOwnedLocationName)
    {
        _database = database;
        SetLocation(lastOwnedLocationName);
    }
    
    public void TrySetNewLocation(BigInteger currentMoney)
    {
        LocationInfo locationInfo = _config.GetInfoByMoney(currentMoney, _database.UnlockedLocationNames[^1]);
        
        if (_database.UnlockedLocationNames.Contains(locationInfo.Name) == false)
        {
            foreach (LocationInfo info in _config.LocationsInfo)
            {
                if (info.Name == locationInfo.Name)
                    break;

                if (_database.UnlockedLocationNames.Contains(info.Name))
                    continue;
                
                _database.UnlockedLocationNames.Add(locationInfo.Name);
            }
            
            _database.UnlockedLocationNames.Add(locationInfo.Name);
            SetLocation(locationInfo.Name);
            LocationAdded?.Invoke();
        }
    }

    public void SetLocation(string name)
    {
        Sprite lastOwnedLocationIcon = _config.GetIconByName(name);
        _view.SetLocation(lastOwnedLocationIcon);
        LocationChanged?.Invoke(name);
    }
    
    private void SetLastOwnedLocation()
    {
        string lastOwnedLocationName = _database.UnlockedLocationNames[^1];
        SetLocation(lastOwnedLocationName);
    }
}