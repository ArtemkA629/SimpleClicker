using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObject/Improvement/ImprovementTypeConfig", fileName = "ImprovementTypeConfig")]
public class ImprovementTypeConfig : ScriptableObject
{
    [field: SerializeField] public ImprovementType Type { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    
    [field: FormerlySerializedAs("<Name>k__BackingField")] 
    [field: SerializeField] public string Id { get; private set; }
    
    [field: FormerlySerializedAs("<DescriptionTemplate>k__BackingField")] 
    [field: SerializeField] public string DescriptionTemplateId { get; private set; }
}

public enum ImprovementType
{
    PowerClick,
    OfflineIncome
}