using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour, ICustomButton
{
    [SerializeField] private Button _button;
    [SerializeField] private TypewriterText _typewriterText;

    public bool IsTextTyping => _typewriterText.IsTyping;
    
    public void AddListener(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }

    public void RemoveListener(UnityAction action)
    {
        _button.onClick.RemoveListener(action);
    }
    
    public void TypeText(string text)
    {
        _typewriterText.TypeText(text);
    }

    public void ClearText()
    {
        _typewriterText.ClearText();
    }

    public void SkipTyping()
    {
        _typewriterText.SkipTyping();
    }
}