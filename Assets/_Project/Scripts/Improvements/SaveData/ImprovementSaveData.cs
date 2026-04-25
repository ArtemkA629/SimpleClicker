using System;
using UnityEngine;

[Serializable]
public class ImprovementSaveData
{
    [SerializeField] private string _name;
    [SerializeField] private int _level;
    
    public ImprovementSaveData(string name, int level)
    {
        _name = name;
        
        if (level < 0)
        {
            Debug.Log("Improvement level cannot be negative");
            return;
        }
        
        _level = level;
    }

    public ImprovementSaveData(string name)
    {
        _name = name;
    }

    public void AddLevel()
    {
        _level++;
    }
    
    public string Name => _name;
    public int Level => _level;
}