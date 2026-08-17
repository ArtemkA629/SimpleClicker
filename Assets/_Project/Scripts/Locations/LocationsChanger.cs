using System;
using System.Numerics;
using UnityEngine;

public class LocationsChanger
{
    private readonly LocationView _view;
    private readonly LocationsConfig _config;
    
    private LocationsDatabase _database;

    public event Action<string> LocationAdded;
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
    
    public void TryUnlockLocations(BigInteger currentMoney)
    {
        LocationInfo locationInfo = _config.GetInfoByMoney(currentMoney);
        
        if (_database.UnlockedLocationNames.Contains(locationInfo.Name) == false)
        {
            foreach (LocationInfo info in _config.LocationsInfo)
            {
                if (_database.UnlockedLocationNames.Contains(info.Name))
                    continue;
                
                _database.UnlockedLocationNames.Add(info.Name);
                LocationAdded?.Invoke(info.Name);
                
                if (info.Name == locationInfo.Name)
                {
                    SetLocation(info.Name);
                    break;
                }
            }
        }
    }

    public void SetLocation(string name)
    {
        Sprite lastOwnedLocationIcon = _config.GetIconByName(name);
        _view.SetLocation(lastOwnedLocationIcon);
        LocationChanged?.Invoke(name);
    }
}