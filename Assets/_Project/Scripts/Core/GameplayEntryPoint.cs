using Zenject;

public class GameplayEntryPoint : IInitializable
{
    private readonly ClickerServicesInitializer _clickerServicesInitializer;
    private readonly MoneyServicesInitializer _moneyServicesInitializer;
    private readonly GemsServicesInitializer _gemsServicesInitializer;
    private readonly PagesServicesInitializer _pagesServicesInitializer;
    private readonly BuildingsServicesInitializer _buildingsServicesInitializer;
    private readonly ImprovementsServicesInitializer _improvementsServicesInitializer;
    private readonly RebirthServicesInitializer _rebirthServicesInitializer;
    private readonly LocationsServicesInitializer _locationsServicesInitializer;
    private readonly DailyRewardServicesInitializer _dailyRewardServicesInitializer;
    
    public GameplayEntryPoint(ClickerServicesInitializer clickerServicesInitializer, 
        MoneyServicesInitializer moneyServicesInitializer, GemsServicesInitializer gemsServicesInitializer,
        PagesServicesInitializer pagesServicesInitializer, BuildingsServicesInitializer buildingsServicesInitializer, 
        ImprovementsServicesInitializer improvementsServicesInitializer, 
        LocationsServicesInitializer locationsServicesInitializer, RebirthServicesInitializer rebirthServicesInitializer,
        DailyRewardServicesInitializer dailyRewardServicesInitializer)
    {
        _clickerServicesInitializer = clickerServicesInitializer;
        _moneyServicesInitializer = moneyServicesInitializer;
        _gemsServicesInitializer = gemsServicesInitializer;
        _pagesServicesInitializer = pagesServicesInitializer;
        _buildingsServicesInitializer = buildingsServicesInitializer;
        _improvementsServicesInitializer = improvementsServicesInitializer;
        _rebirthServicesInitializer = rebirthServicesInitializer;
        _locationsServicesInitializer = locationsServicesInitializer;
        _dailyRewardServicesInitializer = dailyRewardServicesInitializer;
    }

    public void Initialize()
    {
        _clickerServicesInitializer.Initialize();
        _moneyServicesInitializer.Initialize();
        _gemsServicesInitializer.Initialize();
        _pagesServicesInitializer.Initialize();
        _buildingsServicesInitializer.Initialize();
        _locationsServicesInitializer.Initialize();
        _rebirthServicesInitializer.Initialize();
        _improvementsServicesInitializer.Initialize();
        _dailyRewardServicesInitializer.Initialize();
    }
}