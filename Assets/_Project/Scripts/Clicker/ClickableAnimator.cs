using UnityEngine;

public class ClickableAnimator
{
    private BounceService _bounceService;
    
    public ClickableAnimator(BounceService bounceService)
    {
        _bounceService = bounceService;
    }
    
    public void Play(Transform clickable)
    {
        PlayBounce(clickable);
    }
    
    private void PlayBounce(Transform clickable)
    {
        _bounceService.SetObjectTransform(clickable);
        _bounceService.Play();
    }
}