using UnityEngine;

public class CoinPopupService
{
    private CoinPopupSpawner _spawner;
    private CoinPopupAnimator _animator;
    
    private CoinPopupService(CoinPopupSpawner spawner, CoinPopupAnimator animator)
    {
        _spawner = spawner;
        _animator = animator;
    }
    
    public void AnimateAdding(Vector3 positionToStart)
    {
        Coin coin = _spawner.CreateCoin(positionToStart);
        _animator.AnimateCoin(coin);
    }
}