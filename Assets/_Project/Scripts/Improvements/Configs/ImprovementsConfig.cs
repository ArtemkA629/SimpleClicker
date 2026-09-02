using System;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "ScriptableObject/Improvement/ImprovementsConfig", fileName = "ImprovementsConfig")]
public class ImprovementsConfig : ScriptableObject
{
    [field: SerializeField] public ImprovementConfigInfo[] ImprovementsInfo { get; private set; }
    [field: SerializeField] public ImprovementItem ItemPrefab { get; private set; }

    public BigInteger FirstImprovementPrice => FirstImprovementInfo.GetPriceByLevel(1);
    
    private IImprovementLevelInfoConfig FirstImprovementInfo => (IImprovementLevelInfoConfig)ImprovementsInfo[0].LevelInfoConfig;

    public ImprovementConfigInfo GetInfoByName(string id)
    {
        return ImprovementsInfo.First(x => x.TypeConfig.Id == id);
    }
}

[Serializable]
public class ImprovementConfigInfo
{
    [field: SerializeField] public ImprovementTypeConfig TypeConfig { get; private set; }
    [field: SerializeField] public ScriptableObject LevelInfoConfig { get; private set; }

    private ILocalizationService _localizationService;
    
    [Inject]
    private void Construct(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    
    public string GetDescription(int level)
    {
        string localizationTemplate = _localizationService.GetText(TypeConfig.DescriptionTemplateId);
        IImprovementDescriptionCreator descriptionCreator = TypeConfig.Type.GetDescriptionCreator();
        return descriptionCreator.GetDescription(localizationTemplate, 
            ((IImprovementLevelInfoConfig)LevelInfoConfig).GetLevelInfo(level));
    }
}