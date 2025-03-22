using VContainer;
using VContainer.Unity;

public class GameEntryPoint : IStartable
{
    [Inject] private Clicker _clicker;
    [Inject] private SaveSystem _saveSystem;
    [Inject] private ChocolateRain _chocolateRain;

    public void Start()
    {
        _clicker.Init();
        _saveSystem.Init();
        _chocolateRain.Init();
    }
}
