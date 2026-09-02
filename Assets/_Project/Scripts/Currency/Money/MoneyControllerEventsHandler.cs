using System;
using UnityEngine;

public class MoneyControllerEventsHandler : IDisposable
{
    private readonly MoneyController _moneyController;
    private readonly BuildingsView _buildingsView;
    private readonly ImprovementsView _improvementsView;
    private readonly ImprovementsDatabase _improvementsDatabase;
    private readonly GemsController _gemsController;
    private readonly LocationsChanger _locationsChanger;
    private readonly PagesView _pagesView;
    private readonly PagesStateHandler _pagesStateHandler;
    private readonly ISaveSystem _saveSystem;
    
    public MoneyControllerEventsHandler(MoneyController moneyController, BuildingsView buildingsView, 
        ImprovementsView improvementsView, ImprovementsModel improvementsModel, 
        LocationsChanger locationsChanger, PagesView pagesView, PagesStateHandler pagesStateHandler, ISaveSystem saveSystem)
    {
        _moneyController = moneyController;
        _buildingsView = buildingsView;
        _improvementsView = improvementsView;
        _improvementsDatabase = improvementsModel.Database;
        _locationsChanger = locationsChanger;
        _pagesView = pagesView;
        _pagesStateHandler = pagesStateHandler;
        _saveSystem = saveSystem;
    }

    public void Initialize()
    {
        _moneyController.MoneyAmountChanged += OnMoneyAmountChanged;
    }

    public void Dispose()
    {
        _moneyController.MoneyAmountChanged -= OnMoneyAmountChanged;
    }

    private void OnMoneyAmountChanged()
    {
        _buildingsView.UpdateBuildingsPrices(_moneyController.Amount);
        _improvementsView.UpdateAllItemsView(_improvementsDatabase);
        _saveSystem.Save(SavingConstants.MoneyId, _moneyController.Amount.ToString());
        _locationsChanger.TryUnlockLocations(_moneyController.Amount);
        UpdatePageButtons();
    }

    private void UpdatePageButtons()
    {
        foreach (PageButton button in _pagesView.PageButtons)
        {
            bool isLocked = _pagesStateHandler.GetPageLockedState(button.Id);
            button.DisplayLockedState(isLocked);
            _pagesView.DisplayPageLockedState(button.Id, isLocked);

            if (isLocked == false)
            {
                _pagesStateHandler.SaveUnlocked(button.Id);
            }
        }
    }
}