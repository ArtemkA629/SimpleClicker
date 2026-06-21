using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CoinAnimationConfig", fileName = "CoinAnimationConfig")]
public class CoinAnimationConfig : ScriptableObject
{
    [field: SerializeField] public Coin CoinPrefab;
    [field: SerializeField] public float Duration = 0.8f;
    [field: SerializeField] public float DelayBeforeStart = 0f;
    [field: SerializeField] public float ArcHeight = 150f;
    [field: SerializeField] public float ArcSpread = 80f;
    [field: SerializeField] public bool PunchOnSpawn = true;
    [field: SerializeField] public float PunchDuration = 0.2f;
    [field: SerializeField] public bool FadeIn = true;
    [field: SerializeField] public float FadeInDuration = 0.15f;
}