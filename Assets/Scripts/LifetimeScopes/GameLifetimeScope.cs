using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private Clicker _clicker;
    [SerializeField] private ClickerView _clickerView;
    [SerializeField] private UpgradesController _upgradesController;
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private ChocolateRain _chocolateRain;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_clicker);
        builder.RegisterComponent(_clickerView);
        builder.RegisterComponent(_upgradesController);
        builder.RegisterComponent(_saveSystem);
        builder.RegisterComponent(_chocolateRain);
        builder.RegisterEntryPoint<GameEntryPoint>();
    }
}
