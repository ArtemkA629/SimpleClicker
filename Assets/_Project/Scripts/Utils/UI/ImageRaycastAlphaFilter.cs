using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageRaycastAlphaFilter : MonoBehaviour
{
    [Range(0f, 1f), SerializeField] private float _threshold = 0.5f;

    private void Awake()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = _threshold;
    }
}