using UnityEngine;
using Zenject;

public class CoinPopupServicesInstaller : MonoInstaller
{
    [SerializeField] private RectTransform _coinPopupTarget;
    [SerializeField] private Canvas _canvas;
    
    public override void InstallBindings()
    {
        BindCoinPopupAnimator();
        BindCoinPopupSpawner();
        BindCoinPopupService();
    }

    private void BindCoinPopupAnimator()
    {
        Container.Bind<CoinPopupAnimator>()
            .AsSingle()
            .WithArguments(_coinPopupTarget);
    }
    
    private void BindCoinPopupSpawner()
    {
        Container.Bind<CoinPopupSpawner>()
            .AsSingle()
            .WithArguments(_canvas);
    }

    private void BindCoinPopupService()
    {
        Container.Bind<CoinPopupService>()
            .AsSingle();
    }
}