using Zenject;

public class GameplayEntryPoint : IInitializable
{
    private readonly ClickerServicesInitializer _clickerServicesInitializer;
    private readonly MoneyServicesInitializer _moneyServicesInitializer;
    private readonly PagesServicesInitializer _pagesServicesInitializer;
    private readonly BuildingsServicesInitializer _buildingsServicesInitializer;
    
    public GameplayEntryPoint(ClickerServicesInitializer clickerServicesInitializer, 
        MoneyServicesInitializer moneyServicesInitializer, PagesServicesInitializer pagesServicesInitializer,
        BuildingsServicesInitializer buildingsServicesInitializer)
    {
        _clickerServicesInitializer = clickerServicesInitializer;
        _moneyServicesInitializer = moneyServicesInitializer;
        _pagesServicesInitializer = pagesServicesInitializer;
        _buildingsServicesInitializer = buildingsServicesInitializer;
    }

    public void Initialize()
    {
        _clickerServicesInitializer.Initialize();
        _moneyServicesInitializer.Initialize();
        _pagesServicesInitializer.Initialize();
        _buildingsServicesInitializer.Initialize();
    }
}