using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioView : MonoBehaviour
{
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Toggle _sfxToggle;

    public event Action MusicToggleClicked;
    public event Action SfxToggleClicked;

    private void Awake()
    {
        _musicToggle.onValueChanged.AddListener(OnMusicToggleChanged);
        _sfxToggle.onValueChanged.AddListener(OnSfxToggleChanged);
    }

    private void OnDestroy()
    {
        _musicToggle.onValueChanged.RemoveListener(OnMusicToggleChanged);
        _sfxToggle.onValueChanged.RemoveListener(OnSfxToggleChanged);
    }

    private void OnMusicToggleChanged(bool isOn)
    {
        MusicToggleClicked?.Invoke();
    }

    private void OnSfxToggleChanged(bool isOn)
    {
        SfxToggleClicked?.Invoke();
    }

    public void SetMusicToggleState(bool isEnabled)
    {
        if (_musicToggle != null)
            _musicToggle.isOn = isEnabled;
    }

    public void SetSfxToggleState(bool isEnabled)
    {
        if (_sfxToggle != null)
            _sfxToggle.isOn = isEnabled;
    }
}
