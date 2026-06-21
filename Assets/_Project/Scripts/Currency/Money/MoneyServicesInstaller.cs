using Zenject;

public class MoneyServicesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindMoneyModel();
        BindMoneyView();
        BindMoneyController();
        BindMoneyControllerEventsHandler();
        BindServicesInitializer();
    }

    private void BindMoneyModel()
    {
        Container.Bind<MoneyModel>()
            .AsSingle();
    }

    private void BindMoneyView()
    {
        Container.Bind<MoneyView>()
            .FromComponentInHierarchy()
            .AsSingle();
    }
    
    private void BindMoneyController()
    {
        Container.BindInterfacesAndSelfTo<MoneyController>()
            .AsSingle();
    }

    private void BindMoneyControllerEventsHandler()
    {
        Container.BindInterfacesAndSelfTo<MoneyControllerEventsHandler>()
            .AsSingle();
    }
    
    private void BindServicesInitializer()
    {
        Container.Bind<MoneyServicesInitializer>()
            .AsSingle();;
    }
}