using Zenject;

public class SaveSystemInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindSaveSystem();
    }
    
    private void BindSaveSystem()
    {
        Container.Bind<ISaveSystem>()
            .To<JsonUtilitySaveSystem>()
            .AsSingle();
    }
}