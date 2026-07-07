public class LocationItemsEventsHandler
{
    private readonly LocationsChanger _locationsChanger;
    
    private LocationItem[] _items;
    
    public LocationItemsEventsHandler(LocationsChanger locationsChanger)
    {
        _locationsChanger = locationsChanger;
    }
    
    public void Initialize(LocationItem[] items)
    {
        _items = items;
        
        foreach (var item in _items)
        {
            item.Clicked += OnItemClicked;
        }
    }
    
    private void OnItemClicked(string name)
    {
        _locationsChanger.SetLocation(name);
    }
}