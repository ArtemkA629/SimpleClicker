using UnityEngine;

public class LocalizationView : MonoBehaviour
{
    public void Initialize()
    {
        var localizationTexts = FindObjectsOfType<LocalizationText>();
        
        foreach (var localizationText in localizationTexts)
        {
            localizationText.UpdateText();
        }
    }
}