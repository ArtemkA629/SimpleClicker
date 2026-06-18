using System.Numerics;
using TMPro;
using UnityEngine;

public class PassiveIncomeView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalIncomeText;

    public void DisplayTotalIncome(BigInteger income)
    {
        _totalIncomeText.text = $"Total income: {income.ToString()}";
    }
}