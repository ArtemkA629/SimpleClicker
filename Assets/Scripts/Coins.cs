using TMPro;
using UnityEngine;
using VContainer;

public class Coins : MonoBehaviour
{
    [Inject] private Clicker _clicker;

    [SerializeField] private TextMeshProUGUI _coinsText;

    private CoinsModel _model = new();

    public CoinsModel Model => _model;
    public int Amount => _model.Amount;

    public void Init(int coins)
    {
        _model.Changed += OnAmountChanged;
        _clicker.Model.LevelChanged += OnLevelChanged;

        _model.SetAmount(coins);
    }

    private void OnApplicationQuit()
    {
        _model.Changed -= OnAmountChanged;
        _clicker.Model.LevelChanged -= OnLevelChanged;
    }

    public bool CanPay(int price)
    {
        return _model.Amount >= price;
    }

    public void PayUpgrade(int price)
    {
        _model.SetAmount(_model.Amount - price);
    }

    private void OnLevelChanged(int currentNextLevelClicksCount)
    {
        _model.SetAmount(_model.Amount + currentNextLevelClicksCount/2);
        Debug.Log(currentNextLevelClicksCount);
    }

    private void OnAmountChanged()
    {
        _coinsText.text = _model.Amount.ToString();
    }
}
