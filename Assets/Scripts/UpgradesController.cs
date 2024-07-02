using System.Collections.Generic;
using UnityEngine;

public class UpgradesController : MonoBehaviour
{
    [SerializeField] private BuyUpgradeButton[] _buyUpgradeButtons;

    private List<int> _boughtUpgradeNumbers;

    public List<int> BoughtUpgradeNumbers => _boughtUpgradeNumbers;

    private void OnDisable()
    {
        foreach (var upgradeButton in _buyUpgradeButtons)
            upgradeButton.Disabled -= OnDisabled;
    }

    public void OnLoad(List<int> boughtUpgradeNumbers)
    {
        _boughtUpgradeNumbers = boughtUpgradeNumbers;
        UpdateView();
        foreach (var upgradeButton in _buyUpgradeButtons)
            upgradeButton.Disabled += OnDisabled;
    }

    private void UpdateView()
    {
        foreach (var boughtUpgradeNumber in _boughtUpgradeNumbers)
            _buyUpgradeButtons[boughtUpgradeNumber - 1].Disable();
    }

    private void OnDisabled(int number)
    {
        _boughtUpgradeNumbers.Add(number);
    }
}
