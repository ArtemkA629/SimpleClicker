using System.Numerics;
using TMPro;
using UnityEngine;
using Zenject;

public class PassiveIncomeView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalIncomeText;

    private ILocalizationService _localizationService;
    
    [Inject]
    private void Construct(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    
    public void DisplayPassiveIncome(BigInteger income)
    {
        _totalIncomeText.text = $"{_localizationService.GetText(LocalizationConstants.PassiveIncome)}: {income.ToString()}";
    }
}