using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class SaveSystem : MonoBehaviour
{
    [Inject] private readonly UpgradesController _upgradesController;
    [Inject] private readonly Clicker _clicker;

    [SerializeField] private Transform _upgrade;

    private SaveData _saveData;

    public void Init()
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
        for (int i = 1; i <= _upgrade.childCount; i++)
        {
            if (PlayerPrefs.GetInt(SaveSystemConstants.Upgrade + i) == 0)
                continue;

            boughtUpgradeNumbers.Add(i);
        }

        _saveData = new SaveData(clicksCount, level, currentNextLevelClicksCount, perClickStat);
        _clicker.OnLoad(_saveData);
        _upgradesController.OnLoad(boughtUpgradeNumbers);
    }

    private void OnDisable()
    {
        var data = _clicker.GetSaveData();
        PlayerPrefs.SetInt(SaveSystemConstants.ClicksCount, data.ClicksCount);
        PlayerPrefs.SetInt(SaveSystemConstants.Level, data.Level);
        PlayerPrefs.SetInt(SaveSystemConstants.CurrentNextLevelClicksCount, data.CurrentNextLevelClicksCount);
        PlayerPrefs.SetInt(SaveSystemConstants.PerClickStat, data.PerClickStat);
        foreach (var boughtUpgradeNumber in _upgradesController.BoughtUpgradeNumbers)
            PlayerPrefs.SetInt(SaveSystemConstants.Upgrade + boughtUpgradeNumber, boughtUpgradeNumber);
    }

    [ContextMenu("ResetData")]
    private void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
}
