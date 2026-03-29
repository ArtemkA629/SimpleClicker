using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ClickableServicesInstaller : MonoInstaller
{
    [SerializeField] private List<Clickable> _clickables;
    
    public override void InstallBindings()
    {
        BindClickableAnimator();
        BindClickableEventsHandler();
        BindServicesInitializer();
    }

    private void BindClickableAnimator()
    {
        Container.Bind<ClickableAnimator>()
            .AsSingle();
    }

    private void BindClickableEventsHandler()
    {
        Container.BindInterfacesAndSelfTo<ClickableEventsHandler>()
            .AsSingle()
            .WithArguments(_clickables);
    }

    private void BindServicesInitializer()
    {
        Container.Bind<ClickerServicesInitializer>()
            .AsSingle();
    }
}
