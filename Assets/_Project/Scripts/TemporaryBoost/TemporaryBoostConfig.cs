using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TemporaryBoost/TemporaryBoostConfig", fileName = "TemporaryBoostConfig")]
public class TemporaryBoostConfig : ScriptableObject
{
    [SerializeField] private float _boostDuration = 30f;
    [SerializeField] private float _boostMultiplier = 2f;
    [SerializeField] private float _spawnIntervalMinutes = 10f;
    [SerializeField] private float _boostLifetime = 5f;
    [SerializeField] private TemporaryBoostAnimationInfo _animationInfo;
    [SerializeField] private TemporaryBoost _boostPrefab;

    public float BoostMultiplier => _boostMultiplier;
    public float BoostDuration => _boostDuration;
    public float SpawnInterval => _spawnIntervalMinutes * 60f;
    public float BoostLifetime => _boostLifetime;
    public TemporaryBoostAnimationInfo AnimationInfo => _animationInfo;
    public TemporaryBoost BoostPrefab => _boostPrefab;
}
