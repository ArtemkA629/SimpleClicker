using System;
using UnityEngine;
using Zenject;

public class MoneyControllerEventsHandler : IDisposable
{
    private readonly MoneyController _moneyController;
    private readonly ISaveSystem _saveSystem;
    
    public MoneyControllerEventsHandler(MoneyController moneyController, ISaveSystem saveSystem)
    {
        _moneyController = moneyController;
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
        _saveSystem.Save(SavingConstants.MoneyId, _moneyController.Amount);
    }
}