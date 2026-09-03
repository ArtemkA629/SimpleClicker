public class RewardInitializer
{
    private readonly RewardService _rewardService;

    public RewardInitializer(RewardService rewardService)
    {
        _rewardService = rewardService;
    }

    public void Initialize()
    {
        _rewardService.Initialize();
    }
}
