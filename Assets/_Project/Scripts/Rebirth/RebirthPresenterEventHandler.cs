using System;
using System.Numerics;

public class RebirthPresenterEventHandler : IDisposable
{
    private readonly RebirthModel _model;
    private readonly RebirthPresenter _presenter;
    private readonly RebirthView _view;
    private readonly MoneyController _moneyController;
    private readonly GemsController _gemsController;
    private readonly ISaveSystem _saveSystem;

    public RebirthPresenterEventHandler(RebirthModel model, RebirthPresenter presenter, RebirthView view, 
        MoneyController moneyController, GemsController gemsController, ISaveSystem saveSystem)
    {
        _model = model;
        _presenter = presenter;
        _view = view;
        _moneyController = moneyController;
        _gemsController = gemsController;
        _saveSystem = saveSystem;
    }

    public void Initialize()
    {
        _presenter.RebirthPerformed += OnRebirthPerformed;
        _moneyController.MoneyAmountChanged += OnMoneyAmountChanged;
    }

    public void Dispose()
    {
        _presenter.RebirthPerformed -= OnRebirthPerformed;
        _moneyController.MoneyAmountChanged -= OnMoneyAmountChanged;
    }

    public void UpdateMoneyDisplay()
    {
        BigInteger currentMoney = _moneyController.Amount;
        _presenter.TryGetRequiredMoneyForRebirth(out BigInteger price);
        BigInteger requiredMoney = price;
        _view.UpdateViewOnMoneyChanged(currentMoney, requiredMoney);
    }
    
    private void OnRebirthPerformed()
    {
        _saveSystem.Save(SavingConstants.RebirthId, new RebirthDatabase 
        { 
            RebirthData = new RebirthSaveData(_model.RebirthLevel) 
        });

        if (_model.IsLevelMax == false)
        {
            UpdateMoneyDisplay();
        }

        if (_model.RebirthLevel == 1)
        {
            _gemsController.UpdateGemsVisibility();
        }
    }

    private void OnMoneyAmountChanged()
    {
        if (_model.IsLevelMax)
            return;
        
        UpdateMoneyDisplay();
    }
}
