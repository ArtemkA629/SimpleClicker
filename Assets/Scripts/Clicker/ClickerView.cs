using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _howManyLeftClicksText;
    [SerializeField] private TextMeshProUGUI _perClickStatText;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _clickAnimationPrefab;
    [SerializeField] private Image _progressBar;
    [SerializeField] private float _animateDistance = 150f;
    [SerializeField] private float _animateDuration = 2f;

    private ClickerPresenter _presenter;

    public void Init(ClickerPresenter presenter)
    {
        _presenter = presenter;
    }

    public void OnClick()
    {
        _presenter.OnClick();
    }

    public void OnBuyUpgrade(int multiplyRatio, BuyUpgradeButton clickedButton)
    {
        _presenter.OnBuyUpgrade(multiplyRatio, clickedButton);
    }

    public void UpdateProgressAfterClick(int level, int clicksCount, int currentNextLevelClicksCount)
    {
        _levelText.text = "Level " + level.ToString();
        CommonUpdate(clicksCount, currentNextLevelClicksCount);
        AnimateClick();
    }

    public void UpdateProgressAfterUpgrade(int clicksCount, int currentNextLevelClicksCount, int perClickStat)
    {
        CommonUpdate(clicksCount, currentNextLevelClicksCount);
        _perClickStatText.text = perClickStat.ToString() + "/per click";
    }

    public void UpdateAll(int clicksCount, int level, int currentNextLevelClicksCount, int perClickStat)
    {
        _levelText.text = "Level " + level.ToString();
        CommonUpdate(clicksCount, currentNextLevelClicksCount);
        _perClickStatText.text = perClickStat.ToString() + "/per click";
    }

    private void CommonUpdate(int clicksCount, int currentNextLevelClicksCount)
    {
        _howManyLeftClicksText.text = (currentNextLevelClicksCount - clicksCount).ToString() + " left";
        _progressBar.fillAmount = (float)clicksCount / currentNextLevelClicksCount;
    }

    private void AnimateClick()
    {
        var clickAnimation = Instantiate(_clickAnimationPrefab, _canvas.transform);
        clickAnimation.transform.position = Input.mousePosition;
        clickAnimation.transform.DOMoveY(clickAnimation.transform.position.y + _animateDistance, _animateDuration);
        clickAnimation.DOFade(0f, _animateDuration).OnComplete(() => Destroy(clickAnimation.gameObject));
    }
}
