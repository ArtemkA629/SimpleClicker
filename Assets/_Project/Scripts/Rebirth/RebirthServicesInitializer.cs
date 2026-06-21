public class RebirthServicesInitializer
{
    private readonly RebirthModel _model;
    private readonly RebirthView _view;
    private readonly RebirthPresenter _presenter;
    private readonly RebirthPresenterEventHandler _presenterEventHandler;

    public RebirthServicesInitializer(RebirthModel model, RebirthView view, RebirthPresenter presenter,
        RebirthPresenterEventHandler presenterEventHandler, IConfigProvider configProvider)
    {
        _model = model;
        _view = view;
        _presenter = presenter;
        _presenterEventHandler = presenterEventHandler;
    }

    public void Initialize()
    {
        _view.Initialize(_presenter);
        _presenterEventHandler.Initialize();
        _view.DisplayCurrentLevel(_model.RebirthLevel);

        if (_model.IsLevelMax)
        {
            _view.DisplayMaxLevelMode();
        }
        else
        {
            _view.DisplayGemsReward(_model.CurrentRebirthGemsReward);
            _presenterEventHandler.UpdateMoneyDisplay();
        }
    }
}
