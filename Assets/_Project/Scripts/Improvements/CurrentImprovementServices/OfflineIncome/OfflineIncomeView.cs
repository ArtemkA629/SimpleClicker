using System;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class OfflineIncomeView : MonoBehaviour
{
    [SerializeField] private Popup _popup;
    [SerializeField] private Button _claimButton;
    [SerializeField] private TextMeshProUGUI _incomeText;

    private UnityAction _claimAction;
    private ILocalizationService _localizationService;

    private void Start()
    {
        _claimButton.onClick.AddListener(_popup.Hide);
    }

    private void OnDestroy()
    {
        if (_claimAction != null)
        {
            _claimButton.onClick.RemoveListener(_claimAction);
        }
        
        _claimButton.onClick.RemoveListener(_popup.Hide);
    }

    [Inject]
    private void Construct(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    
    public void ShowPopup(BigInteger income, UnityAction action)
    {
        _claimAction = action;
        _popup.Show();
        _claimButton.onClick.AddListener(action);
        _incomeText.text = $"{_localizationService.GetText(LocalizationConstants.OfflineIncomePrescription)} " 
                           + income.ToShortValue();
    }
}