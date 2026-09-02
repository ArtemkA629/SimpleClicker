using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class BuildingsView : IDisposable
{
    private BuildingsPresenter _presenter;
    
    private Dictionary<BuildingItem, UnityAction> _buildingItemsHandlers = new();
    
    public void Initialize(BuildingsPresenter presenter, List<BuildingItem> buildingItems)
    {
        _presenter = presenter;
        
        foreach (BuildingItem buildingItem in buildingItems)
        {
            string itemName = buildingItem.Id;
            UnityAction handler = () => OnItemClicked(itemName);
            _buildingItemsHandlers[buildingItem] = handler;
            buildingItem.AddListener(handler);
        }
    }

    public void Dispose()
    {
        foreach (var pair in _buildingItemsHandlers)
        {
            pair.Key.RemoveListener(pair.Value);
        }

        _buildingItemsHandlers.Clear();
    }
    
    public void UpdateBuildingCount(BuildingData data)
    {
        bool buildingFound = false;
        
        foreach (var (buildingItem, action) in _buildingItemsHandlers)
        {
            if (buildingItem.Id == data.ID)
            {
                buildingItem.UpdateCount(data.Count);
                buildingFound = true;
                break;
            }
        }
        
        if (buildingFound)
            return;
        
        Debug.LogWarning("Building with name " + data.ID + " not found");
    }

    public void UpdateBuildingsPrices(BigInteger moneyAmount)
    {
        foreach (var (buildingItem, action) in _buildingItemsHandlers)
        {
            UpdatePrice(buildingItem, moneyAmount);
        }
    }
    
    private void OnItemClicked(string buildingName)
    {
        _presenter.TryBuyBuilding(buildingName);
    }

    private void UpdatePrice(BuildingItem buildingItem, BigInteger moneyAmount)
    {
        BuildingInfo info = _presenter.GetBuildingInfo(buildingItem.Id);
        BuildingData data = _presenter.GetBuildingData(buildingItem.Id);
        BigInteger totalPrice = info.StartPrice.Multiply(Mathf.Pow(_presenter.BuildingsPriceMultiplier, data.Count));
        buildingItem.UpdatePrice(totalPrice, totalPrice <= moneyAmount);
    }
}