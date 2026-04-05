using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/BuildingsConfig", fileName = "BuildingsConfig")]
public class BuildingsConfig : ScriptableObject
{
    [field : SerializeField] public BuildingInfo[] BuildingsInfo { get; private set; }
    [field: SerializeField] public BuildingItem BuildingItemPrefab { get; private set; }
    [field: SerializeField] public float PriceMultiplier { get; private set; } = 1.15f;

    public BuildingInfo GetBuildingInfo(string buildingName)
    {
        return BuildingsInfo.FirstOrDefault(i => i.Name  == buildingName);
    }

    public BuildingsDatabase GetDefaultDatabase()
    {
        var buildingsData = new List<BuildingData>();

        foreach (BuildingInfo info in BuildingsInfo)
        {
            buildingsData.Add(new BuildingData(info.Name, 0));
        }

        var dataBase = new BuildingsDatabase();
        dataBase.BuildingsData = buildingsData;
        return dataBase;
    }
}

[Serializable]
public class BuildingInfo
{
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int StartPrice { get; private set; }
    [field: SerializeField] public int IncomePerSecond { get; private set; }
}