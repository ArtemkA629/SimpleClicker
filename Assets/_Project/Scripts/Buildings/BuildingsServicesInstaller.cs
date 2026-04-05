using UnityEngine;
using Zenject;

public class BuildingsServicesInstaller : MonoInstaller
{
    [SerializeField] private Transform _buildingItemsParent;
    
    public override void InstallBindings()
    {
        BindModel();
        BindPresenter();
        BindView();
        BindItemsFactory();
        BindPresenterEventsHandler();
        BindInitializer();
        BindPassiveIncomeController();
        BindPassiveIncomeModel();
        BindPassiveIncomeView();
    }

    private void BindModel()
    {
        Container.Bind<BuildingsModel>()
            .AsSingle();
    }
    
    private void BindPresenter()
    {
        Container.Bind<BuildingsPresenter>()
            .AsSingle();
    }
    
    private void BindView()
    {
        Container.BindInterfacesAndSelfTo<BuildingsView>()
            .AsSingle();
    }

    private void BindItemsFactory()
    {
        Container.Bind<BuildingItemsFactory>()
            .AsSingle()
            .WithArguments(_buildingItemsParent);
    }

    private void BindPresenterEventsHandler()
    {
        Container.BindInterfacesAndSelfTo<BuildingsPresenterEventHandler>()
            .AsSingle();
    }
    
    private void BindInitializer()
    {
        Container.Bind<BuildingsServicesInitializer>()
            .AsSingle();
    }
    
    private void BindPassiveIncomeController()
    {
        Container.BindInterfacesAndSelfTo<PassiveIncomeController>()
            .AsSingle();
    }

    private void BindPassiveIncomeModel()
    {
        Container.Bind<PassiveIncomeModel>()
            .AsSingle();
    }

    private void BindPassiveIncomeView()
    {
        Container.Bind<PassiveIncomeView>()
            .FromComponentInHierarchy()
            .AsSingle();
    }
}