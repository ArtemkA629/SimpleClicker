using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Clickable : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool _isAnimated;
    
    private ClickableAnimator _animator;

    public event Action<PointerEventData> Clicked;
    
    [Inject]
    private void Construct(ClickableAnimator animator)
    {
        _animator = animator;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isAnimated)
        {
            _animator.Play(transform);
        }
        
        Clicked?.Invoke(eventData);
    }
}
