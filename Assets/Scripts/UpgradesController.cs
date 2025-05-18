using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesController : MonoBehaviour
{
    [SerializeField] private BuyUpgradeButton[] _buyUpgradeButtons;
    [SerializeField] private RectTransform _upgradesParent;

    private List<int> _boughtUpgradeNumbers;

    public event Action DataChanged;

    public List<int> BoughtUpgradeNumbers => _boughtUpgradeNumbers;
    public Transform UpgradesParent => _upgradesParent;

    public void Init(List<int> boughtUpgradeNumbers)
    {
        _boughtUpgradeNumbers = boughtUpgradeNumbers;
        UpdateView();
        foreach (var upgradeButton in _buyUpgradeButtons)
            upgradeButton.Disabled += OnDisabled;
    }

    private void OnDisable()
    {
        foreach (var upgradeButton in _buyUpgradeButtons)
            upgradeButton.Disabled -= OnDisabled;
    }

    private void UpdateView()
    {
        for (int i = 1; i <= _upgradesParent.childCount; i++)
        {
            if (_boughtUpgradeNumbers.Contains(i))
                _buyUpgradeButtons[i - 1].Disable();
            else
                _buyUpgradeButtons[i - 1].Enable();
        }
    }

    private void OnDisabled(int number)
    {
        _boughtUpgradeNumbers.Add(number);
        DataChanged?.Invoke();
        YGAdsProvider.TryShowFullScreenAdWithChance();
    }
}
