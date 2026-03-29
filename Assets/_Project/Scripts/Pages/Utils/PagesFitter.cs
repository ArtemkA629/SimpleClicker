using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class PagesFitter : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;

    private void OnValidate()
    {
        SetupPages();
    }
    
    public void Initialize()
    {
        SetupPages();
    }
    
    private void SetupPages()
    {
        RectTransform content = _scrollRect.content;
        RectTransform viewport = _scrollRect.viewport;

        float width = viewport.rect.width;
        float height = viewport.rect.height;

        for (int i = 0; i < content.childCount; i++)
        {
            RectTransform page = content.GetChild(i) as RectTransform;
            page.sizeDelta = new Vector2(width, height);
        }
        
        content.sizeDelta = new Vector2(width * content.childCount, height);
    }
}