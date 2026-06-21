using DG.Tweening;
using UnityEngine;

public class CoinPopupAnimator
{
    private readonly CoinAnimationConfig _coinAnimationConfig;
    private readonly RectTransform _coinTarget;
    
    public CoinPopupAnimator(IConfigProvider configProvider, RectTransform coinTarget)
    {
        _coinAnimationConfig = configProvider.Get<CoinAnimationConfig>();
        _coinTarget = coinTarget;
    }
    
    public void AnimateCoin(Coin coin)
    {
        TryPunchCoin(coin);
        TryFadeInCoin(coin);
 
        Vector2 startPos = coin.RectTransform.anchoredPosition;
        Vector2 endPos = coin.RectTransform.GetTargetAnchoredPos(_coinTarget);
        Vector2 mid = (startPos + endPos) * 0.5f;
        Vector2 controlPoint = mid + new Vector2(
            Random.Range(-_coinAnimationConfig.ArcSpread, _coinAnimationConfig.ArcSpread),
            _coinAnimationConfig.ArcHeight
        );
 
        float punchRatio = 0.5f;
        
        DOVirtual.Float(0f, 1f, _coinAnimationConfig.Duration, t =>
            {
                float u = 1f - t;
                Vector2 pos = u * u * startPos
                              + 2f * u * t * controlPoint
                              + t * t * endPos;
                coin.RectTransform.anchoredPosition = pos;
            })
            .SetEase(Ease.InOutQuad)
            .SetDelay(_coinAnimationConfig.DelayBeforeStart + 
                      (_coinAnimationConfig.PunchOnSpawn ? _coinAnimationConfig.PunchDuration * punchRatio : 0f))
            .OnComplete(() =>
            {
                PlayArriveEffect(_coinTarget, coin);
            });
    }

    private void TryPunchCoin(Coin coin)
    {
        if (_coinAnimationConfig.PunchOnSpawn == false)
            return;
        
        coin.RectTransform.localScale = Vector3.zero;
        coin.RectTransform.DOScale(Vector3.one, _coinAnimationConfig.PunchDuration)
            .SetEase(Ease.OutBack)
            .SetDelay(_coinAnimationConfig.DelayBeforeStart);
    }

    private void TryFadeInCoin(Coin coin)
    {
        if (_coinAnimationConfig.FadeIn == false)
            return;
            
        coin.CanvasGroup.alpha = 0f;
        coin.CanvasGroup.DOFade(1f, _coinAnimationConfig.FadeInDuration)
            .SetDelay(_coinAnimationConfig.DelayBeforeStart);
    }
    
    private void PlayArriveEffect(RectTransform target, Coin coin)
    {
        float punchScaleRatio = 0.25f;
        float punchDuration = 0.3f;
        
        if (target != null)
        {
            target.DOKill(true);
            target.DOPunchScale(Vector3.one * punchScaleRatio, punchDuration);
        }

        float fadeDuration = 0.15f;
        coin.CanvasGroup.DOFade(0f, fadeDuration)
            .OnComplete(() =>
            {
                Object.Destroy(coin.gameObject);
            });
    }
}