using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class DailyRewardView : IDisposable
{
    private readonly DailyRewardPresenter _presenter;
    private readonly DailyRewardPopup _popup;

    private List<DailyRewardItem> _itemViews = new();
    
    public DailyRewardView(DailyRewardPresenter presenter, DailyRewardPopup popup)
    {
        _presenter = presenter;
        _popup = popup;
    }
    
    public void Initialize(List<DailyRewardItem> items)
    {
        _itemViews = items;
        _presenter.DayRewardClaimed += OnRewardClaimed;
        InitializePopup();
    }

    private void InitializePopup()
    {
        if (_presenter.CanClaimReward(out var rewardData, out int day))
        {
            DailyRewardItem item = _itemViews.First(x => x.Day == day);
            UnityAction claimAction = () => _presenter.ClaimReward(day, item);
            _popup.Display(rewardData.Icon, day, rewardData.RewardDescription, claimAction);
        }
    }
    
    public void Dispose()
    {
        _presenter.DayRewardClaimed -= OnRewardClaimed;
    }

    private void OnRewardClaimed(int day)
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
}
