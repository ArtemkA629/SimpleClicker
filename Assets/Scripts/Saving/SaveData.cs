public struct SaveData
{
    private readonly int _clicksCount;
    private readonly int _level;
    private readonly int _currentNextLevelClicksCount;
    private readonly int _perClickStat;

    public int ClicksCount => _clicksCount;
    public int Level => _level;
    public int CurrentNextLevelClicksCount => _currentNextLevelClicksCount;
    public int PerClickStat => _perClickStat;

    public SaveData(int clicksCount, int level, int currentNextLevelClicksCount, int perClickStat)
    {
        _clicksCount = clicksCount;
        _level = level;
        _currentNextLevelClicksCount = currentNextLevelClicksCount;
        _perClickStat = perClickStat;
    }
}
