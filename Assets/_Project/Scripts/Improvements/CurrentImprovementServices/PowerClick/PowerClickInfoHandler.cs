using System.Linq;

public class PowerClickInfoHandler
{
    private readonly ImprovementsModel _improvementsModel;
    private readonly TemporaryBoostModel _temporaryBoostModel;
    private readonly ImprovementConfigInfo _powerClickInfo;
    private readonly float _temporaryBoostMultiplier;
    
    public PowerClickInfoHandler(ImprovementsModel improvementsModel, TemporaryBoostModel temporaryBoostModel, 
        IConfigProvider configProvider)
    {
        _improvementsModel = improvementsModel;
        _temporaryBoostModel = temporaryBoostModel;
        var powerClickConfig = configProvider.Get<ImprovementsConfig>();
        _powerClickInfo = powerClickConfig.ImprovementsInfo
            .First(i => i.TypeConfig.Type == ImprovementType.PowerClick);
        var temporaryBoostConfig = configProvider.Get<TemporaryBoostConfig>();
        _temporaryBoostMultiplier = temporaryBoostConfig.BoostMultiplier;
    }
    
    public float GetPowerClickMultiplier()
    {
        int currentLevel = _improvementsModel.GetImprovementData(_powerClickInfo.TypeConfig.Name).Level;
        var levelInfoConfig = (IImprovementLevelInfoConfig)_powerClickInfo.LevelInfoConfig;
        PowerClickLevelInfo levelInfo = (PowerClickLevelInfo)levelInfoConfig.GetLevelInfo(currentLevel);
        float powerClickMultiplier = levelInfo == null ? 1 : levelInfo.PowerClickMultiplier;
        float temporaryBoostMultiplier = _temporaryBoostModel.IsBoostActive ? _temporaryBoostMultiplier : 1f;
        return powerClickMultiplier * temporaryBoostMultiplier;
    }
}