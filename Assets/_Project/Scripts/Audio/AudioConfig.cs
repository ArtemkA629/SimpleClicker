using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "ScriptableObject/AudioConfig", fileName = "AudioConfig")]
public class AudioConfig : ScriptableObject
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private string _musicVolumeParameter = "Music";
    [SerializeField] private string _sfxVolumeParameter = "Sounds";
    [SerializeField] private float _minVolume = -80f;
    [SerializeField] private float _maxVolume = 0f;

    public AudioMixer AudioMixer => _audioMixer;
    public string MusicVolumeParameter => _musicVolumeParameter;
    public string SfxVolumeParameter => _sfxVolumeParameter;
    public float MinVolume => _minVolume;
    public float MaxVolume => _maxVolume;
}
