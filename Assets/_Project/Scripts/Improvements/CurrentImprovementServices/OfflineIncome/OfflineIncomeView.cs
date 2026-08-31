using System;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OfflineIncomeView : MonoBehaviour
{
    [SerializeField] private Popup _popup;
    [SerializeField] private Button _claimButton;
    [SerializeField] private TextMeshProUGUI _incomeText;

    private UnityAction _claimAction;

    private void Start()
    {
        _claimButton.onClick.AddListener(_popup.Hide);
    }

    private void OnDestroy()
    {
        _claimButton.onClick.RemoveListener(_claimAction);
        _claimButton.onClick.RemoveListener(_popup.Hide);
    }

    public void ShowPopup(BigInteger income, UnityAction action)
    {
        _claimAction = action;
        _popup.Show();
        _claimButton.onClick.AddListener(action);
        _incomeText.text = "While you were out, you earned " + income.ToShortValue();
    }
}