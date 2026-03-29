using System;
using System.Collections.Generic;
using UnityEngine.Events;
using Zenject;

public class PagesView : IDisposable
{
    private PagesPresenter _presenter;
    private PagesSwiper _swiper;
    
    private Dictionary<PageButton, UnityAction> _pageButtonHandlers = new();
    
    [Inject]
    private void Construct(PagesSwiper swiper)
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
    
    private void OnPageButtonClicked(int number)
    {
        _presenter.SelectPage(number);
    }
}