using System;
using System.Collections.Generic;
using UnityEngine;

public class RewardCooldownModel
{
    private Dictionary<string, long> _buttonCooldownTimes = new();

    public DateTime GetLastRewardTime(string buttonId)
    {
        if (_buttonCooldownTimes.TryGetValue(buttonId, out long timeTicks) && timeTicks > 0)
            return new DateTime(timeTicks);
        
        return DateTime.MinValue;
    }

    public bool HasCooldownPassed(string buttonId, int cooldownMinutes)
    {
        DateTime lastTime = GetLastRewardTime(buttonId);

        if (lastTime == DateTime.MinValue)
            return true;

        TimeSpan timeSinceLastReward = DateTime.Now - lastTime;
        return timeSinceLastReward >= TimeSpan.FromMinutes(cooldownMinutes);
    }

    public void UpdateLastRewardTime(string buttonId)
    {
        _buttonCooldownTimes[buttonId] = DateTime.Now.Ticks;
    }
}
