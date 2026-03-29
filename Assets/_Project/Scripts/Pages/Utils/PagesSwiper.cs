using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using Zenject;

[RequireComponent(typeof(ScrollRect))]
public class PagesSwiper : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private EventSystem _eventSystem;

    private int _pageCount;
    private int _currentPage;
    private float[] _pagePositions;
    private Vector2 _startDragPosition;
    private PagesViewConfig _pagesViewConfig;
    private PagesPresenter _presenter;

    [Inject]
    private void Construct(IConfigProvider configProvider, PagesPresenter presenter)
    {
        _pagesViewConfig = configProvider.Get<PagesViewConfig>();
        _presenter = presenter;
    }
    
    public void Initialize()
    {
        _pageCount = _scrollRect.content.childCount;
        _pagePositions = new float[_pageCount];
        float step = 1f / (_pageCount - 1);

        for (int i = 0; i < _pageCount; i++)
        {
            _pagePositions[i] = step * i;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startDragPosition = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float delta = eventData.position.x - _startDragPosition.x;

        if (Mathf.Abs(delta) > _eventSystem.pixelDragThreshold)
        {
            if (delta < 0)
            {
                _currentPage++; 
            }
            else
            {
                _currentPage--;
            }
        }

        _currentPage = Mathf.Clamp(_currentPage, 1, _pageCount);
        _presenter.SelectPage(_currentPage);
    }

    public void GoToPage(int number, bool instant = false)
    {
        float target = _pagePositions[number - 1];

        if (instant)
        {
            _scrollRect.horizontalNormalizedPosition = target;
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(SmoothMove(target));
        }
    }

    private IEnumerator SmoothMove(float target)
    {
        while (Mathf.Abs(_scrollRect.horizontalNormalizedPosition - target) > 0.001f)
        {
            _scrollRect.horizontalNormalizedPosition =
                Mathf.Lerp(_scrollRect.horizontalNormalizedPosition, target, Time.deltaTime * _pagesViewConfig.SnapSpeed);

            yield return null;
        }

        _scrollRect.horizontalNormalizedPosition = target;
    }
}