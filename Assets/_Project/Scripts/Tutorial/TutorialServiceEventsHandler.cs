using System;

public class TutorialServiceEventsHandler : IDisposable
{
    private readonly TutorialService _tutorialService;
    private readonly DailyRewardView _dailyRewardView;
    
    public TutorialServiceEventsHandler(TutorialService tutorialService, DailyRewardView dailyRewardView)
    {
        _tutorialService = tutorialService;
        _dailyRewardView = dailyRewardView;
    }

    public void Initialize()
    {
        _tutorialService.TutorialCompleted += TutorialCompleted;
    }

    public void Dispose()
    {
        _tutorialService.TutorialCompleted -= TutorialCompleted;
    }
    
    private void TutorialCompleted()
    {
        _dailyRewardView.ShowClaimPopup();
    }
}