using UnityEngine;
using TMPro;
using Zenject;

public class LocalizationText : MonoBehaviour
{
    [SerializeField] private string _id;

    private TextMeshPro _text;
    private TextMeshProUGUI _textUI;
    private ILocalizationService _localizationService;

    private void Awake()
    {
        _text = GetComponent<TextMeshPro>();
        _textUI = GetComponent<TextMeshProUGUI>();
    }

    [Inject]
    private void Construct(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    
    public void UpdateText()
    {
        if (string.IsNullOrEmpty(_id)) 
            return;

        var text = _localizationService.GetText(_id);

        if (_text != null)
        {
            _text.text = text;
        }
        
        if (_textUI != null)
        {
            Debug.Log(text);
            _textUI.text = text;
        }
    }
}