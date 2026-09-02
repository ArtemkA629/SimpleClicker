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
    private readonly TemporaryBoostServicesInitializer _temporaryBoostServicesInitializer;
    private readonly ShopServicesInitializer _shopServicesInitializer;
    private readonly AudioInitializer _audioInitializer;
    private readonly TutorialInitializer _tutorialInitializer;
    private readonly LocalizationServicesInitializer _localizationServicesInitializer;
    
    public GameplayEntryPoint(ClickerServicesInitializer clickerServicesInitializer, 
        MoneyServicesInitializer moneyServicesInitializer, GemsServicesInitializer gemsServicesInitializer,
        PagesServicesInitializer pagesServicesInitializer, BuildingsServicesInitializer buildingsServicesInitializer, 
        ImprovementsServicesInitializer improvementsServicesInitializer, 
        LocationsServicesInitializer locationsServicesInitializer, RebirthServicesInitializer rebirthServicesInitializer,
        DailyRewardServicesInitializer dailyRewardServicesInitializer,
        TemporaryBoostServicesInitializer temporaryBoostServicesInitializer, 
        ShopServicesInitializer shopServicesInitializer, AudioInitializer audioInitializer,
        TutorialInitializer tutorialInitializer, LocalizationServicesInitializer localizationServicesInitializer)
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
        _temporaryBoostServicesInitializer = temporaryBoostServicesInitializer;
        _shopServicesInitializer = shopServicesInitializer;
        _audioInitializer = audioInitializer;
        _tutorialInitializer = tutorialInitializer;
        _localizationServicesInitializer = localizationServicesInitializer;
    }

    public void Initialize()
    {
        _clickerServicesInitializer.Initialize();
        _moneyServicesInitializer.Initialize();
        _gemsServicesInitializer.Initialize();
        _localizationServicesInitializer.Initialize();
        _pagesServicesInitializer.Initialize();
        _buildingsServicesInitializer.Initialize();
        _locationsServicesInitializer.Initialize();
        _rebirthServicesInitializer.Initialize();
        _improvementsServicesInitializer.Initialize();
        _dailyRewardServicesInitializer.Initialize();
        _temporaryBoostServicesInitializer.Initialize();
        _shopServicesInitializer.Initialize();
        _audioInitializer.Initialize();
        _tutorialInitializer.Initialize();
    }
}