using UnityEngine;
using Zenject;

public class RewardServicesInstaller : MonoInstaller
{
    [SerializeField] private RewardButton[] _rewardButtons;
    
    public override void InstallBindings()
    {
        BindRewardCooldownModel();
        BindRewardButtonsLifetimeService();
        BindRewardService();
        BindRewardInitializer();
    }

    private void BindRewardCooldownModel()
    {
        Container.Bind<RewardCooldownModel>()
            .AsSingle();
    }

    private void BindRewardButtonsLifetimeService()
    {
        Container.BindInterfacesAndSelfTo<RewardButtonsLifetimeService>()
            .AsSingle()
            .WithArguments(_rewardButtons);
    }

    private void BindRewardService()
    {
        Container.BindInterfacesAndSelfTo<RewardService>()
            .AsSingle()
            .WithArguments(_rewardButtons);
    }

    private void BindRewardInitializer()
    {
        Container.Bind<RewardInitializer>()
            .AsSingle();
    }
}
