using System;
using UnityEngine;
using Zenject;

public class GemsController : IInitializable
{
    private readonly GemsModel _model;
    private readonly GemsView _view;
    
    public event Action GemsAmountChanged;
    
    public GemsController(GemsModel model, GemsView view)
    {
        _model = model;
        _view = view;
    }
    
    public int Amount => _model.Amount;

    public void Initialize()
    {
        _view.DisplayGems(Amount);
        _view.ShowGemsActive(Amount > 0);
    }
    
    public void AddGems(int amount)
    {
        _model.AddGems(amount);
        _view.DisplayGems(Amount);
        GemsAmountChanged?.Invoke();
    }

    public bool TrySubtractGems(int amount)
    {
        if (_model.TrySubtractGems(amount) == false)
            return false;
        
        _view.DisplayGems(Amount);
        GemsAmountChanged?.Invoke();
        return true;
    }

    public void UpdateGemsVisibility()
    {
        Debug.Log("UpdateGemsVisibility");
        _view.ShowGemsActive(Amount > 0);
    }
}
