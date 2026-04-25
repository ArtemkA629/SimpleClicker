using System.Linq;

public class PowerClickInfoHandler
{
    private readonly ImprovementsModel _improvementsModel;
    private readonly ImprovementConfigInfo _powerClickInfo;
    
    public PowerClickInfoHandler(ImprovementsModel improvementsModel, IConfigProvider configProvider)
    {
        _improvementsModel = improvementsModel;
        var powerClickConfig = configProvider.Get<ImprovementsConfig>();
        _powerClickInfo = powerClickConfig.ImprovementsInfo
            .First(i => i.TypeConfig.Type == ImprovementType.PowerClick);
    }
    
    public int GetPowerClickMultiplier()
    {
        int currentLevel = _improvementsModel.GetImprovementData(_powerClickInfo.TypeConfig.Name).Level;
        var levelInfoConfig = (IImprovementLevelInfoConfig)_powerClickInfo.LevelInfoConfig;
        PowerClickLevelInfo levelInfo = (PowerClickLevelInfo)levelInfoConfig.GetLevelInfo(currentLevel);
        return levelInfo == null ? 1 : levelInfo.PowerClickMultiplier;
    }
}