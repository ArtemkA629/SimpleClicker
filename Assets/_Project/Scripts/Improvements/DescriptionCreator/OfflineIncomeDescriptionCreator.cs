using UnityEngine;

public class OfflineIncomeDescriptionCreator : IImprovementDescriptionCreator
{
    public string GetDescription(string descriptionTemplate, ImprovementLevelInfo levelInfo)
    {
        OfflineIncomeLevelInfo offlineIncomeLevelInfo = (OfflineIncomeLevelInfo)levelInfo;

        if (offlineIncomeLevelInfo == null)
        {
            Debug.LogError("ImprovementLevelInfo is not OfflineIncomeLevelInfo");
            return "";
        }
        
        return descriptionTemplate.Replace(
            ImprovementsConstants.OfflineIncomePercentKey, 
            offlineIncomeLevelInfo.TotalIncomePercentPerDay.ToString());
    }
}