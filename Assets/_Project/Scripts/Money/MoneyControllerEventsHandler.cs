using System;
using UnityEngine;
using Zenject;

public class MoneyControllerEventsHandler : IDisposable
{
    private readonly MoneyController _moneyController;
    private readonly BuildingsView _buildingsView;
    private readonly ISaveSystem _saveSystem;
    
    public MoneyControllerEventsHandler(MoneyController moneyController, BuildingsView buildingsView, ISaveSystem saveSystem)
    {
        _moneyController = moneyController;
        _buildingsView = buildingsView;
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
        _saveSystem.Save(SavingConstants.MoneyId, _moneyController.Amount);
    }
}