using System;
using System.Collections.Generic;
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
            string itemName = buildingItem.Name;
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
            if (buildingItem.Name == data.Name)
            {
                buildingItem.UpdateCount(data.Count);
                buildingFound = true;
                break;
            }
        }
        
        if (buildingFound)
            return;
        
        Debug.LogWarning("Building with name " + data.Name + " not found");
    }

    public void UpdateBuildingsPrices(int moneyAmount)
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

    private void UpdatePrice(BuildingItem buildingItem, int moneyAmount)
    {
        BuildingInfo info = _presenter.GetBuildingInfo(buildingItem.Name);
        BuildingData data = _presenter.GetBuildingData(buildingItem.Name);
        int totalPrice = (int)(info.StartPrice * Mathf.Pow(_presenter.BuildingsPriceMultiplier, data.Count));
        buildingItem.UpdatePrice(totalPrice, totalPrice <= moneyAmount);
    }
}