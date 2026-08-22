using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DailyRewardSaveData
{
    [SerializeField] private int _currentDay;
    [SerializeField] private List<int> _claimedDays;
    [SerializeField] private bool _allRewardsClaimed;
    
    public DailyRewardSaveData()
    {
        _currentDay = 1;
        _claimedDays = new();
        _allRewardsClaimed = false;
    }
    
    public int CurrentDay => _currentDay;
    public bool AllRewardsClaimed => _allRewardsClaimed;
    public List<int> ClaimedDays => _claimedDays;
    
    public void IncrementDay()
    {
        _currentDay++;
    }
    
    public bool IsDayClaimed(int day)
    {
        return _claimedDays.Contains(day);
    }
    
    public void MarkDayClaimed(int day)
    {
        _claimedDays.Add(day);
    }
    
    public void ResetProgress()
    {
        _currentDay = 1;
        _claimedDays = new();
    }
    
    public void MarkAllRewardsClaimed()
    {
        _allRewardsClaimed = true;
    }
}
