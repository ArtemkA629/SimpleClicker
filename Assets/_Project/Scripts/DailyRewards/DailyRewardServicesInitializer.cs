public class DailyRewardServicesInitializer
{
    private readonly DailyRewardPresenter _presenter;
    private readonly DailyRewardView _view;
    private readonly DailyRewardItemsFactory _itemsFactory;
    private readonly DailyRewardChecker _rewardChecker;
    private readonly DailyRewardPresenterEventsHandler _presenterEventsHandler;

    private DailyRewardSaveData _saveData;
    
    public DailyRewardServicesInitializer(DailyRewardPresenter presenter, DailyRewardView view, 
        DailyRewardItemsFactory itemsFactory, DailyRewardChecker rewardChecker, 
        DailyRewardPresenterEventsHandler presenterEventsHandler, ISaveSystem saveSystem)
    {
        _presenter = presenter;
        _view = view;
        _itemsFactory = itemsFactory;
        _rewardChecker = rewardChecker;
        _presenterEventsHandler = presenterEventsHandler;
        _saveData = saveSystem.Load(SavingConstants.DailyRewardId, new DailyRewardSaveData());
    }
    
    public void Initialize()
    {
        _rewardChecker.Initialize(_saveData);
        _itemsFactory.Initialize(_saveData);
        var items = _itemsFactory.CreateRewardItems();
        _view.Initialize(items);
        _presenter.Initialize(_saveData);
        _presenterEventsHandler.Initialize();
    }
}
