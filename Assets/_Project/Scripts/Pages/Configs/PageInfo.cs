using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Pages/PageInfo", fileName = "PageInfo")]
public class PageInfo : ScriptableObject
{
    [field: SerializeField] public int Number { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
}