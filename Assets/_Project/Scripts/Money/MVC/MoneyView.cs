using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    
    public void DisplayMoney(int amount)
    {
        _moneyText.text = amount.ToString();
    }
}