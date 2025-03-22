using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class ChocolateRain : MonoBehaviour
{
    [SerializeField] private ChocolateRainDrop _chocolatePrefab;
    [SerializeField, FormerlySerializedAs("_panel")] private RectTransform _background;
    [SerializeField] private float _boundOffset;
    [SerializeField] private float _rainFrequency;
    [SerializeField] private int _chocolatesAtStart = 30;

    private Vector2 _backgroundSize;

    public void Init()
    {
        _backgroundSize = _background.sizeDelta;

        InitializeRain();
        StartCoroutine(DoRain());
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
        var drop = Instantiate(_chocolatePrefab, _background, false);
        drop.Init(position, rotation, _background);
    }
}
