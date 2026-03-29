using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Pages/PagesConfig", fileName = "PagesConfig")]
public class PagesConfig : ScriptableObject
{
    [field: SerializeField] public PageInfo[] PagesInfo { get; private set; }
    [field: SerializeField] public int PageAtStartNumber { get; private set; }
}

[Serializable]
public class PageInfo
{
    [field: SerializeField] public int Number { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
}