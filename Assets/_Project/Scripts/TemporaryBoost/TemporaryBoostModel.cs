public class TemporaryBoostModel
{
    private float _remainingBoostTime;
    private bool _isBoostActive;
    
    public bool IsBoostActive => _isBoostActive;
    public float RemainingBoostTime => _remainingBoostTime;
    
    public void ActivateBoost(float duration)
    {
        _remainingBoostTime = duration;
        _isBoostActive = true;
    }
    
    public void DeactivateBoost()
    {
        _remainingBoostTime = 0f;
        _isBoostActive = false;
    }
    
    public void UpdateRemainingTime(float deltaTime)
    {
        if (_isBoostActive)
        {
            _remainingBoostTime -= deltaTime;
            if (_remainingBoostTime <= 0f)
            {
                DeactivateBoost();
            }
        }
    }
}
