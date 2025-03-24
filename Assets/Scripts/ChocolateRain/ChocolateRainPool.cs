using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ChocolateRainPool : MonoBehaviour
{
    [SerializeField] private ChocolateRainDrop _chocolatePrefab;
    [SerializeField] private RectTransform _background;
    [SerializeField] private float _boundOffset;
    [SerializeField] private float _rainFrequency;
    [SerializeField] private int _chocolatesAtStart = 30;
    [SerializeField] private bool _collectionCheck = false;
    [SerializeField] private int _defaultCapacity = 50;
    [SerializeField] private int _maxSize = 100;

    private Vector2 _backgroundSize;
    private ObjectPool<ChocolateRainDrop> _chocolateDropsPool;

    public void Init()
    {
        _backgroundSize = _background.sizeDelta;
        _chocolateDropsPool = new ObjectPool<ChocolateRainDrop>(
            CreateObject,
            OnGetObject,
            OnReleaseObject,
            OnDestroyObject,
            _collectionCheck,
            _defaultCapacity,
            _maxSize
        );

        InitializeRain();
        StartCoroutine(DoRain());
    }

    private ChocolateRainDrop CreateObject()
    {
        return Instantiate(_chocolatePrefab, _background, false);
    }

    private void OnGetObject(ChocolateRainDrop drop)
    {
        drop.gameObject.SetActive(true);
    }

    private void OnReleaseObject(ChocolateRainDrop drop)
    {
        drop.gameObject.SetActive(false);
    }

    private void OnDestroyObject(ChocolateRainDrop drop)
    {
        Destroy(drop.gameObject);
    }

    private IEnumerator DoRain()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / _rainFrequency);
            var position = new Vector3(Random.Range(-_backgroundSize.x / 2 + _boundOffset, _backgroundSize.x / 2 - _boundOffset), _backgroundSize.y / 2 + _boundOffset);
            var rotation = Quaternion.Euler(0, 0, Random.Range(-180, 180));
            Spawn(position, rotation);
        }
    }

    private void InitializeRain()
    {
        for (int i = 0; i < _chocolatesAtStart; i++)
        {
            var position = new Vector3(Random.Range(-_backgroundSize.x / 2 + _boundOffset, _backgroundSize.x / 2 - _boundOffset), 
                Random.Range(-_backgroundSize.y / 2 + _boundOffset, _backgroundSize.y / 2 - _boundOffset));
            var rotation = Quaternion.Euler(0, 0, Random.Range(-180, 180));
            Spawn(position, rotation);
        }
    }

    private void Spawn(Vector3 position, Quaternion rotation)
    {
        var drop = _chocolateDropsPool.Get();
        drop.Destroyed += OnDropDestroyed;
        drop.Init(position, rotation, _background);
    }

    private void OnDropDestroyed(ChocolateRainDrop drop)
    {
        drop.Destroyed -= OnDropDestroyed;
        _chocolateDropsPool.Release(drop);
    }
}
