using YG;

public class YGLocalizationService : ILocalizationService
{
    private readonly LocalizationConfig _config;

    private string _languageCode;
    
    public YGLocalizationService(IConfigProvider configProvider)
    {
        _config = configProvider.Get<LocalizationConfig>();
    }

    public void Initialize()
    {
        _languageCode = GetLanguageCode();
    }
    
    public string GetText(string id) => _config.GetText(id, _languageCode);

    private string GetLanguageCode()
    {
        var lang = YG2.lang;
        
        if (lang == "ru") 
            return "Russian";
        
        return "English";
    }
}