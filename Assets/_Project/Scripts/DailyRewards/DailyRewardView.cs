using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

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
    
    public void Initialize(List<DailyRewardItem> items, bool canShowPopup)
    {
        _itemViews = items;
        _presenter.DayRewardClaimed += OnRewardClaimed;
        
        if (canShowPopup == false)
            return;
        
        ShowClaimPopup();
    }

    public void Dispose()
    {
        _presenter.DayRewardClaimed -= OnRewardClaimed;
    }
    
    public void ShowClaimPopup()
    {
        if (_presenter.CanClaimReward(out var rewardData, out int day))
        {
            DailyRewardItem item = _itemViews.First(x => x.Day == day);
            UnityAction claimAction = () => _presenter.ClaimReward(day, item);
            _popup.Display(rewardData.Icon, day, rewardData.RewardDescription, claimAction);
        }
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
