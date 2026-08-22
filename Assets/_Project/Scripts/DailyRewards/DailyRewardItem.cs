using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DailyRewardItem : MonoBehaviour
{
    [SerializeField] private Image _rewardIcon;
    [SerializeField] private TextMeshProUGUI _dayText;
    [SerializeField] private TextMeshProUGUI _rewardText;
    [SerializeField] private Button _claimButton;
    [SerializeField] private GameObject _claimedIndicator;
    
    private int _day;
    private bool _isClaimed;
    private UnityAction _claimAction;
    
    public int Day => _day;

    private void OnDisable()
    {
        if (_claimAction != null)
            _claimButton.onClick.RemoveListener(_claimAction);
    }

    public void Initialize(int day, string description, Sprite rewardIcon, bool isClaimed)
    {
        _day = day;
        _isClaimed = isClaimed;
        _rewardIcon.sprite = rewardIcon;
        _dayText.text = $"Day {day}";
        _rewardText.text = description;
        UpdateVisualState();
    }
    
    public void SetClaimAction(UnityAction onClaim)
    {
        _claimAction = onClaim;
        _claimButton.onClick.AddListener(onClaim);
    }
    
    public void SetClaimed()
    {
        _isClaimed = true;
        UpdateVisualState();
    }
    
    private void UpdateVisualState()
    {
        _claimButton.interactable = _isClaimed == false;
        _claimedIndicator.SetActive(_isClaimed);
    }
}
