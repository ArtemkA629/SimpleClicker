using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AudioButton : MonoBehaviour
{
    [SerializeField] private AudioClip _clickSound;

    private Button _button;
    private AudioService _audioService;
    
    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    [Inject]
    private void Construct(AudioService audioService)
    {
        _audioService = audioService;
    }
    
    private void OnButtonClicked()
    {
        _audioService.PlaySound(_clickSound);
    }
}
