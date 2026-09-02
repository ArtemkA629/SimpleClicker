using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Pages/PagesConfig", fileName = "PagesConfig")]
public class PagesConfig : ScriptableObject
{
    [field: SerializeField] public PageInfo[] PagesInfo { get; private set; }
    [field: SerializeField] public int PageAtStartNumber { get; private set; }

    public PagesDatabase GetDefaultUnlockedPages()
    {
        var database = new PagesDatabase();
        database.UnlockedPages.Add(PagesInfo[PageAtStartNumber - 1].Id);
        return database;
    }
}