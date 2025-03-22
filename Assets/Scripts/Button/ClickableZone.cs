using UnityEngine;

public class ClickableZone : CustomButton
{
    [SerializeField] private ClickerView _clickerView;
    [SerializeField] private Animator _animator;

    protected override void OnClick()
    {
        _clickerView.OnClick();
        _animator.SetTrigger(ClickableZoneAnimatorConstants.ClickedTrigger);
    }
}
