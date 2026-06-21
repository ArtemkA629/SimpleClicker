using UnityEngine;

[RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
public class Coin : MonoBehaviour
{
    [field: SerializeField] public RectTransform RectTransform;
    [field: SerializeField] public CanvasGroup CanvasGroup;
}