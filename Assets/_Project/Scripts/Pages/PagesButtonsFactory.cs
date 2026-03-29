using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PagesButtonsFactory
{
    private readonly PagesConfig _pagesConfig;
    private readonly PageButton _pageButtonPrefab;
    private readonly Transform _pagesButtonsContent;
    private readonly DiContainer _container;
    
    public PagesButtonsFactory(IConfigProvider configProvider, Transform pagesButtonsContent, DiContainer container)
    {
        _pagesConfig = configProvider.Get<PagesConfig>();
        _pageButtonPrefab = configProvider.Get<PagesViewConfig>().PageButtonPrefab;
        _pagesButtonsContent = pagesButtonsContent;
        _container = container;
    }

    public List<PageButton> CreatePagesButtons()
    {
        List<PageButton> pageButtons = new();
        var orderedPagesInfo = _pagesConfig.PagesInfo.OrderBy(p => p.Number);

        foreach (PageInfo info in orderedPagesInfo)
        {
            var pageButton = _container.InstantiatePrefabForComponent<PageButton>(_pageButtonPrefab, _pagesButtonsContent);
            pageButton.Initialize();
            pageButton.SetInfo(info.Number);
            pageButton.SetUI(info.Sprite, info.Description);
            pageButtons.Add(pageButton);
        }

        return pageButtons;
    }
}