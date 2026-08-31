public class DailyRewardServicesInitializer
{
    private readonly DailyRewardPresenter _presenter;
    private readonly DailyRewardView _view;
    private readonly DailyRewardItemsFactory _itemsFactory;
    private readonly DailyRewardChecker _rewardChecker;
    private readonly DailyRewardPresenterEventsHandler _presenterEventsHandler;
    private readonly DailyRewardSaveData _saveData;
    private readonly bool _isTutorialCompleted;
    
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
        var tutorialSaveData = saveSystem.Load(SavingConstants.TutorialId, new TutorialSaveData());
        _isTutorialCompleted = tutorialSaveData.Step == TutorialStep.Completed;
    }
    
    public void Initialize()
    {
        _rewardChecker.Initialize(_saveData);
        _itemsFactory.Initialize(_saveData);
        var items = _itemsFactory.CreateRewardItems();
        _presenter.Initialize(_saveData);
        _view.Initialize(items, _isTutorialCompleted);
        _presenterEventsHandler.Initialize();
    }
}
