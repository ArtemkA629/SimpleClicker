using Zenject;

public class GameplayEntryPointInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindEntryPoint();
    }

    private void BindEntryPoint()
    {
        Container.BindInterfacesAndSelfTo<GameplayEntryPoint>()
            .AsSingle();
    }
}