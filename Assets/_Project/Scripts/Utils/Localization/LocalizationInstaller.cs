using Zenject;

public class LocalizationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindLocalizationService();
        BindInitializer();
        BindView();
    }

    private void BindLocalizationService()
    {
        Container.BindInterfacesAndSelfTo<YGLocalizationService>()
            .AsSingle();
    }

    private void BindInitializer()
    {
        Container.Bind<LocalizationServicesInitializer>()
            .AsSingle();
    }
    
    private void BindView()
    {
        Container.Bind<LocalizationView>()
            .FromComponentInHierarchy()
            .AsSingle();
    }
}