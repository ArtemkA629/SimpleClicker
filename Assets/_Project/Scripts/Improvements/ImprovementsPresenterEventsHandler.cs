using System;

public class ImprovementsPresenterEventsHandler : IDisposable
{
    private readonly ImprovementsPresenter _presenter;
    private readonly ImprovementsDatabase _database;
    private readonly ISaveSystem _saveSystem;

    public ImprovementsPresenterEventsHandler(ImprovementsPresenter presenter, ImprovementsModel model,
        ISaveSystem saveSystem)
    {
        _presenter = presenter;
        _database = model.Database;
        _saveSystem = saveSystem;
    }

    public void Initialize()
    {
        _presenter.ImprovementBought += OnImprovementBought;
        SaveImprovements();
    }
    
    public void Dispose()
    {
        _presenter.ImprovementBought -= OnImprovementBought;
    }
    
    private void OnImprovementBought()
    {
        SaveImprovements();
    }

    private void SaveImprovements()
    {
        _saveSystem.Save(SavingConstants.BoughtImprovementsId, _database);
    }
}