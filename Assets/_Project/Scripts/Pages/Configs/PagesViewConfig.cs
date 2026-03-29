using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Pages/PagesViewConfig", fileName = "PagesViewConfig")]
public class PagesViewConfig : ScriptableObject
{
    [field: SerializeField] public PageButton PageButtonPrefab { get; private set; }
    [field: SerializeField] public float PageButtonScaleRatio { get; private set; }
    [field: SerializeField] public float PageScaleChangingDuration { get; private set; } = 0.2f;
    [field: SerializeField] public float SnapSpeed { get; private set; } = 10f;
}