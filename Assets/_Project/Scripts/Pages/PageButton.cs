using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class PageButton : MonoBehaviour
{
    [NonSerialized] public int Number;
    
    [SerializeField] private RectTransform _rectTransformComponent;
    [SerializeField] private Button _buttonComponent;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _description;

    private PagesViewConfig _pagesViewConfig;
    private Vector3 _initialScale;
    
    [Inject]
    private void Construct(IConfigProvider configProvider)
    {
        _pagesViewConfig = configProvider.Get<PagesViewConfig>();
    }

    public void Initialize()
    {
        _initialScale = _rectTransformComponent.localScale;
    }
    
    public void SetInfo(int number)
    {
        if (number < 1)
        {
            Debug.LogError("Number must be greater than zero");
            return;
        }
        
        Number = number;
    }
    
    public void SetUI(Sprite iconSprite, string description)
    {
        _icon.sprite = iconSprite;
        _description.text = description;
    }

    public void DisplaySelected(bool isSelected)
    {
        _rectTransformComponent.DOKill(true);
        float scaleRatio = isSelected ? _pagesViewConfig.PageButtonScaleRatio : 1f;
        _rectTransformComponent.DOScale(_initialScale * scaleRatio, _pagesViewConfig.PageScaleChangingDuration);
        _description.gameObject.SetActive(isSelected);
    }

    public void AddListener(UnityAction listener)
    {
        _buttonComponent.onClick.AddListener(listener);
    }

    public void RemoveListener(UnityAction listener)
    {
        _buttonComponent.onClick.RemoveListener(listener);
    }
}