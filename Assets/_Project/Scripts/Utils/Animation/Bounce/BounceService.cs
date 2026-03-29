using DG.Tweening;
using UnityEngine;

public class BounceService
{
    private readonly BounceAnimationConfig _animationConfig;

    private Transform _objectTransform;
    
    public BounceService(IConfigProvider configProvider)
    {
        _animationConfig = configProvider.Get<BounceAnimationConfig>();
    }

    public void SetObjectTransform(Transform objectTransform)
    {
        _objectTransform = objectTransform;
    }

    public void Play()
    {
        _objectTransform.DOKill(true);
        _objectTransform.localScale = Vector3.one;

        _objectTransform.DOPunchScale(
            Vector3.one * _animationConfig.Strength,
            _animationConfig.Duration,
            _animationConfig.Vibrato,     
            _animationConfig.Elasticity    
        );
    }
}