using System;

public class GemsControllerEventsHandler : IDisposable
{
    private readonly GemsController _gemsController;
    private readonly ISaveSystem _saveSystem;
    
    public GemsControllerEventsHandler(GemsController gemsController, ISaveSystem saveSystem)
    {
        _gemsController = gemsController;
        _saveSystem = saveSystem;
    }

    public void Initialize()
    {
        _gemsController.GemsAmountChanged += OnGemsAmountChanged;
    }

    public void Dispose()
    {
        _gemsController.GemsAmountChanged -= OnGemsAmountChanged;
    }

    private void OnGemsAmountChanged()
    {
        _saveSystem.Save(SavingConstants.GemsId, _gemsController.Amount);
    }
}
