using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DailyRewardItemsFactory
{
    private readonly DailyRewardConfig _config;
    private readonly DailyRewardItem _itemPrefab;
    private readonly Transform _itemsContainer;
    private readonly DiContainer _container;
    
    private DailyRewardSaveData _saveData;
    
    public DailyRewardItemsFactory(Transform itemsContainer, IConfigProvider configProvider, DiContainer container)
    {
        _itemsContainer = itemsContainer;
        _config = configProvider.Get<DailyRewardConfig>();
        _container = container;
    }
    
    public void Initialize(DailyRewardSaveData saveData)
    {
        _saveData = saveData;
    }
    
    public List<DailyRewardItem> CreateRewardItems()
    {
        List<DailyRewardItem> items = new();
        
        for (int day = 1; day <= _config.TotalDays; day++)
        {
            if (_config.TryGetRewardData(day, out var rewardData))
            {
                DailyRewardItem item = CreateRewardItem(day, rewardData);
                items.Add(item);
            }
        }
        
        return items;
    }
    
    private DailyRewardItem CreateRewardItem(int day, DailyRewardData rewardData)
    {
        DailyRewardItem item = _container.InstantiatePrefabForComponent<DailyRewardItem>(_config.ItemPrefab, _itemsContainer);
        
        Sprite rewardIcon = rewardData.Icon;
        bool isClaimed = day < _saveData.CurrentDay || _saveData.ClaimedDays.Contains(day);
        string description = rewardData.RewardDescription;
        
        item.Initialize(day, description, rewardIcon, isClaimed);
        
        return item;
    }
}
