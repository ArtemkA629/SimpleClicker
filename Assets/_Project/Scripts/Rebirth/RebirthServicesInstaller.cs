using UnityEngine;
using Zenject;

public class RebirthServicesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindModel();
        BindPresenter();
        BindView();
        BindPresenterEventsHandler();
        BindServicesInitializer();
    }

    private void BindModel()
    {
        Container.Bind<RebirthModel>()
            .AsSingle();
    }

    private void BindPresenter()
    {
        Container.Bind<RebirthPresenter>()
            .AsSingle();
    }

    private void BindView()
    {
        Container.Bind<RebirthView>()
            .FromComponentInHierarchy()
            .AsSingle();
    }

    private void BindPresenterEventsHandler()
    {
        Container.BindInterfacesAndSelfTo<RebirthPresenterEventHandler>()
            .AsSingle();
    }

    private void BindServicesInitializer()
    {
        Container.Bind<RebirthServicesInitializer>()
            .AsSingle();
    }
}
