using System;

public class ClickerModel
{
    private readonly ClickerView _view;

    private int _clicksCount;
    private int _currentNextLevelClicksCount;
    private int _level;
    private int _perClickStat;

    public ClickerModel(ClickerView view)
    {
        _view = view;
    }

    public void AddClick()
    {
        _clicksCount += 1 * _perClickStat;
        if (_clicksCount >= _currentNextLevelClicksCount)
        {
            _level += 1;
            if (_clicksCount - _currentNextLevelClicksCount < 0)
                throw new Exception("Invalid clicksCount");

            _clicksCount -= _currentNextLevelClicksCount;
            _currentNextLevelClicksCount *= 2;
        }

        _view.UpdateProgressAfterClick(_level, _clicksCount, _currentNextLevelClicksCount);
    }

    public void ApplyUpgrade(int multiplyRatio, int price, BuyUpgradeButton clickedButton)
    {
        if (_clicksCount < price)
            return;

        if (_clicksCount - price < 0)
            throw new Exception("Invalid clicksCount");

        _clicksCount -= price;
        _perClickStat *= multiplyRatio;
        _view.UpdateProgressAfterUpgrade(_clicksCount, _currentNextLevelClicksCount, _perClickStat);
        clickedButton.Disable();
    }

    public void LoadData(SaveData saveData)
    {
        _clicksCount = saveData.ClicksCount;
        _level = saveData.Level;
        _currentNextLevelClicksCount = saveData.CurrentNextLevelClicksCount;
        _perClickStat = saveData.PerClickStat;
        _view.UpdateAll(_clicksCount, _level, _currentNextLevelClicksCount, _perClickStat);
    }

    public SaveData GetSaveData()
    {
        return new SaveData(_clicksCount, _level, _currentNextLevelClicksCount, _perClickStat);
    }
}
