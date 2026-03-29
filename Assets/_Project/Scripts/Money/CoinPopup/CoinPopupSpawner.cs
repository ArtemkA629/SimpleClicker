using UnityEngine;

public class CoinPopupSpawner
{
    private readonly Canvas _canvas;
    private readonly Coin _coinPrefab;
    
    public CoinPopupSpawner(IConfigProvider configProvider, Canvas canvas)
    {
        _coinPrefab = configProvider.Get<CoinAnimationConfig>().CoinPrefab;
        _canvas = canvas;
    }
    
    public Coin CreateCoin(Vector3 position)
    {
        Vector2 finalPosition;
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.transform as RectTransform,
            position,
            _canvas.worldCamera,
            out finalPosition
        );

        Coin coin = Object.Instantiate(_coinPrefab, _canvas.transform);
        coin.RectTransform.localPosition = finalPosition;
        return coin;
    }
}