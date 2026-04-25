using UnityEngine;

public class PowerClickDescriptionCreator : IImprovementDescriptionCreator
{
    public string GetDescription(string descriptionTemplate, ImprovementLevelInfo levelInfo)
    {
        PowerClickLevelInfo powerClickLevelInfo = (PowerClickLevelInfo)levelInfo;

        if (powerClickLevelInfo == null)
        {
            Debug.LogError("ImprovementLevelInfo is not PowerClickLevelInfo");
            return "";
        }
        
        return descriptionTemplate.Replace(
            ImprovementsConstants.PowerMultiplierTemplateKey, 
            powerClickLevelInfo.PowerClickMultiplier.ToString("0.##"));
    }
}