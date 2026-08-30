using UnityEngine;
using UnityEngine.UI;

public class CustomToggle : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private GameObject _disabledToggle;

    private void Start()
    {
        var toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnChanged);
        OnChanged(toggle.isOn);
    }

    private void OnChanged(bool isActive)
    {
        _background.color = isActive ? Color.green : Color.grey;
        _disabledToggle.SetActive(!isActive);
    }
}
