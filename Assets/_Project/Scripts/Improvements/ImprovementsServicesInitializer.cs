using System.Collections.Generic;
using System.Linq;

public class ImprovementsServicesInitializer
{
    private readonly ImprovementItemsFactory _factory;
    private readonly ImprovementsView _view;
    private readonly ImprovementsPresenter _presenter;
    private readonly ImprovementsConfig _config;
    private readonly ImprovementsDatabase _database;
    private readonly ImprovementsPresenterEventsHandler _presenterEventsHandler;
    private readonly OfflineIncomeService _offlineIncomeService;
    private readonly MoneyModel _moneyModel;
    
    public ImprovementsServicesInitializer(ImprovementItemsFactory factory, ImprovementsView view, 
        ImprovementsPresenter presenter, ImprovementsModel model, 
        ImprovementsPresenterEventsHandler presenterEventsHandler, OfflineIncomeService offlineIncomeService,
        MoneyModel moneyModel, IConfigProvider configProvider)
    {
        _factory = factory;
        _view = view;
        _presenter = presenter;
        _config = configProvider.Get<ImprovementsConfig>();
        _database = model.Database;
        _presenterEventsHandler = presenterEventsHandler;
        _offlineIncomeService = offlineIncomeService;
        _moneyModel = moneyModel;
    }
    
    public void Initialize()
    {
        List<ImprovementItem> improvementItems = CreateItems();
        _view.Init(_presenter, improvementItems);
        _presenterEventsHandler.Initialize();
        _offlineIncomeService.AddOfflineIncome();
    }

    private List<ImprovementItem> CreateItems()
    {
        var improvementItems = new List<ImprovementItem>();
        
        foreach (var improvementInfo in _config.ImprovementsInfo)
        {
            ImprovementSaveData saveData = _database.GetData(improvementInfo.TypeConfig.Name);
            var levelInfoConfig = (IImprovementLevelInfoConfig)improvementInfo.LevelInfoConfig;
            int nextLevelImprovementLevel = levelInfoConfig.LevelsInfo.Length == saveData.Level ? saveData.Level : saveData.Level + 1;
            var nextLevelPrice = levelInfoConfig.GetPriceByLevel(nextLevelImprovementLevel);
            _factory.SetInfo(improvementInfo, saveData, _moneyModel.Amount >= nextLevelPrice);
            ImprovementItem item = _factory.CreateItem();
            improvementItems.Add(item);
        }
        
        return improvementItems;
    }
}