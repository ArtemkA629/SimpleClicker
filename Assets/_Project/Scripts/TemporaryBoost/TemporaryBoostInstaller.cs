using UnityEngine;
using Zenject;

public class TemporaryBoostInstaller : MonoInstaller
{
    [SerializeField] private RectTransform[] _boostsSpawnPanels;
    
    public override void InstallBindings()
    {
        BindModel();
        BindAnimator();
        BindController();
        BindView();
        BindEventsHandler();
        BindSpawner();
        BindServicesInitializer();
    }
    
    private void BindModel()
    {
        Container.Bind<TemporaryBoostModel>()
            .AsSingle();
    }
    
    private void BindAnimator()
    {
        Container.Bind<TemporaryBoostAnimator>()
            .AsSingle();
    }
    
    private void BindController()
    {
        Container.BindInterfacesAndSelfTo<TemporaryBoostController>()
            .AsSingle();
    }
    
    private void BindView()
    {
        Container.Bind<TemporaryBoostView>()
            .FromComponentInHierarchy()
            .AsSingle();
    }
    
    private void BindEventsHandler()
    {
        Container.Bind<TemporaryBoostEventsHandler>()
            .AsSingle();
    }
    
    private void BindSpawner()
    {
        Container.BindInterfacesAndSelfTo<TemporaryBoostSpawner>()
            .AsSingle()
            .WithArguments(_boostsSpawnPanels);
    }
    
    private void BindServicesInitializer()
    {
        Container.Bind<TemporaryBoostServicesInitializer>()
            .AsSingle();
    }
}
