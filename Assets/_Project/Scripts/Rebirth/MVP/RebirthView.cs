using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RebirthView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rebirthLevelText;
    [SerializeField] private TextMeshProUGUI _requiredMoneyText;
    [SerializeField] private TextMeshProUGUI _gemsRewardText;
    [SerializeField] private Image _moneyFillBar;
    [SerializeField] private Button _rebirthButton;
    [SerializeField] private GameObject _rewardPanel;
    [SerializeField] private GameObject _moneyBar;
    [SerializeField] private GameObject _maxRebirthModeObject;
    [SerializeField] private RebirthConfirmPopup _confirmPopup;
    
    private RebirthPresenter _presenter;
    private ILocalizationService _localizationService;

    private void OnDestroy()
    {
        _rebirthButton.onClick.RemoveListener(OnRebirthButtonClicked);
        _confirmPopup.ConfirmButtonClicked -= OnConfirmButtonClicked;
    }

    [Inject]
    private void Construct(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    public void Initialize(RebirthPresenter presenter)
    {
        _presenter = presenter;
        
        _rebirthButton.onClick.AddListener(OnRebirthButtonClicked);
        _confirmPopup.ConfirmButtonClicked += OnConfirmButtonClicked;
    }
    
    public void DisplayCurrentLevel(int rebirthLevel)
    {
        _rebirthLevelText.text = $"{_localizationService.GetText(LocalizationConstants.CurrentLevel)}: " 
                                 + rebirthLevel;
    }

    public void UpdateViewOnMoneyChanged(BigInteger currentMoney, BigInteger requiredMoney)
    {
        _requiredMoneyText.text = $"{_localizationService.GetText(LocalizationConstants.RequiredMoney)}: " +
                                  $"{currentMoney.ToShortValue()}/{requiredMoney.ToShortValue()}";
        _moneyFillBar.fillAmount = requiredMoney == 0 ? 1f : currentMoney.Divide(requiredMoney);
        _rebirthButton.interactable = currentMoney >= requiredMoney;
    }
    
    public void DisplayGemsReward(int gemsReward)
    {
        _gemsRewardText.text = gemsReward.ToShortValue();
    }

    public void DisplayMaxLevelMode()
    {
        _maxRebirthModeObject.SetActive(true);
        _rewardPanel.SetActive(false);
        _moneyBar.SetActive(false);
        _rebirthButton.gameObject.SetActive(false);
    }
    
    private void OnRebirthButtonClicked()
    {
        _confirmPopup.Show();
    }
    
    private void OnConfirmButtonClicked()
    {
        _presenter.TryPerformRebirth();
        _confirmPopup.Hide();
    }
}