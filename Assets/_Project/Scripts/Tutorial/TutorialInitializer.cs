public class TutorialInitializer
{
    private readonly TutorialStepHandler _stepHandler;
    private readonly TutorialService _tutorialService;
    private readonly TutorialServiceEventsHandler _eventsHandler;

    public TutorialInitializer(TutorialStepHandler stepHandler, TutorialService tutorialService, 
        TutorialServiceEventsHandler eventsHandler)
    {
        _stepHandler = stepHandler;
        _tutorialService = tutorialService;
        _eventsHandler = eventsHandler;
    }

    public void Initialize()
    {
        _stepHandler.Initialize();
        _tutorialService.Initialize();
        _eventsHandler.Initialize();
    }
}
