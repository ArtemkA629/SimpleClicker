using UnityEngine;
using Zenject;

public class AudioInstaller : MonoInstaller
{
    [SerializeField] private Transform _soundsParent;
    
    public override void InstallBindings()
    {
        BindAudioService();
        BindView();
        BindAudioInitializer();
    }

    private void BindAudioService()
    {
        Container.BindInterfacesAndSelfTo<AudioService>()
            .AsSingle()
            .WithArguments(_soundsParent);
    }

    private void BindView()
    {
        Container.Bind<AudioView>()
            .FromComponentInHierarchy()
            .AsSingle();
    }
    
    private void BindAudioInitializer()
    {
        Container.BindInterfacesAndSelfTo<AudioInitializer>()
            .AsSingle();
    }
}
