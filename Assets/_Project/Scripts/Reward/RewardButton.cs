using System;
using UnityEngine;
using UnityEngine.UI;

public class RewardButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _lockPanel;
    [SerializeField] private AdRewardType _rewardType;

    public event Action<RewardButton> Clicked;
    public AdRewardType RewardType => _rewardType;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        Clicked?.Invoke(this);
    }
    
    public void SetButtonState(bool isActive)
    {
        _lockPanel.SetActive(isActive == false);
        _button.interactable = isActive;
    }
}
