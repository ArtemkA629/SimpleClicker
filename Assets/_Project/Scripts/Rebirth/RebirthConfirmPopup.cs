using System;
using UnityEngine;
using UnityEngine.UI;

public class RebirthConfirmPopup : MonoBehaviour
{
    [SerializeField] private Popup _popup;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button _closeButton;

    public event Action ConfirmButtonClicked;
    
    private void OnEnable()
    {
        _confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        _cancelButton.onClick.AddListener(OnCancelButtonClicked);
        _closeButton.onClick.AddListener(OnCloseButtonClicked);
    }
    
    private void OnDisable()
    {
        _confirmButton.onClick.RemoveListener(OnConfirmButtonClicked);
        _cancelButton.onClick.RemoveListener(OnCancelButtonClicked);
        _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
    }
    
    public void Show()
    {
        _popup.Show();
    }
    
    public void Hide()
    {
        _popup.Hide();
    }
    
    private void OnConfirmButtonClicked()
    {
        ConfirmButtonClicked?.Invoke();
    }
    
    private void OnCancelButtonClicked()
    {
        _popup.Hide();
    }
    
    private void OnCloseButtonClicked()
    {
        _popup.Hide();
    }
}