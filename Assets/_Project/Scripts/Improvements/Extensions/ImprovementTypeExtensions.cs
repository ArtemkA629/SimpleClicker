using UnityEngine;

public static class ImprovementTypeExtensions
{
    public static IImprovementDescriptionCreator GetDescriptionCreator(this ImprovementType improvementType)
    {
        switch (improvementType)
        {
            case ImprovementType.PowerClick:
                return new PowerClickDescriptionCreator();
            case ImprovementType.OfflineIncome:
                return new OfflineIncomeDescriptionCreator();
            default:
                Debug.LogError("Unknown improvement type");
                return null;
        }
    }
}