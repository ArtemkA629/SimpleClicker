using System;
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
    
    public int Amount => _model.Amount;

    public void Initialize()
    {
        _view.DisplayMoney(Amount);
    }
    
    public void AddMoney(int amount)
    {
        _model.AddMoney(amount);
        _view.DisplayMoney(Amount);
        MoneyAmountChanged?.Invoke();
    }
}