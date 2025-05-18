using System.Collections.Generic;
using YG;

public struct SaveData
{
    private readonly int _clicksCount;
    private readonly int _level;
    private readonly int _currentNextLevelClicksCount;
    private readonly int _perClickStat;
    private readonly List<int> _boughtUpgradeNumbers;
    private readonly List<int> _boughtStylesNumbers;
    private readonly int _chosenStyle;
    private readonly int _coins;

    public int ClicksCount => _clicksCount;
    public int Level => _level;
    public int CurrentNextLevelClicksCount => _currentNextLevelClicksCount;
    public int PerClickStat => _perClickStat;
    public List<int> BoughtUpgradeNumbers => _boughtUpgradeNumbers;
    public List<int> BoughtStylesNumbers => _boughtStylesNumbers;
    public int ChosenStyle => _chosenStyle;
    public int Coins => _coins;

    public SaveData(int clicksCount, int level, int currentNextLevelClicksCount, int perClickStat, List<int> boughtUpgradeNumbers, 
        List<int> boughtStylesNumbers, int chosenStyle, int coins)
    {
        _clicksCount = clicksCount;
        _level = level;
        _currentNextLevelClicksCount = currentNextLevelClicksCount;
        _perClickStat = perClickStat;
        _boughtUpgradeNumbers = boughtUpgradeNumbers;
        _boughtStylesNumbers = boughtStylesNumbers;
        _chosenStyle = chosenStyle;
        _coins = coins;
    }

    public SaveData(SavesYG saveData)
    {
        _clicksCount = saveData.ClicksCount;
        _level = saveData.Level;
        _currentNextLevelClicksCount = saveData.CurrentNextLevelClicksCount;
        _perClickStat = saveData.PerClickStat;
        _boughtUpgradeNumbers = saveData.BoughtUpgradeNumbers;
        _boughtStylesNumbers = saveData.BoughtStylesNumbers;
        _chosenStyle = saveData.ChosenStyle;
        _coins = saveData.Coins;
    }
}
