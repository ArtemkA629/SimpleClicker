using UnityEngine;
using Zenject;

public class BounceServiceInstaller : MonoInstaller 
{
    public override void InstallBindings()
    {
        Container.Bind<BounceService>()
            .AsSingle();
    }
}