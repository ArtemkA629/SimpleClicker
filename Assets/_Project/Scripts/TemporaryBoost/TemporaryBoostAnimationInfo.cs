using System;
using UnityEngine;

[Serializable]
public class TemporaryBoostAnimationInfo
{
    [SerializeField] private float _bounceScale = 1.2f;
    [SerializeField] private float _bounceDuration = 0.5f;
    [SerializeField] private float _fadeDuration = 1f;
    [SerializeField] private float _floatAmplitude = 20f;
    [SerializeField] private float _floatDuration = 1f;
    
    public float BounceScale => _bounceScale;
    public float BounceDuration => _bounceDuration;
    public float FadeDuration => _fadeDuration;
    public float FloatAmplitude => _floatAmplitude;
    public float FloatDuration => _floatDuration;
}
