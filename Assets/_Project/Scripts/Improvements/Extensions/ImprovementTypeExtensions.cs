using UnityEngine;

public static class ImprovementTypeExtensions
{
    public static IImprovementDescriptionCreator GetDescriptionCreator(this ImprovementType improvementType)
    {
        switch (improvementType)
        {
            case ImprovementType.PowerClick:
                return new PowerClickDescriptionCreator();
            default:
                Debug.LogError("Unknown improvement type");
                return null;
        }
    }
}