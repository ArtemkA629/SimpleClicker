using System;
using UnityEngine;

[Serializable]
public class PagePosition
{
    [field: SerializeField] public PageInfo PageInfo { get; private set; }
    [field: SerializeField] public Transform Position { get; private set; }
}

public class TutorialView : MonoBehaviour
{
    [SerializeField] private TutorialPanel _tutorialPanel;
    [SerializeField] private Clickable _clickable;
    [SerializeField] private Clickable _clickableZone;
    [SerializeField] private GameObject _tutorialHidePanel;
    [SerializeField] private PagePosition[] _panelPositions;
    
    public Clickable Clickable => _clickable;
    public Clickable ClickableZone => _clickableZone;
    
    public event Action PanelClicked;
    
    public void Initialize()
    {
        _tutorialPanel.AddListener(OnPanelButtonClicked);
    }
    
    public void Dispose()
    {
        _tutorialPanel.RemoveListener(OnPanelButtonClicked);
    }
    
    public void Show(string message)
    {
        _tutorialPanel.gameObject.SetActive(true);
        _tutorialPanel.TypeText(message);
    }
    
    public void Hide()
    {
        _tutorialPanel.gameObject.SetActive(false);
        _tutorialPanel.ClearText();
    }
    
    private void OnPanelButtonClicked()
    {
        if (_tutorialPanel.IsTextTyping)
        {
            _tutorialPanel.SkipTyping();
        }
        else
        {
            PanelClicked?.Invoke();
        }
    }
    
    public void SetHidePanelActive(bool active)
    {
        _tutorialHidePanel.SetActive(active);
    }
    
    public void MoveToPage(string pageId)
    {
        foreach (var pagePosition in _panelPositions)
        {
            if (pagePosition.PageInfo.Id == pageId)
            {
                _tutorialPanel.transform.SetParent(pagePosition.Position);
                _tutorialPanel.transform.localPosition = Vector3.zero;
                return;
            }
        }
        
        Debug.LogError($"Panel position not found for page ID: {pageId}");
    }
}
