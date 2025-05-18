using UnityEngine;
using UnityEngine.UI;

public class UpgradesButton : CustomButton
{
    [SerializeField] private GameObject _upgrade;
    [SerializeField] private GameObject _followers;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _followersButton;

    protected override void OnClick()
    {
        _upgrade.SetActive(true);
        _followers.SetActive(false);
        _upgradeButton.enabled = false;
        _followersButton.enabled = true;
        YGAdsProvider.TryShowFullScreenAdWithChance();
    }
}
