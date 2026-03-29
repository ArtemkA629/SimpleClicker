using System.Collections.Generic;

public class PagesServicesInitializer
{
    private readonly PagesFitter _pagesFitter;
    private readonly PagesButtonsFactory _pagesButtonsFactory;
    private readonly PagesPresenter _pagesPresenter;
    private readonly PagesView _pagesView;
    private readonly PagesSwiper _pagesSwiper;
    
    public PagesServicesInitializer(PagesFitter pagesFitter, PagesButtonsFactory pagesButtonsFactory, PagesPresenter pagesPresenter, 
        PagesView pagesView, PagesSwiper pagesSwiper)
    {
        _pagesFitter = pagesFitter;
        _pagesButtonsFactory = pagesButtonsFactory;
        _pagesPresenter = pagesPresenter;
        _pagesView = pagesView;
        _pagesSwiper = pagesSwiper;
    }

    public void Initialize()
    {
        InitializePagesFitter();
        InitializePagesSwiper();
        InitializeView();
        InitializePresenter();
    }
    
    private void InitializePagesFitter() => _pagesFitter.Initialize();
    private void InitializePresenter() => _pagesPresenter.Initialize();
    private void InitializePagesSwiper() => _pagesSwiper.Initialize();

    private void InitializeView()
    {
        List<PageButton> pagesButtons = _pagesButtonsFactory.CreatePagesButtons();
        _pagesView.Initialize(_pagesPresenter, pagesButtons);
    }
    
}