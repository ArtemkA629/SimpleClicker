using UnityEngine;
using Zenject;

public class LocationsFactory
{
    private readonly LocationItem _itemPrefab;
    private readonly Transform _content;
    private readonly DiContainer _container;
    private readonly LocationsConfig _config;
    
    public LocationsFactory(LocationItem itemPrefab, Transform content, DiContainer container, 
        IConfigProvider configProvider)
    {
        _itemPrefab = itemPrefab;
        _content = content;
        _container = container;
        _config = configProvider.Get<LocationsConfig>();
    }

    public LocationItem[] CreateItems(LocationsDatabase database)
    {
        LocationItem[] items = new LocationItem[_config.LocationsInfo.Length];
        LocationInfo[] locationsInfo = _config.LocationsInfo;
        
        for (int i = 0; i < _config.LocationsInfo.Length; i++)
        {
            var item = _container.InstantiatePrefabForComponent<LocationItem>(_itemPrefab, _content);
            bool isOwned = database.UnlockedLocationNames.Contains(locationsInfo[i].Id);
            item.DisplayInfo(locationsInfo[i].Icon, locationsInfo[i].Id);

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