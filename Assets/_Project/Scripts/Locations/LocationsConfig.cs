using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "LocationsConfig", menuName = "ScriptableObject/LocationsConfig")]
public class LocationsConfig : ScriptableObject
{
    [field: SerializeField] public LocationInfo[] LocationsInfo { get; private set; }
    
    public BigInteger FirstLocationPrice => BigIntegerStatic.Parse(LocationsInfo[1].MoneyToUnlock);
    
    public LocationsDatabase GetDefaultDatabase()
    {
        return new LocationsDatabase()
        {
            UnlockedLocationNames = new List<string>() { LocationsInfo[0].Id }
        };
    }

    public LocationInfo GetInfoByMoney(BigInteger money)
    {
        LocationInfo locationInfo = LocationsInfo[0];
        
        foreach (var info in LocationsInfo)
        {
            if (BigIntegerStatic.Parse(info.MoneyToUnlock) <= money)
            {
                locationInfo = info;
            }
            else
            {
                break;
            }
        }
        
        return locationInfo;
    }
    
    public Sprite GetIconById(string locationId)
    {
        return Array.Find(LocationsInfo, info => info.Id == locationId).Icon;
    }
}

[Serializable]
public struct LocationInfo
{
    [field: FormerlySerializedAs("<Name>k__BackingField")] 
    [field: SerializeField] public string Id { get; private set; }
    
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string MoneyToUnlock { get; private set; }
}