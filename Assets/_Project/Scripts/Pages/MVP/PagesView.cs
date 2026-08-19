using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PagesView : MonoBehaviour, IDisposable
{
    [SerializeField] private PageViewInfo[] _pageViewInfos;
    
    private PagesPresenter _presenter;
    private PagesSwiper _swiper;
    
    private Dictionary<PageButton, UnityAction> _pageButtonHandlers = new();
    
    public IEnumerable<PageButton> PageButtons => _pageButtonHandlers.Keys;
    
    [Inject]
    private void Inject(PagesSwiper swiper)
    {
        _swiper = swiper;
    }
    
    public void Initialize(PagesPresenter presenter, List<PageButton> pagesButtons)
    {
        _presenter = presenter;
        
        foreach (PageButton pageButton in pagesButtons)
        {
            int pageNumber = pageButton.Number;
            UnityAction handler = () => OnPageButtonClicked(pageNumber);
            _pageButtonHandlers[pageButton] = handler;
            pageButton.AddListener(handler);
        }
    }

    public void Dispose()
    {
        foreach (var pair in _pageButtonHandlers)
        {
            pair.Key.RemoveListener(pair.Value);
        }

        _pageButtonHandlers.Clear();
    }
    
    public void DisplayPageSelected(int buttonNumber, bool instantScroll = false)
    {
        foreach (var pageButton in _pageButtonHandlers.Keys)
        {
            pageButton.DisplaySelected(pageButton.Number == buttonNumber);
        }
        
        _swiper.GoToPage(buttonNumber, instantScroll);
    }

    public void DisplayPageSelectedInstant(int buttonNumber)
    {
        DisplayPageSelected(buttonNumber, true);
    }

    public void DisplayPageLockedState(string pageId, bool isLocked)
    {
        foreach (PageViewInfo viewInfo in _pageViewInfos)
        {
            if (viewInfo.Info.Description == pageId)
            {
                viewInfo.LockPanel.SetActive(isLocked);
                break;
            }
        }
    }
    
    private void OnPageButtonClicked(int number)
    {
        _presenter.SelectPage(number);
    }
}

[Serializable]
public struct PageViewInfo
{
    [field: SerializeField] public GameObject LockPanel { get; private set; }
    [field: SerializeField] public PageInfo Info { get; private set; }
}