using System.Collections;
using UnityEngine;
using VContainer;
using YG;

public class SaveService : MonoBehaviour
{
    private static float _autoSavingDelay = 4f;

    [Inject] private UpgradesController _upgradesController;
    [Inject] private StylesController _stylesController;
    [Inject] private Clicker _clicker;
    [Inject] private Coins _coins;

    private ISaveSystem _saveSystem;

    public SaveData Data => _saveSystem.Load();

    public void Init()
    {
        _saveSystem = new YGSaveSystem();
        InjectData();
        StartCoroutine(AutoSave());

        _clicker.Model.DataChanged += OnDataChanged;
        _upgradesController.DataChanged += OnDataChanged;
        _coins.Model.Changed += OnDataChanged;
        _stylesController.DataChanged += OnDataChanged;
    }

    private void OnApplicationQuit()
    {
        SaveData();
        YandexGame.SaveProgress();
    }

    private void OnDisable()
    {
        _clicker.Model.DataChanged -= OnDataChanged;
        _upgradesController.DataChanged -= OnDataChanged;
        _coins.Model.Changed -= OnDataChanged;
        _stylesController.DataChanged -= OnDataChanged;
    }

    private void SaveData()
    {
        var clickerModel = _clicker.Model;
        _saveSystem.Save(new SaveData(
            clickerModel.ClicksCount,
            clickerModel.Level,
            clickerModel.CurrentNextLevelClicksCount,
            clickerModel.PerClickStat,
            _upgradesController.BoughtUpgradeNumbers,
            _stylesController.BoughtStylesNumbers,
            _stylesController.ChosenStyle,
            _coins.Amount
        ));
    }

    private void InjectData()
    {
        _clicker.Init(Data);
        _upgradesController.Init(Data.BoughtUpgradeNumbers);
        _stylesController.Init(Data.BoughtStylesNumbers, Data.ChosenStyle);
        _coins.Init(Data.Coins);
    }

    private void OnDataChanged()
    {
        SaveData();
    }

    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(_autoSavingDelay);
            YandexGame.SaveProgress();
        }
    }

    [ContextMenu("ResetData")]
    private void ResetData()
    {
        PlayerPrefs.DeleteAll();
        YandexGame.ResetSaveProgress();
        InjectData();
    }
}
