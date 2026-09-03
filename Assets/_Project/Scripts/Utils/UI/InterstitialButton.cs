using UnityEngine;
using UnityEngine.UI;
using YG;

public class InterstitialButton : MonoBehaviour
{
    private Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }
    
    private void OnButtonClick()
    {
        YG2.InterstitialAdvShow();
    }
}