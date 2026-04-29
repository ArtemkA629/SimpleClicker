using UnityEngine;
using Zenject;

public class ImprovementsServicesInstaller : MonoInstaller
{
    [SerializeField] private Transform _improvementItemsParent;
    
    public override void InstallBindings()
    {
        BindItemsFactory();
        BindPresenter();
        BindPresenterEventsHandler();
        BindView();
        BindModel();
        BindPowerClickInfoHandler();
        BindLastLoginTimeSaver();
        BindOfflineIncomeService();
        BindServicesInitializer();
    }

    private void BindItemsFactory()
    {
        Container.Bind<ImprovementItemsFactory>()
            .AsSingle()
            .WithArguments(_improvementItemsParent);
    }
    
    private void BindPresenter()
    {
        Container.Bind<ImprovementsPresenter>()
            .AsSingle();
    }

    private void BindPresenterEventsHandler()
    {
        Container.BindInterfacesAndSelfTo<ImprovementsPresenterEventsHandler>()
            .AsSingle();
    }
    
    private void BindView()
    {
        Container.Bind<ImprovementsView>()
            .AsSingle();
    }

    private void BindModel()
    {
        Container.Bind<ImprovementsModel>()
            .AsSingle();
    }

    private void BindPowerClickInfoHandler()
    {
        Container.Bind<PowerClickInfoHandler>()
            .AsSingle();
    }

    private void BindLastLoginTimeSaver()
    {
        Container.BindInterfacesAndSelfTo<LastLoginTimeSaver>()
            .AsSingle();
    }

    private void BindOfflineIncomeService()
    {
        Container.Bind<OfflineIncomeService>()
            .AsSingle();
    }
    
    private void BindServicesInitializer()
    {
        Container.BindInterfacesAndSelfTo<ImprovementsServicesInitializer>()
            .AsSingle();
    }
}