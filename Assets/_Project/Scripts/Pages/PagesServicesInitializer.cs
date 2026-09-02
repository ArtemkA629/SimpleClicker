using System.Collections.Generic;
using UnityEngine;

public class PagesServicesInitializer
{
    private readonly PagesFitter _pagesFitter;
    private readonly PagesButtonsFactory _pagesButtonsFactory;
    private readonly PagesPresenter _pagesPresenter;
    private readonly PagesView _pagesView;
    private readonly PagesSwiper _pagesSwiper;
    private readonly PagesStateHandler _pagesStateHandler;
    private readonly PagesDatabase _database;
    
    public PagesServicesInitializer(PagesFitter pagesFitter, PagesButtonsFactory pagesButtonsFactory, 
        PagesPresenter pagesPresenter, PagesView pagesView, PagesSwiper pagesSwiper, PagesStateHandler pagesStateHandler, 
        IConfigProvider configProvider, ISaveSystem saveSystem)
    {
        _pagesFitter = pagesFitter;
        _pagesButtonsFactory = pagesButtonsFactory;
        _pagesPresenter = pagesPresenter;
        _pagesView = pagesView;
        _pagesSwiper = pagesSwiper;
        _pagesStateHandler = pagesStateHandler;
        PagesConfig config = configProvider.Get<PagesConfig>();
        _database = saveSystem.Load(SavingConstants.UnlockedPagesId, config.GetDefaultUnlockedPages());
    }

    public void Initialize()
    {
        _pagesFitter.Initialize();
        _pagesSwiper.Initialize();
        _pagesStateHandler.Initialize(_database);
        InitializeView();
        _pagesPresenter.Initialize();
    }

    private void InitializeView()
    {
        List<PageButton> pagesButtons = _pagesButtonsFactory.CreatePagesButtons();

        foreach (var button in pagesButtons)
        {
            Debug.Log(button.Id);
        }
        
        _pagesView.Initialize(_pagesPresenter, pagesButtons);

        foreach (var button in pagesButtons)
        {
            bool isLocked = _pagesStateHandler.GetPageLockedState(button.Id);
            _pagesView.DisplayPageLockedState(button.Id, isLocked);
        }
    }
    
}