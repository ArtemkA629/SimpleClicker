using UnityEngine;
using Zenject;

public class BuildingItemsFactory
{
    private readonly BuildingItem _buildingsItemPrefab;
    private readonly Transform _itemsParent;
    private readonly DiContainer _container;

    private string _buildingName;
    private Sprite _buildingIcon;
    private int _buildingPrice;
    private bool _canBuyBuilding;
    private int _buildingCount;
    
    public BuildingItemsFactory(Transform itemsParent, IConfigProvider configProvider, DiContainer container)
    {
        _itemsParent = itemsParent;
        _buildingsItemPrefab = configProvider.Get<BuildingsConfig>().BuildingItemPrefab;
        _container = container;
    }

    public void SetBuildingInfo(string name, Sprite icon, int price, bool canBuy, int count)
    {
        _buildingName = name;
        _buildingIcon = icon;
        _buildingPrice = price;
        _canBuyBuilding = canBuy;
        _buildingCount = count;
    }
    
    public BuildingItem Create()
    {
        var buildingItem = _container.InstantiatePrefabForComponent<BuildingItem>(_buildingsItemPrefab, _itemsParent);
        buildingItem.SetInfo(_buildingIcon, _buildingName, _buildingPrice, _canBuyBuilding, _buildingCount);
        return buildingItem;
    }
}
