using UnityEngine;

public static class RectTransformExtensions
{
    public static Vector2 GetTargetAnchoredPos(this RectTransform origin, RectTransform target)
    {
        Vector3 worldPos = target.position;
        RectTransform parent = origin.parent as RectTransform;
        
        if (parent == null) 
            return worldPos;
 
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parent,
            RectTransformUtility.WorldToScreenPoint(null, worldPos),
            null,
            out Vector2 localPos
        );
        
        return localPos;
    }
}