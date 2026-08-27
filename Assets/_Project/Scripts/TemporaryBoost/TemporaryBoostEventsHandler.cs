public class TemporaryBoostEventsHandler
{
    private readonly TemporaryBoostAnimator _animator;

    public TemporaryBoostEventsHandler(TemporaryBoostAnimator animator)
    {
        _animator = animator;
    }

    public void RegisterBoost(TemporaryBoost boost)
    {
        boost.Destroyed += OnBoostDestroyed;
    }

    private void OnBoostDestroyed(TemporaryBoost boost)
    {
        boost.Destroyed -= OnBoostDestroyed;
        _animator.StopAnimations(boost);
    }
}
