public class LocalizationServicesInitializer
{
    private readonly YGLocalizationService _localizationService;
    private readonly LocalizationView _view;
    
    public LocalizationServicesInitializer(YGLocalizationService localizationService, LocalizationView view)
    {
        _localizationService = localizationService;
        _view = view;
    }
    
    public void Initialize()
    {
        _localizationService.Initialize();
        _view.Initialize();
    }
}