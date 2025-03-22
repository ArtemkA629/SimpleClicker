using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ChocolateRainDrop : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _speed = 10f;

    private RectTransform _boundaries;

    public void Init(Vector3 position, Quaternion rotation, RectTransform boundaries)
    {
        _rectTransform.anchoredPosition = position;
        _rectTransform.rotation = rotation;
        _boundaries = boundaries;

        StartCoroutine(WaitUntilDie());
    }

    public void Update()
    {
        _rectTransform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);
    }

    public IEnumerator WaitUntilDie()
    {
        yield return new WaitUntil(() => IsElementOutOfBoundaries(_rectTransform, _boundaries));
        Destroy(gameObject);
    }

    private bool IsElementOutOfBoundaries(RectTransform element, RectTransform boundaries)
    {
        Vector3[] elementCorners = new Vector3[4];
        Vector3[] boundaryCorners = new Vector3[4];

        element.GetWorldCorners(elementCorners);
        boundaries.GetWorldCorners(boundaryCorners);

        float highestYCorner = Mathf.Max(elementCorners.Select((e) => e.y).ToArray());
        bool isBelowBoundary = highestYCorner < boundaryCorners[0].y; 

        return isBelowBoundary;
    }
}
