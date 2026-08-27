public class TemporaryBoostServicesInitializer
{
    private readonly TemporaryBoostController _controller;
    private readonly TemporaryBoostView _view;
    private readonly TemporaryBoostSpawner _spawner;
    
    public TemporaryBoostServicesInitializer(TemporaryBoostController controller, TemporaryBoostView view,
        TemporaryBoostSpawner spawner)
    {
        _controller = controller;
        _view = view;
        _spawner = spawner;
    }
    
    public void Initialize()
    {
        _controller.Initialize(_view);
        _spawner.Initialize();
        _view.Initialize();
    }
}