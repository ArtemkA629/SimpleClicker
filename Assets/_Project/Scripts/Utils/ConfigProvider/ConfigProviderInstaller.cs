using UnityEngine;
using Zenject;

public class ConfigProviderInstaller : MonoInstaller
{
    [SerializeField] private ConfigRegistry _configRegistry;
    
    public override void InstallBindings()
    {
        BindConfigRegistry();
        BindConfigProvider();
    }

    private void BindConfigRegistry()
    {
        Container.BindInstance(_configRegistry)
            .AsSingle();
    }
    
    private void BindConfigProvider()
    {
        Container
            .Bind<IConfigProvider>()
            .To<GlobalConfigProvider>()
            .AsSingle();
    }
}