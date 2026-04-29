using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Improvement/ImprovementTypeConfig", fileName = "ImprovementTypeConfig")]
public class ImprovementTypeConfig : ScriptableObject
{
    [field: SerializeField] public ImprovementType Type { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string DescriptionTemplate { get; private set; }
}

public enum ImprovementType
{
    PowerClick,
    OfflineIncome
}