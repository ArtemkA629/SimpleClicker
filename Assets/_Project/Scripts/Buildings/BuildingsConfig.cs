using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObject/BuildingsConfig", fileName = "BuildingsConfig")]
public class BuildingsConfig : ScriptableObject
{
    [field : SerializeField] public BuildingInfo[] BuildingsInfo { get; private set; }
    [field: SerializeField] public BuildingItem BuildingItemPrefab { get; private set; }
    [field: SerializeField] public float PriceMultiplier { get; private set; } = 1.15f;

    public BigInteger FirstBuildingPrice => BuildingsInfo[0].StartPrice;
    
    public BuildingInfo GetBuildingInfo(string buildingName)
    {
        return BuildingsInfo.FirstOrDefault(i => i.Id  == buildingName);
    }

    public BuildingsDatabase GetDefaultDatabase()
    {
        var buildingsData = new List<BuildingData>();

        foreach (BuildingInfo info in BuildingsInfo)
        {
            buildingsData.Add(new BuildingData(info.Id, 0));
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
    
    [field: FormerlySerializedAs("<Name>k__BackingField")] 
    [field: SerializeField] public string Id { get; private set; }

    [SerializeField] public string _startPrice;
    [SerializeField] public string _incomePerSecond;
    
    public BigInteger StartPrice => BigIntegerStatic.Parse(_startPrice);
    public BigInteger IncomePerSecond => BigIntegerStatic.Parse(_incomePerSecond);
}