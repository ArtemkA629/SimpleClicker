using System;
using UnityEngine;
using Zenject;

public class TemporaryBoostController : ITickable
{
    private readonly TemporaryBoostConfig _config;
    private readonly TemporaryBoostModel _model;
    private TemporaryBoostView _view;
    private bool _isInitialized;
    
    public TemporaryBoostController(TemporaryBoostModel model, IConfigProvider configProvider)
    {
        _model = model;
        _config = configProvider.Get<TemporaryBoostConfig>();
    }
    
    public void Initialize(TemporaryBoostView view)
    {
        _view = view;
        _isInitialized = true;
        
        if (_model.IsBoostActive)
        {
            _view.ShowBoost();
        }
        else
        {
            _view.HideBoost();
        }
    }
    
    public void CollectBoost()
    {
        _model.ActivateBoost(_config.BoostDuration);
        
        if (_isInitialized)
        {
            _view.ShowBoost();
        }
    }
    
    public void Tick()
    {
        if (_model.IsBoostActive)
        {
            _model.UpdateRemainingTime(Time.deltaTime);
            
            if (_isInitialized)
            {
                _view.UpdateTimer(_model.RemainingBoostTime, _config.BoostDuration);
            }
            
            if (!_model.IsBoostActive)
            {
                
                if (_isInitialized)
                {
                    _view.HideBoost();
                }
            }
        }
    }
}
