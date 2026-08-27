using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TemporaryBoostView : MonoBehaviour
{
    [SerializeField] private GameObject _boostIndicator;
    [SerializeField] private Image _timerFill;
    [SerializeField] private TextMeshProUGUI _timerText;
    
    public void Initialize()
    {
        _boostIndicator.SetActive(false);
    }
    
    public void ShowBoost()
    {
        _boostIndicator.SetActive(true);
    }
    
    public void HideBoost()
    {
        _boostIndicator.SetActive(false);
    }
    
    public void UpdateTimer(float remainingTime, float totalTime)
    {
        _timerFill.fillAmount = remainingTime / totalTime;
        
        int seconds = Mathf.FloorToInt(remainingTime);
        int milliseconds = Mathf.FloorToInt((remainingTime - seconds) * 100);
        _timerText.text = $"{seconds:00}:{milliseconds:00}";
    }
}
