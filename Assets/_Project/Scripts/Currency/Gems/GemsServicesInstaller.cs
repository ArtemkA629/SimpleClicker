using Zenject;

public class GemsServicesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindGemsModel();
        BindGemsView();
        BindGemsController();
        BindGemsControllerEventsHandler();
        BindServicesInitializer();
    }

    private void BindGemsModel()
    {
        Container.Bind<GemsModel>()
            .AsSingle();
    }

    private void BindGemsView()
    {
        Container.Bind<GemsView>()
            .FromComponentInHierarchy()
            .AsSingle();
    }
    
    private void BindGemsController()
    {
        Container.BindInterfacesAndSelfTo<GemsController>()
            .AsSingle();
    }

    private void BindGemsControllerEventsHandler()
    {
        Container.BindInterfacesAndSelfTo<GemsControllerEventsHandler>()
            .AsSingle();
    }
    
    private void BindServicesInitializer()
    {
        Container.Bind<GemsServicesInitializer>()
            .AsSingle();;
    }
}
