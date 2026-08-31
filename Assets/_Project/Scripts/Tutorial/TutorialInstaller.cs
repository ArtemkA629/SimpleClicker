using UnityEngine;
using Zenject;

public class TutorialInstaller : MonoInstaller
{
    [SerializeField] private GameObject _tutorialFingerPrefab;
    
    public override void InstallBindings()
    {
        BindTutorialService();
        BindTutorialInitializer();
        BindTutorialView();
        BindTutorialStepHandler();
        BindEventsHandler();
    }

    private void BindTutorialService()
    {
        Container.Bind<TutorialService>()
            .AsSingle()
            .WithArguments(_tutorialFingerPrefab);
    }
    
    private void BindTutorialInitializer()
    {
        Container.Bind<TutorialInitializer>()
            .AsSingle();
    }
    
    private void BindTutorialView()
    {
        Container.Bind<TutorialView>()
            .FromComponentInHierarchy()
            .AsSingle();
    }
    
    private void BindTutorialStepHandler()
    {
        Container.BindInterfacesAndSelfTo<TutorialStepHandler>()
            .AsSingle();
    }

    private void BindEventsHandler()
    {
        Container.BindInterfacesAndSelfTo<TutorialServiceEventsHandler>()
            .AsSingle();
    }
}
