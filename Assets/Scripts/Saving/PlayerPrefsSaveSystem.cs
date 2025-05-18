using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsSaveSystem : ISaveSystem
{
    private Transform _upgradesParent;
    private Transform _stylesParent;

    public PlayerPrefsSaveSystem(Transform upgradesParent, Transform stylesParent)
    {
        _upgradesParent = upgradesParent;
        _stylesParent = stylesParent;
    }

    public void Save(SaveData data)
    {
        PlayerPrefs.SetInt(SaveSystemConstants.ClicksCount, data.ClicksCount);
        PlayerPrefs.SetInt(SaveSystemConstants.Level, data.Level);
        PlayerPrefs.SetInt(SaveSystemConstants.CurrentNextLevelClicksCount, data.CurrentNextLevelClicksCount);
        PlayerPrefs.SetInt(SaveSystemConstants.PerClickStat, data.PerClickStat);
        foreach (var boughtUpgradeNumber in data.BoughtUpgradeNumbers)
            PlayerPrefs.SetInt(SaveSystemConstants.Upgrade + boughtUpgradeNumber, boughtUpgradeNumber);
        foreach (var boughtStylesNumber in data.BoughtStylesNumbers)
            PlayerPrefs.SetInt(SaveSystemConstants.Style + boughtStylesNumber, boughtStylesNumber);
        PlayerPrefs.SetInt(SaveSystemConstants.ChosenStyle, data.ChosenStyle);
        PlayerPrefs.SetInt(SaveSystemConstants.CoinsAmont, data.Coins);
    }

    
    public SaveData Load()
    {
        int clicksCount = PlayerPrefs.GetInt(SaveSystemConstants.ClicksCount);
        int savedLevel = PlayerPrefs.GetInt(SaveSystemConstants.Level);
        int level = savedLevel == 0 ? DefaultStatsConstants.DefaultLevel : savedLevel;
        int savedCurrentNextLevelClicksCount = PlayerPrefs.GetInt(SaveSystemConstants.CurrentNextLevelClicksCount);
        int currentNextLevelClicksCount = savedCurrentNextLevelClicksCount == 0 ? DefaultStatsConstants.DefaultCurrentNextLevelClicksCount
            : savedCurrentNextLevelClicksCount;
        int savedPerClickStat = PlayerPrefs.GetInt(SaveSystemConstants.PerClickStat);
        int perClickStat = savedPerClickStat == 0 ? DefaultStatsConstants.DefaultPerClickStat : savedPerClickStat;

        var boughtUpgradeNumbers = new List<int>();
        for (int i = 1; i <= _upgradesParent.childCount; i++)
        {
            if (PlayerPrefs.GetInt(SaveSystemConstants.Upgrade + i) == 0)
                continue;

            boughtUpgradeNumbers.Add(i);
        }

        var boughtStylesNumbers = new List<int>();
        for (int i = 1; i <= _stylesParent.childCount; i++)
        {
            if (PlayerPrefs.GetInt(SaveSystemConstants.Style + i) == 0)
                continue;

            boughtStylesNumbers.Add(i);
        }

        int chosenStyle = PlayerPrefs.GetInt(SaveSystemConstants.ChosenStyle) == 0 ? 1 : PlayerPrefs.GetInt(SaveSystemConstants.ChosenStyle);
        int coinsAmount = PlayerPrefs.GetInt(SaveSystemConstants.CoinsAmont);

        return new SaveData(clicksCount, level, currentNextLevelClicksCount, perClickStat, boughtUpgradeNumbers, boughtStylesNumbers, chosenStyle, coinsAmount);
    }
}
