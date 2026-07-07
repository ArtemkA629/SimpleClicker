using UnityEngine;
using Zenject;

public class LocationsServicesInstaller : MonoInstaller
{
    [SerializeField] private LocationItem _itemPrefab;
    [SerializeField] private Transform _itemsContent;
    
    public override void InstallBindings()
    {
        BindLocationsChanger();
        BindView();
        BindDatabase();
        BindServicesInitializer();
        BindLocationsChangerEventsHandler();
        BindFactory();
        BindLocationItemsEventsHandler();
    }

    private void BindLocationsChanger()
    {
        Container.Bind<LocationsChanger>()
            .AsSingle();
    }

    private void BindView()
    {
        Container.Bind<LocationView>()
            .FromComponentInHierarchy()
            .AsSingle();
    }

    private void BindDatabase()
    {
        Container.Bind<LocationsDatabase>()
            .FromMethod(() => new LocationsDatabase())
            .AsSingle();
    }
    
    private void BindServicesInitializer()
    {
        Container.Bind<LocationsServicesInitializer>()
            .AsSingle();
    }
    
    private void BindLocationsChangerEventsHandler()
    {
        Container.BindInterfacesAndSelfTo<LocationsChangerEventsHandler>()
            .AsSingle();
    }
    
    private void BindFactory()
    {
        Container.Bind<LocationsFactory>()
            .AsSingle()
            .WithArguments(_itemPrefab, _itemsContent);
    }
    
    private void BindLocationItemsEventsHandler()
    {
        Container.Bind<LocationItemsEventsHandler>()
            .AsSingle();
    }
}