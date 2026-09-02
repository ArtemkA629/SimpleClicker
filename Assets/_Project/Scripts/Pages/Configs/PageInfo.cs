using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObject/Pages/PageInfo", fileName = "PageInfo")]
public class PageInfo : ScriptableObject
{
    [field: SerializeField] public int Number { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    
    [field: FormerlySerializedAs("<Description>k__BackingField")] 
    [field: SerializeField] public string Id { get; private set; }
    
    [field: FormerlySerializedAs("<TutorialDescription>k__BackingField")] 
    [field: SerializeField] public string TutorialDescriptionId { get; private set; }
}