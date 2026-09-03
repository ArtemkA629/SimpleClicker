using UnityEngine;
using YG;

public class YGSaveSystem : ISaveSystem
{
    private SavesYG _savesYg = YG2.saves;
    
    public void Save<T>(string key, T value)
    {
        switch (key)
        {
            case SavingConstants.MoneyId:
                _savesYg.Money = value.ToString();
                break;
            case SavingConstants.GemsId:
                _savesYg.Gems = (int)(object)value;
                break;
            case SavingConstants.BoughtBuildingsId:
                _savesYg.Buildings = (BuildingsDatabase)(object)value;
                break;
            case SavingConstants.BoughtImprovementsId:
                _savesYg.Improvements = (ImprovementsDatabase)(object)value;
                break;
            case SavingConstants.LastLoginTimeId:
                _savesYg.LastLoginTime = (string)(object)value;
                break;
            case SavingConstants.RebirthId:
                _savesYg.Rebirth = (RebirthDatabase)(object)value;
                break;
            case SavingConstants.LocationsId:
                _savesYg.Locations = (LocationsDatabase)(object)value;
                break;
            case SavingConstants.SelectedLocationId:
                _savesYg.SelectedLocation = (string)(object)value;
                break;
            case SavingConstants.UnlockedPagesId:
                _savesYg.UnlockedPages = (PagesDatabase)(object)value;
                break;
            case SavingConstants.DailyRewardId:
                _savesYg.DailyReward = (DailyRewardSaveData)(object)value;
                break;
            case SavingConstants.AudioId:
                _savesYg.Audio = (AudioSaveData)(object)value;
                break;
            case SavingConstants.TutorialId:
                _savesYg.Tutorial = (TutorialSaveData)(object)value;
                break;
            default:
                Debug.LogError($"Unknown save key: {key}");
                return;
        }

        SaveAll();
    }

    public T Load<T>(string key, T defaultValue = default)
    {
        switch (key)
        {
            case SavingConstants.MoneyId:
                if (_savesYg.Money == null) return defaultValue;
                return (T)(object)_savesYg.Money;
            case SavingConstants.GemsId:
                return (T)(object)_savesYg.Gems;
            case SavingConstants.BoughtBuildingsId:
                if (_savesYg.Buildings == null)
                {
                    Save(SavingConstants.BoughtBuildingsId, defaultValue);
                    return defaultValue;
                }
                
                return (T)(object)_savesYg.Buildings;
            case SavingConstants.BoughtImprovementsId:
                if (_savesYg.Improvements == null) return defaultValue;
                return (T)(object)_savesYg.Improvements;
            case SavingConstants.LastLoginTimeId:
                if (_savesYg.LastLoginTime == null) return defaultValue;
                return (T)(object)_savesYg.LastLoginTime;
            case SavingConstants.RebirthId:
                if (_savesYg.Rebirth == null) return defaultValue;
                return (T)(object)_savesYg.Rebirth;
            case SavingConstants.LocationsId:
                if (_savesYg.Locations == null)
                {
                    Save(SavingConstants.LocationsId, defaultValue);
                    return defaultValue;
                }
                
                return (T)(object)_savesYg.Locations;
            case SavingConstants.SelectedLocationId:
                if (_savesYg.SelectedLocation == null) return defaultValue;
                return (T)(object)_savesYg.SelectedLocation;
            case SavingConstants.UnlockedPagesId:
                if (_savesYg.UnlockedPages == null) return defaultValue;
                return (T)(object)_savesYg.UnlockedPages;
            case SavingConstants.DailyRewardId:
                if (_savesYg.DailyReward == null) return defaultValue;
                return (T)(object)_savesYg.DailyReward;
            case SavingConstants.AudioId:
                if (_savesYg.Audio == null) return defaultValue;
                return (T)(object)_savesYg.Audio;
            case SavingConstants.TutorialId:
                if (_savesYg.Tutorial == null) return defaultValue;
                return (T)(object)_savesYg.Tutorial;
            default:
                Debug.LogError($"Unknown load key: {key}");
                return defaultValue;
        }
    }

    public bool HasKey(string key)
    {
        switch (key)
        {
            case SavingConstants.MoneyId:
                return _savesYg.Money != null;
            case SavingConstants.GemsId:
                return true;
            case SavingConstants.BoughtBuildingsId:
                return _savesYg.Buildings != null;
            case SavingConstants.BoughtImprovementsId:
                return _savesYg.Improvements != null;
            case SavingConstants.LastLoginTimeId:
                return _savesYg.LastLoginTime != null;
            case SavingConstants.RebirthId:
                return _savesYg.Rebirth != null;
            case SavingConstants.LocationsId:
                return _savesYg.Locations != null;
            case SavingConstants.SelectedLocationId:
                return _savesYg.SelectedLocation != null;
            case SavingConstants.UnlockedPagesId:
                return _savesYg.UnlockedPages != null;
            case SavingConstants.DailyRewardId:
                return _savesYg.DailyReward != null;
            case SavingConstants.AudioId:
                return _savesYg.Audio != null;
            case SavingConstants.TutorialId:
                return _savesYg.Tutorial != null;
            default:
                return false;
        }
    }

    public void Delete(string key)
    {
        switch (key)
        {
            case SavingConstants.MoneyId:
                _savesYg.Money = null;
                break;
            case SavingConstants.GemsId:
                _savesYg.Gems = 0;
                break;
            case SavingConstants.BoughtBuildingsId:
                _savesYg.Buildings = null;
                break;
            case SavingConstants.BoughtImprovementsId:
                _savesYg.Improvements = null;
                break;
            case SavingConstants.LastLoginTimeId:
                _savesYg.LastLoginTime = null;
                break;
            case SavingConstants.RebirthId:
                _savesYg.Rebirth = null;
                break;
            case SavingConstants.LocationsId:
                _savesYg.Locations = null;
                break;
            case SavingConstants.SelectedLocationId:
                _savesYg.SelectedLocation = null;
                break;
            case SavingConstants.UnlockedPagesId:
                _savesYg.UnlockedPages = null;
                break;
            case SavingConstants.DailyRewardId:
                _savesYg.DailyReward = null;
                break;
            case SavingConstants.AudioId:
                _savesYg.Audio = null;
                break;
            case SavingConstants.TutorialId:
                _savesYg.Tutorial = null;
                break;
            default:
                Debug.LogError($"Unknown delete key: {key}");
                return;
        }

        SaveAll();
    }

    private void SaveAll()
    {
        YG2.SaveProgress();
    }
}
