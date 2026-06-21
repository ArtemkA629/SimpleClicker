public class GemsServicesInitializer
{
    private readonly GemsControllerEventsHandler _gemsControllerEventsHandler;
    
    public GemsServicesInitializer(GemsControllerEventsHandler gemsControllerEventsHandler)
    {
        _gemsControllerEventsHandler = gemsControllerEventsHandler;
    }
    
    public void Initialize()
    {
        _gemsControllerEventsHandler.Initialize();
    }    
}
