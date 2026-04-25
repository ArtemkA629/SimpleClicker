using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Zenject;

public class ClickableEventsHandler : IDisposable
{
    private readonly List<Clickable> _clickables;
    private readonly CoinPopupService _coinPopupService;
    private readonly MoneyController _moneyController;
    private readonly PowerClickInfoHandler _powerClickInfoHandler;

    public ClickableEventsHandler(List<Clickable> clickables, CoinPopupService coinPopupService,
        MoneyController moneyController, PowerClickInfoHandler powerClickInfoHandler)
    {
        _clickables = clickables;
        _coinPopupService = coinPopupService;
        _moneyController = moneyController;
        _powerClickInfoHandler = powerClickInfoHandler;
    }

    public void Initialize()
    {
        foreach (var clickable in _clickables)
        {
            clickable.Clicked += OnClickableClicked;
        }
    }

    public void Dispose()
    {
        foreach (var clickable in _clickables)
        {
            clickable.Clicked -= OnClickableClicked;
        }
    }
    
    private void OnClickableClicked(PointerEventData eventData)
    {
        int clickMultiplier = _powerClickInfoHandler.GetPowerClickMultiplier();
        _moneyController.AddMoney(clickMultiplier);
        _coinPopupService.AnimateAdding(eventData.position);
    }
}