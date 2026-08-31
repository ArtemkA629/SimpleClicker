using System;
using UnityEngine;
using UnityEngine.UI;

public class TemporaryBoost : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    
    private TemporaryBoostController _controller;
    private float _lifetime;
    private bool _isPaused;

    public event Action<TemporaryBoost> TimePassed;
    public event Action<TemporaryBoost> Clicked;
    
    public RectTransform RectTransform => _rectTransform;
    public Image Image => _image;
    
    public void Initialize(TemporaryBoostController controller, float lifetime)
    {
        _controller = controller;
        _lifetime = lifetime;
        _isPaused = false;
        
        _button.onClick.AddListener(OnClicked);
    }
    
    public void PauseTimer()
    {
        _isPaused = true;
    }
    
    public void ResumeTimer()
    {
        _isPaused = false;
    }
    
    private void OnClicked()
    {
        Clicked?.Invoke(this);
    }
    
    private void Update()
    {
        if (_isPaused)
            return;
        
        _lifetime -= Time.deltaTime;
        
        if (_lifetime <= 0f)
        {
            TimePassed?.Invoke(this);
        }
    }
    
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClicked);
    }
}
