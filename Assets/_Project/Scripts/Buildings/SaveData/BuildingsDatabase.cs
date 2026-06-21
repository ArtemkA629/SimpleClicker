using System;
using System.Collections.Generic;

[Serializable]
public class BuildingsDatabase
{
    public List<BuildingData> BuildingsData;

    public void Clear()
    {
        foreach (var data in BuildingsData)
        {
            data.ResetCount();
        }
    }
}