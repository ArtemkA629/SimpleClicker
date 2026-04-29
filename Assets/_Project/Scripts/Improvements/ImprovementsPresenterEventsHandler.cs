using System;

public class ImprovementsPresenterEventsHandler : IDisposable
{
    private readonly ImprovementsPresenter _presenter;
    private readonly ImprovementsDatabase _database;
    private readonly ImprovementsView _view;
    private readonly ISaveSystem _saveSystem;

    public ImprovementsPresenterEventsHandler(ImprovementsPresenter presenter, ImprovementsModel model,
        ImprovementsView view, ISaveSystem saveSystem)
    {
        _presenter = presenter;
        _database = model.Database;
        _view = view;
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
        UpdateItemsView();
    }

    private void SaveImprovements()
    {
        _saveSystem.Save(SavingConstants.BoughtImprovementsId, _database);
    }

    private void UpdateItemsView()
    {
        _view.UpdateAllItemsView(_database);
    }
}