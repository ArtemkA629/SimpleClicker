using UnityEngine;

public class LocationsFactory
{
    private readonly LocationItem _itemPrefab;
    private readonly Transform _content;
    private readonly LocationsConfig _config;
    
    public LocationsFactory(LocationItem itemPrefab, Transform content, IConfigProvider configProvider)
    {
        _itemPrefab = itemPrefab;
        _content = content;
        _config = configProvider.Get<LocationsConfig>();
    }

    public LocationItem[] CreateItems(LocationsDatabase database)
    {
        LocationItem[] items = new LocationItem[_config.LocationsInfo.Length];
        LocationInfo[] locationsInfo = _config.LocationsInfo;
        
        for (int i = 0; i < _config.LocationsInfo.Length; i++)
        {
            LocationItem item = Object.Instantiate(_itemPrefab, _content);
            bool isOwned = database.UnlockedLocationNames.Contains(locationsInfo[i].Name);
            item.DisplayInfo(locationsInfo[i].Icon, locationsInfo[i].Name);

            if (isOwned)
            {
                item.DisplayUnlocked();
            }
            else
            {
                item.DisplayLocked(BigIntegerStatic.Parse(locationsInfo[i].MoneyToUnlock));
            }
            
            items[i] = item;
        }
        
        return items;
    }
}