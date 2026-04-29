using System;

public class LastLoginTimeSaver : IDisposable
{
    private readonly ISaveSystem _saveSystem;
    
    public LastLoginTimeSaver(ISaveSystem saveSystem)
    {
        _saveSystem = saveSystem;
    }
    
    public DateTime Load()
    {
        string dateTime = _saveSystem.Load(SavingConstants.LastLoginTimeId, DateTime.UtcNow.ToString("o"));
        return DateTime.Parse(dateTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
    }

    private void Save()
    {
        string dateTime = DateTime.UtcNow.ToString("o");
        _saveSystem.Save(SavingConstants.LastLoginTimeId, dateTime);
    }

    public void Dispose()
    {
        Save();
    }
}