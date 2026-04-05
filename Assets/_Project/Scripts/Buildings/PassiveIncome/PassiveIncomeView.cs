using TMPro;
using UnityEngine;

public class PassiveIncomeView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalIncomeText;

    public void DisplayTotalIncome(int income)
    {
        _totalIncomeText.text = $"Total income: {income.ToString()}";
    }
}