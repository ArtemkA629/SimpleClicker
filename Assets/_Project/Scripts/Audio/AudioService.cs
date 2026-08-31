using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using Zenject;

public class AudioService : ITickable
{
    private readonly AudioConfig _config;
    private readonly ISaveSystem _saveSystem;
    
    private List<AudioSource> _activeAudioSources = new();
    private AudioSaveData _saveData;
    private Transform _soundsParent;

    public bool MusicEnabled => _saveData.MusicEnabled;
    public bool SfxEnabled => _saveData.SfxEnabled;

    public AudioService(Transform soundsParent, IConfigProvider configProvider, ISaveSystem saveSystem)
    {
        _soundsParent = soundsParent;
        _config = configProvider.Get<AudioConfig>();
        _saveSystem = saveSystem;
    }

    public void Initialize()
    {
        LoadSettings();
        ApplyMusicVolume();
        ApplySfxVolume();
    }

    public void Tick()
    {
        Cleanup();
    }

    private void Cleanup()
    {
        for (int i = _activeAudioSources.Count - 1; i >= 0; i--)
        {
            AudioSource audioSource = _activeAudioSources[i];
            
            if (audioSource == null || !audioSource.isPlaying)
            {
                if (audioSource != null)
                    Object.Destroy(audioSource.gameObject);
                
                _activeAudioSources.RemoveAt(i);
            }
        }
    }

    public void ToggleMusic()
    {
        _saveData.SetMusicEnabled(!_saveData.MusicEnabled);
        ApplyMusicVolume();
        SaveSettings();
    }

    public void ToggleSfx()
    {
        _saveData.SetSfxEnabled(!_saveData.SfxEnabled);
        ApplySfxVolume();
        SaveSettings();
    }

    public void PlaySound(AudioClip clip)
    {
        if (!_saveData.SfxEnabled || clip == null)
            return;

        GameObject soundObject = new GameObject("Sound_" + clip.name);
        soundObject.transform.SetParent(_soundsParent);
        
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.playOnAwake = false;
        audioSource.outputAudioMixerGroup = _config.AudioMixer.FindMatchingGroups(_config.SfxVolumeParameter)[0];
        
        _activeAudioSources.Add(audioSource);
        
        audioSource.Play();
    }

    private void ApplyMusicVolume()
    {
        float volume = _saveData.MusicEnabled ? _config.MaxVolume : _config.MinVolume;
        _config.AudioMixer.SetFloat(_config.MusicVolumeParameter, volume);
    }

    private void ApplySfxVolume()
    {
        float volume = _saveData.SfxEnabled ? _config.MaxVolume : _config.MinVolume;
        _config.AudioMixer.SetFloat(_config.SfxVolumeParameter, volume);
    }

    private void LoadSettings()
    {
        _saveData = _saveSystem.Load(SavingConstants.AudioId, new AudioSaveData());
    }

    private void SaveSettings()
    {
        _saveSystem.Save(SavingConstants.AudioId, _saveData);
    }
}
