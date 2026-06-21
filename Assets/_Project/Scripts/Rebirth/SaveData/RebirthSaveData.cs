using System;
using UnityEngine;

[Serializable]
public class RebirthSaveData
{
    [SerializeField] private int _rebirthLevel;

    public RebirthSaveData(int rebirthLevel = 0)
    {
        if (rebirthLevel < 0)
        {
            Debug.LogError("Rebirth level cannot be negative");
            _rebirthLevel = 0;
            return;
        }
        
        _rebirthLevel = rebirthLevel;
    }

    public void IncrementLevel()
    {
        _rebirthLevel++;
    }
    
    public int RebirthLevel => _rebirthLevel;
}
