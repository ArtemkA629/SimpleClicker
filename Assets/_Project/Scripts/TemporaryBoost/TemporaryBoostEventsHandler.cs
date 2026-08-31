using Object = UnityEngine.Object;

public class TemporaryBoostEventsHandler
{
    private readonly TemporaryBoostAnimator _animator;

    public TemporaryBoostEventsHandler(TemporaryBoostAnimator animator)
    {
        _animator = animator;
    }

    public void RegisterBoost(TemporaryBoost boost)
    {
        boost.TimePassed += OnBoostTimePassed;
    }
    
    public void UnregisterBoost(TemporaryBoost boost)
    {
        boost.TimePassed -= OnBoostTimePassed;
    }

    private void OnBoostTimePassed(TemporaryBoost boost)
    {
        boost.TimePassed -= OnBoostTimePassed;
        _animator.StopAnimations(boost);
        Object.Destroy(boost.gameObject);
    }
    
    public void DestroyBoost(TemporaryBoost boost)
    {
        boost.TimePassed -= OnBoostTimePassed;
        _animator.StopAnimations(boost);
        Object.Destroy(boost.gameObject);
    }
}
