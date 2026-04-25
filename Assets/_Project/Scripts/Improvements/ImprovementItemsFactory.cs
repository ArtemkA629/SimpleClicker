using UnityEngine;

public class ImprovementItemsFactory
{
    private readonly Transform _itemsParent;
    private readonly ImprovementsConfig _config;

    private ImprovementConfigInfo _currentImprovementInfo;
    private ImprovementSaveData _saveData;
    private bool _canBuyImprovement;
    
    public ImprovementItemsFactory(Transform itemsParent, IConfigProvider configProvider)
    {
        _itemsParent = itemsParent;
        _config = configProvider.Get<ImprovementsConfig>();
    }
    
    public void SetInfo(ImprovementConfigInfo currentImprovementInfo, ImprovementSaveData saveData, bool canBuyImprovement)
    {
        _currentImprovementInfo = currentImprovementInfo;
        _saveData = saveData;
        _canBuyImprovement = canBuyImprovement;
    }
    
    public ImprovementItem CreateItem()
    {
        ImprovementItem item = Object.Instantiate(_config.ItemPrefab, _itemsParent);
        ImprovementTypeConfig typeConfig = _currentImprovementInfo.TypeConfig;
        IImprovementLevelInfoConfig levelInfoConfig = (IImprovementLevelInfoConfig)_currentImprovementInfo.LevelInfoConfig;
        IImprovementDescriptionCreator descriptionCreator = typeConfig.Type.GetDescriptionCreator();
        int nextLevelImprovement = _saveData.Level == levelInfoConfig.LevelsInfo.Length ? _saveData.Level : _saveData.Level + 1;
        string description = descriptionCreator.GetDescription(typeConfig.DescriptionTemplate, levelInfoConfig.GetLevelInfo(nextLevelImprovement));
        
        item.SetInfo(
            typeConfig.Icon, 
            typeConfig.Name, 
            description, 
            _saveData.Level, 
            levelInfoConfig.GetPriceByLevel(nextLevelImprovement));
        
        if (levelInfoConfig.LevelsInfo.Length == _saveData.Level)
        {
            item.UpdateMaxLevelReachedState();
        }
        else
        {
            item.UpdateCanBuyState(_canBuyImprovement);
        }
        
        return item;
    }
}
