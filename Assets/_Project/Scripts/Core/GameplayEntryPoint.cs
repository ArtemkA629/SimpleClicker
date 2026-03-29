using Zenject;

public class GameplayEntryPoint : IInitializable
{
    private readonly ClickerServicesInitializer _clickerServicesInitializer;
    private readonly MoneyServicesInitializer _moneyServicesInitializer;
    private readonly PagesServicesInitializer _pagesServicesInitializer;
    
    public GameplayEntryPoint(ClickerServicesInitializer clickerServicesInitializer, 
        MoneyServicesInitializer moneyServicesInitializer, PagesServicesInitializer pagesServicesInitializer)
    {
        _clickerServicesInitializer = clickerServicesInitializer;
        _moneyServicesInitializer = moneyServicesInitializer;
        _pagesServicesInitializer = pagesServicesInitializer;
    }

    public void Initialize()
    {
        _clickerServicesInitializer.Initialize();
        _moneyServicesInitializer.Initialize();
        _pagesServicesInitializer.Initialize();
    }
}