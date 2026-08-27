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

    public event Action<TemporaryBoost> Destroyed;
    
    public RectTransform RectTransform => _rectTransform;
    public Image Image => _image;
    
    public void Initialize(TemporaryBoostController controller, float lifetime)
    {
        _controller = controller;
        _lifetime = lifetime;
        
        _button.onClick.AddListener(OnClicked);
    }
    
    private void OnClicked()
    {
        _controller.CollectBoost();
        Destroy(gameObject);
    }
    
    private void Update()
    {
        _lifetime -= Time.deltaTime;
        
        if (_lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClicked);
        Destroyed?.Invoke(this);
    }
}
