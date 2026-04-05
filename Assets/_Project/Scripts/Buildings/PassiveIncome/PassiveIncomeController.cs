using UnityEngine;
using Zenject;

public class PassiveIncomeController : ITickable
{
    private const float Second = 1f;
    
    private readonly PassiveIncomeModel _model;
    private readonly PassiveIncomeView _view;
    private readonly MoneyController _moneyController;
    
    private float _timer;

    public PassiveIncomeController(PassiveIncomeModel model, PassiveIncomeView view, MoneyController moneyController)
    {
        _model = model;
        _view = view;
        _moneyController = moneyController;
    }
    
    public void Initialize()
    {
        _view.DisplayTotalIncome(_model.TotalIncome);
    }
    
    public void Tick()
    {
        _timer += Time.deltaTime;

        if (_timer >= Second)
        {
            _timer = 0f;
            _moneyController.AddMoney(_model.TotalIncome);
        }
    }

    public void UpdateIncome(string addedBuildingName)
    {
        _model.AddBuilding(addedBuildingName);
        _view.DisplayTotalIncome(_model.TotalIncome);
    }
}