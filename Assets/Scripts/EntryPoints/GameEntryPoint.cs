using VContainer;
using VContainer.Unity;

public class GameEntryPoint : IStartable
{
    [Inject] private SaveService _saveService;
    [Inject] private ChocolateRainPool _chocolateRain;

    public void Start()
    {
        _saveService.Init();
        _chocolateRain.Init();
    }
}
