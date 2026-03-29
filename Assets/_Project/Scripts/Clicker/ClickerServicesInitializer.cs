using Zenject;

public class ClickerServicesInitializer
{
    private readonly ClickableEventsHandler _eventsHandler;
    
    public ClickerServicesInitializer(ClickableEventsHandler eventsHandler)
    {
        _eventsHandler = eventsHandler;
    }
    
    public void Initialize()
    {
        _eventsHandler.Initialize();
    }
}