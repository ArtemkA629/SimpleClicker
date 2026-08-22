using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class DailyRewardView : IDisposable
{
    private DailyRewardPresenter _presenter;
    private List<DailyRewardItem> _itemViews = new();
    
    public DailyRewardView(DailyRewardPresenter presenter)
    {
        _presenter = presenter;
    }
    
    public void Initialize(List<DailyRewardItem> items)
    {
        ClearItems();
        _itemViews = items;
        
        foreach (var item in _itemViews)
        {
            item.SetClaimAction(() => OnRewardClaimButtonClicked(item));
        }
        
        _presenter.DayRewardClaimed += RewardClaimed;
    }
    
    public void Dispose()
    {
        _presenter.DayRewardClaimed -= RewardClaimed;
    }
    
    private void OnRewardClaimButtonClicked(DailyRewardItem item)
    {
        _presenter.ClaimReward(item.Day, item);
    }

    private void RewardClaimed(int day)
    {
        foreach (var item in _itemViews)
        {
            if (item.Day == day)
            {
                item.SetClaimed();
                break;
            }
        }
    }
    
    private void ClearItems()
    {
        foreach (var itemView in _itemViews)
        {
            if (itemView != null)
            {
                Object.Destroy(itemView.gameObject);
            }
        }
        
        _itemViews.Clear();
    }
}
