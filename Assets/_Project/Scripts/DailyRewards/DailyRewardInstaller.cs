using UnityEngine;
using Zenject;

public class DailyRewardInstaller : MonoInstaller
{
    [SerializeField] private Transform _itemsContainer;
    
    public override void InstallBindings()
    {
        BindFactory();
        BindChecker();
        BindPresenter();
        BindView();
        BindServicesInitializer();
        BindPresenterEventsHandler();
    }
    
    private void BindFactory()
    {
        Container.Bind<DailyRewardItemsFactory>()
            .AsSingle()
            .WithArguments(_itemsContainer);
    }
    
    private void BindChecker()
    {
        Container.Bind<DailyRewardChecker>()
            .AsSingle();
    }
    
    private void BindPresenter()
    {
        Container.Bind<DailyRewardPresenter>()
            .AsSingle();
    }
    
    private void BindView()
    {
        Container.BindInterfacesAndSelfTo<DailyRewardView>()
            .AsSingle();
    }
    
    private void BindServicesInitializer()
    {
        Container.Bind<DailyRewardServicesInitializer>()
            .AsSingle();
    }
    
    private void BindPresenterEventsHandler()
    {
        Container.BindInterfacesAndSelfTo<DailyRewardPresenterEventsHandler>()
            .AsSingle();
    }
}
