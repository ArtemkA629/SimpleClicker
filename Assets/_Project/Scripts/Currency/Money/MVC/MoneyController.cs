using System;
using System.Numerics;
using Zenject;

public class MoneyController : IInitializable
{
    private readonly MoneyModel _model;
    private readonly MoneyView _view;
    
    public event Action MoneyAmountChanged;
    
    public MoneyController(MoneyModel model, MoneyView view)
    {
        _model = model;
        _view = view;
    }
    
    public BigInteger Amount => _model.Amount;

    public void Initialize()
    {
        _view.DisplayMoney(Amount);
    }
    
    public void AddMoney(BigInteger amount)
    {
        _model.AddMoney(amount);
        _view.DisplayMoney(Amount);
        MoneyAmountChanged?.Invoke();
    }

    public bool TrySubtractMoney(BigInteger amount)
    {
        if (_model.TrySubtractMoney(amount) == false)
            return false;
        
        _view.DisplayMoney(Amount);
        MoneyAmountChanged?.Invoke();
        return true;
    }
}