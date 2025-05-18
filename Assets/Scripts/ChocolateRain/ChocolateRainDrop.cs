using System;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RectTransform), typeof(Rigidbody))]
public class ChocolateRainDrop : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed = 10f;

    private RectTransform _boundaries;

    public event Action<ChocolateRainDrop> Destroyed;

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
        Destroyed?.Invoke(this);
    }

    public void OnGet()
    {
        _rigidbody.isKinematic = false;
    }

    public void OnRelease()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;
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
