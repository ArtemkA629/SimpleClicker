using Zenject;

public class GameplayEntryPoint : IInitializable
{
    private readonly ClickerServicesInitializer _clickerServicesInitializer;
    private readonly MoneyServicesInitializer _moneyServicesInitializer;
    private readonly PagesServicesInitializer _pagesServicesInitializer;
    private readonly BuildingsServicesInitializer _buildingsServicesInitializer;
    private readonly ImprovementsServicesInitializer _improvementsServicesInitializer;
    
    public GameplayEntryPoint(ClickerServicesInitializer clickerServicesInitializer, 
        MoneyServicesInitializer moneyServicesInitializer, PagesServicesInitializer pagesServicesInitializer,
        BuildingsServicesInitializer buildingsServicesInitializer, ImprovementsServicesInitializer improvementsServicesInitializer)
    {
        _clickerServicesInitializer = clickerServicesInitializer;
        _moneyServicesInitializer = moneyServicesInitializer;
        _pagesServicesInitializer = pagesServicesInitializer;
        _buildingsServicesInitializer = buildingsServicesInitializer;
        _improvementsServicesInitializer = improvementsServicesInitializer;
    }

    public void Initialize()
    {
        _clickerServicesInitializer.Initialize();
        _moneyServicesInitializer.Initialize();
        _pagesServicesInitializer.Initialize();
        _buildingsServicesInitializer.Initialize();
        _improvementsServicesInitializer.Initialize();
    }
}