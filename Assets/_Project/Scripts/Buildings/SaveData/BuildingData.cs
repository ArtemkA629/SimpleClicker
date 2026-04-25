using System;
using UnityEngine;

[Serializable]
public class BuildingData
{
    [SerializeField] private string _name;
    [SerializeField] private int _count;

    public BuildingData(string name, int count)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.Log("The name is empty");
            return;
        }
        
        _name = name;

        if (count < 0)
        {
            Debug.Log("The count is negative");
            return;
        }
        
        _count = count;
    }

    public void Add(int addingCount = 1)
    {
        if (addingCount < 0)
        {
            Debug.Log("The count is negative");
            return;
        }
        
        _count += addingCount;
    }
    
    public string Name => _name;
    public int Count => _count;
}