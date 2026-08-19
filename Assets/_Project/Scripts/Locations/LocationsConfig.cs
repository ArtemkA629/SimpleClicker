using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[CreateAssetMenu(fileName = "LocationsConfig", menuName = "ScriptableObject/LocationsConfig")]
public class LocationsConfig : ScriptableObject
{
    [field: SerializeField] public LocationInfo[] LocationsInfo { get; private set; }
    
    public BigInteger FirstLocationPrice => BigIntegerStatic.Parse(LocationsInfo[0].MoneyToUnlock);
    
    public LocationsDatabase GetDefaultDatabase()
    {
        return new LocationsDatabase()
        {
            UnlockedLocationNames = new List<string>() { LocationsInfo[0].Name }
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
    
    public Sprite GetIconByName(string locationName)
    {
        return Array.Find(LocationsInfo, info => info.Name == locationName).Icon;
    }
}

[Serializable]
public struct LocationInfo
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string MoneyToUnlock { get; private set; }
}