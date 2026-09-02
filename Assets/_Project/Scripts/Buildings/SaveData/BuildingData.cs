using System;
using UnityEngine;

[Serializable]
public class BuildingData
{
    [SerializeField] private string _id;
    [SerializeField] private int _count;

    public BuildingData(string id, int count)
    {
        if (string.IsNullOrEmpty(id))
        {
            Debug.Log("The name is empty");
            return;
        }
        
        _id = id;

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

    public void ResetCount()
    {
        _count = 0;
    }
    
    public string ID => _id;
    public int Count => _count;
}