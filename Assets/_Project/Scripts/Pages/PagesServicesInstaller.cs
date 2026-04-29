using UnityEngine;
using Zenject;

public class PagesServicesInstaller : MonoInstaller
{
    [SerializeField] private RectTransform _pagesButtonsParent;
    
    public override void InstallBindings()
    {
        BindModel();
        BindPresenter();
        BindView();
        BindFitter();
        BindSwiper();
        BindButtonsFactory();
        BindServicesInitializer();
    }

    private void BindModel()
    {
        Container.Bind<PagesModel>()
            .AsSingle();
    }
    
    private void BindPresenter()
    {
        Container.Bind<PagesPresenter>()
            .AsSingle();
    }
    
    private void BindView()
    {
        Container.BindInterfacesAndSelfTo<PagesView>()
            .AsSingle();
    }
    
    private void BindFitter()
    {
        Container.Bind<PagesFitter>()
            .FromComponentInHierarchy()
            .AsSingle();
    }
    
    private void BindSwiper()
    {
        Container.Bind<PagesSwiper>()
            .FromComponentInHierarchy()
            .AsSingle();
    }

    private void BindButtonsFactory()
    {
        Container.Bind<PagesButtonsFactory>()
            .AsSingle()
            .WithArguments(_pagesButtonsParent);
    }

    private void BindServicesInitializer()
    {
        Container.Bind<PagesServicesInitializer>()
            .AsSingle();
    }
}