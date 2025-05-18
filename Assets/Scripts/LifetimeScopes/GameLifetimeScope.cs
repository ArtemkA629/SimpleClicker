using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private Clicker _clicker;
    [SerializeField] private ClickerView _clickerView;
    [SerializeField] private UpgradesController _upgradesController;
    [SerializeField] private StylesController _stylesController;
    [SerializeField] private ChocolateRainPool _chocolateRain;
    [SerializeField] private SaveService _saveService;
    [SerializeField] private Coins _coins;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_clicker);
        builder.RegisterComponent(_clickerView);
        builder.RegisterComponent(_upgradesController);
        builder.RegisterComponent(_chocolateRain);
        builder.RegisterComponent(_saveService);
        builder.RegisterComponent(_coins);
        builder.RegisterComponent(_stylesController);
        builder.RegisterEntryPoint<GameEntryPoint>();
    }
}
