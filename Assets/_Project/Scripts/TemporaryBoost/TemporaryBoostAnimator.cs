using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TemporaryBoostAnimator
{
    private readonly TemporaryBoostAnimationInfo _animationInfo;
    
    public TemporaryBoostAnimator(IConfigProvider configProvider)
    {
        var config = configProvider.Get<TemporaryBoostConfig>();
        _animationInfo = config.AnimationInfo;
    }
    
    public void StartAnimations(TemporaryBoost boost, float lifetime)
    {
        RectTransform rectTransform = boost.RectTransform;
        Image image = boost.Image;
        
        InitializeInitialState(rectTransform, image);
        
        Sequence animationSequence = DOTween.Sequence();
        
        AddAppearAnimation(animationSequence, rectTransform, image);
        AddBounceAnimation(animationSequence, rectTransform);
        AddFloatAnimation(animationSequence, rectTransform);
        
        ScheduleDisappearAnimation(rectTransform, image, lifetime);
    }
    
    private void InitializeInitialState(RectTransform rectTransform, Image image)
    {
        rectTransform.localScale = Vector3.zero;
        if (image != null)
        {
            Color color = image.color;
            color.a = 0f;
            image.color = color;
        }
    }
    
    private void AddAppearAnimation(Sequence sequence, RectTransform rectTransform, Image image)
    {
        sequence.Join(rectTransform.DOScale(1f, _animationInfo.FadeDuration)
            .SetEase(Ease.OutBack));
        
        if (image != null)
        {
            sequence.Join(image.DOFade(1f, _animationInfo.FadeDuration));
        }
    }
    
    private void AddBounceAnimation(Sequence sequence, RectTransform rectTransform)
    {
        sequence.Append(rectTransform.DOScale(_animationInfo.BounceScale, _animationInfo.BounceDuration * 0.5f)
            .SetEase(Ease.OutBack));
        sequence.Append(rectTransform.DOScale(1f, _animationInfo.BounceDuration * 0.5f)
            .SetEase(Ease.InOutQuad));
    }
    
    private void AddFloatAnimation(Sequence sequence, RectTransform rectTransform)
    {
        sequence.Append(rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + _animationInfo.FloatAmplitude, _animationInfo.FloatDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo));
    }
    
    private void ScheduleDisappearAnimation(RectTransform rectTransform, Image image, float lifetime)
    {
        DOVirtual.DelayedCall(lifetime - _animationInfo.FadeDuration, () =>
        {
            if (rectTransform != null)
            {
                rectTransform.DOScale(0f, _animationInfo.FadeDuration)
                    .SetEase(Ease.InBack);
                
                if (image != null)
                {
                    image.DOFade(0f, _animationInfo.FadeDuration);
                }
            }
        });
    }
    
    public void StopAnimations(TemporaryBoost boost)
    {
        DOTween.Kill(boost);
    }
}
