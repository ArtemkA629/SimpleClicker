using System.Numerics;
using UnityEngine;
using Zenject;

public class BuildingItemsFactory
{
    private readonly BuildingItem _buildingsItemPrefab;
    private readonly Transform _itemsParent;
    private readonly DiContainer _container;

    private string _buildingId;
    private Sprite _buildingIcon;
    private BigInteger _buildingPrice;
    private bool _canBuyBuilding;
    private int _buildingCount;
    
    public BuildingItemsFactory(Transform itemsParent, IConfigProvider configProvider, DiContainer container)
    {
        _itemsParent = itemsParent;
        _buildingsItemPrefab = configProvider.Get<BuildingsConfig>().BuildingItemPrefab;
        _container = container;
    }

    public void SetBuildingInfo(string id, Sprite icon, BigInteger price, bool canBuy, int count)
    {
        _buildingId = id;
        _buildingIcon = icon;
        _buildingPrice = price;
        _canBuyBuilding = canBuy;
        _buildingCount = count;
    }
    
    public BuildingItem Create()
    {
        var buildingItem = _container.InstantiatePrefabForComponent<BuildingItem>(_buildingsItemPrefab, _itemsParent);
        buildingItem.SetInfo(_buildingIcon, _buildingId, _buildingPrice, _canBuyBuilding, _buildingCount);
        return buildingItem;
    }
}
