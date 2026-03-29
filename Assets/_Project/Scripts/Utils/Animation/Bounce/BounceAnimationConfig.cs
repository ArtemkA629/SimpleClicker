using UnityEngine;

[CreateAssetMenu(fileName = "BounceAnimationConfig", menuName = "ScriptableObject/Animation/BounceAnimationConfig")]
public class BounceAnimationConfig : ScriptableObject
{
    [field: SerializeField] public float Duration { get; private set; } = 0.25f;
    [field: SerializeField] public float Strength { get; private set; } = 0.25f;
    [field: SerializeField] public int Vibrato { get; private set; } = 8;
    [field: SerializeField] public float Elasticity { get; private set; } = 0.8f;
}