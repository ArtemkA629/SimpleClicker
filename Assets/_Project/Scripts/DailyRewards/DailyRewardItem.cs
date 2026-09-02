using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class DailyRewardItem : MonoBehaviour
{
    [SerializeField] private Image _rewardIcon;
    [SerializeField] private TextMeshProUGUI _dayText;
    [SerializeField] private TextMeshProUGUI _rewardText;
    [SerializeField] private GameObject _claimedIndicator;
    
    private int _day;
    private bool _isClaimed;
    private UnityAction _claimAction;
    private ILocalizationService _localizationService;
    
    public int Day => _day;

    [Inject]
    private void Construct(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    public void Initialize(int day, string description, Sprite rewardIcon, bool isClaimed)
    {
        _day = day;
        _isClaimed = isClaimed;
        _rewardIcon.sprite = rewardIcon;
        _dayText.text = $"{_localizationService.GetText(LocalizationConstants.Day)} {day}";
        _rewardText.text = description;
        UpdateVisualState();
    }
    
    public void SetClaimed()
    {
        _isClaimed = true;
        UpdateVisualState();
    }
    
    private void UpdateVisualState()
    {
        _claimedIndicator.SetActive(_isClaimed);
    }
}
