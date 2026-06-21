public class MoneyServicesInitializer
{
    private readonly MoneyControllerEventsHandler _moneyControllerEventsHandler;
    
    public MoneyServicesInitializer(MoneyControllerEventsHandler moneyControllerEventsHandler)
    {
        _moneyControllerEventsHandler = moneyControllerEventsHandler;
    }
    
    public void Initialize()
    {
        _moneyControllerEventsHandler.Initialize();
    }    
}