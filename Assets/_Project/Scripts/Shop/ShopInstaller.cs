using UnityEngine;
using Zenject;

public class ShopInstaller : MonoInstaller
{
    [SerializeField] private RectTransform _itemsContainer;

    public override void InstallBindings()
    {
        BindPurchaseService();
        BindItemsFactory();
        BindServicesInitializer();
    }

    private void BindPurchaseService()
    {
        Container.BindInterfacesAndSelfTo<ShopPurchaseService>()
            .AsSingle();
    }

    private void BindItemsFactory()
    {
        Container.Bind<ShopItemsFactory>()
            .AsSingle()
            .WithArguments(_itemsContainer);
    }

    private void BindServicesInitializer()
    {
        Container.BindInterfacesAndSelfTo<ShopServicesInitializer>()
            .AsSingle();
    }
}
