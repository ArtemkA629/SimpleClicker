using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DailyRewardPopup : MonoBehaviour
{
    [SerializeField] private Popup _popup;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _dayDescription;
    [SerializeField] private TextMeshProUGUI _rewardDescription;
    [SerializeField] private Button _claimButton;

    private UnityAction _claimAction;
    
    private UnityAction Hiding => () => _popup.Hide();

    private void OnEnable()
    {
        _claimButton.onClick.AddListener(Hiding);
    }

    private void OnDisable()
    {
        if (_claimAction != null)
        {
            _claimButton.onClick.RemoveListener(_claimAction);
        }
        
        _claimButton.onClick.RemoveListener(Hiding);
    }

    public void Display(Sprite icon, int day, string description, UnityAction claimAction)
    {
        _popup.Show();
        _icon.sprite = icon;
        _dayDescription.text = $"Day {day}";
        _rewardDescription.text = description;
        _claimAction = claimAction;
        _claimButton.onClick.AddListener(claimAction);
        
    }
}