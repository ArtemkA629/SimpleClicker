public class LocalizationServicesInitializer
{
    private readonly ILocalizationService _localizationService;
    private readonly LocalizationView _view;
    
    public LocalizationServicesInitializer(ILocalizationService localizationService, LocalizationView view)
    {
        _localizationService = localizationService;
        _view = view;
    }
    
    public void Initialize()
    {
        if (_localizationService is YGLocalizationService ygLocalizationService)
        {
            ygLocalizationService.Initialize();
        }
        
        _view.Initialize();
    }
}