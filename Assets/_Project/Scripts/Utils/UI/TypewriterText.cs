using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textComponent;
    [SerializeField] private float _typingSpeed = 0.05f;
    [SerializeField] private float _punctuationDelay = 0.15f;
    
    private Coroutine _typingCoroutine;
    private string _fullText;
    private bool _isTyping;
    private bool _skipRequested;
    
    public bool IsTyping => _isTyping;

    public void TypeText(string text)
    {
        if (_typingCoroutine != null)
        {
            StopCoroutine(_typingCoroutine);
        }
        
        _fullText = text;
        _skipRequested = false;
        _typingCoroutine = StartCoroutine(TypeCoroutine());
    }

    public void SkipTyping()
    {
        if (_isTyping)
        {
            _skipRequested = true;
        }
    }

    public void ClearText()
    {
        if (_typingCoroutine != null)
        {
            StopCoroutine(_typingCoroutine);
            _typingCoroutine = null;
        }
        
        _textComponent.text = string.Empty;
        _isTyping = false;
    }

    private IEnumerator TypeCoroutine()
    {
        _isTyping = true;
        
        _textComponent.text = string.Empty;
        
        for (int i = 0; i < _fullText.Length; i++)
        {
            if (_skipRequested)
            {
                _textComponent.text = _fullText;
                break;
            }
            
            char character = _fullText[i];
            _textComponent.text += character;
            
            float delay = _typingSpeed;
            
            if (IsPunctuation(character))
            {
                delay = _punctuationDelay;
            }
            
            yield return new WaitForSeconds(delay);
        }
        
        _isTyping = false;
        _typingCoroutine = null;
    }

    private bool IsPunctuation(char character)
    {
        return character == '.' || character == ',' || character == '!' || character == '?' || character == ';' || character == ':';
    }
}
