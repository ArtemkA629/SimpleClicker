using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class Popup : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _rectTransform;

    [Header("Animation Settings")]
    [SerializeField] private AnimationType _animationType = AnimationType.Fade;
    [SerializeField] private float _duration = 0.3f;
    [SerializeField] private AnimationCurve _animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private bool _startHidden;

    [Header("Events")]
    [SerializeField] private UnityEvent _onShowStart;
    [SerializeField] private UnityEvent _onShowComplete;
    [SerializeField] private UnityEvent _onHideStart;
    [SerializeField] private UnityEvent _onHideComplete;
    
    private bool _isHidden;
    private Vector2 _originalPosition;
    private Vector3 _originalScale;
    
    public bool IsHidden => _isHidden;

    public enum AnimationType
    {
        Fade,
        Scale,
        SlideFromTop,
        SlideFromBottom,
        SlideFromLeft,
        SlideFromRight,
    }

    private void Awake()
    {
        _originalPosition = _rectTransform.anchoredPosition;
        _originalScale = _rectTransform.localScale;

        if (_startHidden)
        {
            Hide();
        }
    }

    public void Show()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        StopAllCoroutines();
        StartCoroutine(ShowRoutine());
    }

    public void Hide()
    {
        if (_isHidden)
            return;

        StopAllCoroutines();
        StartCoroutine(HideRoutine());
    }

    public void Toggle()
    {
        if (_canvasGroup.alpha > 0.5f)
            Hide();
        else
            Show();
    }

    private IEnumerator ShowRoutine()
    {
        _onShowStart?.Invoke();

        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        Vector2 startPosition = GetStartPosition(_animationType, true);
        Vector3 startScale = _animationType == AnimationType.Scale ? Vector3.zero : _originalScale;

        _rectTransform.anchoredPosition = startPosition;
        _rectTransform.localScale = startScale;
        _rectTransform.gameObject.SetActive(true);

        float elapsed = 0;

        while (elapsed < _duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = _animationCurve.Evaluate(elapsed / _duration);

            _canvasGroup.alpha = t;

            if (_animationType == AnimationType.Scale)
            {
                _rectTransform.localScale = Vector3.Lerp(startScale, _originalScale, t);
            }
            else if (_animationType != AnimationType.Fade)
            {
                _rectTransform.anchoredPosition = Vector2.Lerp(startPosition, _originalPosition, t);
            }

            yield return null;
        }

        _canvasGroup.alpha = 1;
        _rectTransform.anchoredPosition = _originalPosition;
        _rectTransform.localScale = _originalScale;

        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        _isHidden = false;
        _onShowComplete?.Invoke();
    }

    private IEnumerator HideRoutine()
    {
        _onHideStart?.Invoke();

        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        Vector2 endPosition = GetStartPosition(_animationType, false);
        Vector3 endScale = _animationType == AnimationType.Scale ? Vector3.zero : _originalScale;

        float elapsed = 0;

        while (elapsed < _duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = _animationCurve.Evaluate(elapsed / _duration);

            _canvasGroup.alpha = 1 - t;

            if (_animationType == AnimationType.Scale)
            {
                _rectTransform.localScale = Vector3.Lerp(_originalScale, endScale, t);
            }
            else if (_animationType != AnimationType.Fade)
            {
                _rectTransform.anchoredPosition = Vector2.Lerp(_originalPosition, endPosition, t);
            }

            yield return null;
        }

        _canvasGroup.alpha = 0;
        _isHidden = true;
        _onHideComplete?.Invoke();
        _rectTransform.gameObject.SetActive(false);
    }

    private Vector2 GetStartPosition(AnimationType type, bool isShowing)
    {
        Rect rect = _rectTransform.rect;
        Canvas canvas = GetComponentInParent<Canvas>();
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        switch (type)
        {
            case AnimationType.SlideFromTop:
                return isShowing ?
                    new Vector2(_originalPosition.x, canvasRect.rect.height + rect.height / 2) :
                    new Vector2(_originalPosition.x, -canvasRect.rect.height - rect.height / 2);

            case AnimationType.SlideFromBottom:
                return isShowing ?
                    new Vector2(_originalPosition.x, -canvasRect.rect.height - rect.height / 2) :
                    new Vector2(_originalPosition.x, canvasRect.rect.height + rect.height / 2);

            case AnimationType.SlideFromLeft:
                return isShowing ?
                    new Vector2(-canvasRect.rect.width - rect.width / 2, _originalPosition.y) :
                    new Vector2(canvasRect.rect.width + rect.width / 2, _originalPosition.y);

            case AnimationType.SlideFromRight:
                return isShowing ?
                    new Vector2(canvasRect.rect.width + rect.width / 2, _originalPosition.y) :
                    new Vector2(-canvasRect.rect.width - rect.width / 2, _originalPosition.y);

            default:
                return _originalPosition;
        }
    }
}