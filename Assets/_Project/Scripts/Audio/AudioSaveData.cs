using System;
using UnityEngine;

[Serializable]
public class AudioSaveData
{
    [SerializeField] private bool _musicEnabled;
    [SerializeField] private bool _sfxEnabled;

    public AudioSaveData()
    {
        _musicEnabled = true;
        _sfxEnabled = true;
    }

    public bool MusicEnabled => _musicEnabled;
    public bool SfxEnabled => _sfxEnabled;

    public void SetMusicEnabled(bool enabled)
    {
        _musicEnabled = enabled;
    }

    public void SetSfxEnabled(bool enabled)
    {
        _sfxEnabled = enabled;
    }
}
