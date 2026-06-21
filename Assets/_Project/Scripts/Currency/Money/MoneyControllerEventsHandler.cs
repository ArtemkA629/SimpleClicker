using System;

public class MoneyControllerEventsHandler : IDisposable
{
    private readonly MoneyController _moneyController;
    private readonly BuildingsView _buildingsView;
    private readonly ImprovementsView _improvementsView;
    private readonly ImprovementsDatabase _improvementsDatabase;
    private readonly GemsController _gemsController;
    private readonly ISaveSystem _saveSystem;
    
    public MoneyControllerEventsHandler(MoneyController moneyController, BuildingsView buildingsView, 
        ImprovementsView improvementsView, ImprovementsModel improvementsModel, ISaveSystem saveSystem)
    {
        _moneyController = moneyController;
        _buildingsView = buildingsView;
        _improvementsView = improvementsView;
        _improvementsDatabase = improvementsModel.Database;
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
    }
}