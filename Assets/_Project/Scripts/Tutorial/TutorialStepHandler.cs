using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

public class TutorialStepHandler : IDisposable
{
    private readonly TutorialService _tutorialService;
    private readonly TutorialView _tutorialView;
    private readonly ILocalizationService _localizationService;
    
    private GameObject _currentFinger;
    
    public TutorialStepHandler(TutorialService tutorialService, TutorialView tutorialView, 
        ILocalizationService localizationService)
    {
        _tutorialService = tutorialService;
        _tutorialView = tutorialView;
        _localizationService = localizationService;
    }
    
    public void Initialize()
    {
        _tutorialView.Initialize();
        _tutorialService.StepChanged += HandleStepChanged;
        _tutorialView.PanelClicked += PanelClicked;
        _tutorialView.Hide();
    }
    
    public void Dispose()
    {
        _tutorialView.Dispose();
        _tutorialService.StepChanged -= HandleStepChanged;
        _tutorialView.PanelClicked -= PanelClicked;
        _tutorialView.Clickable.Clicked -= OnClickableClicked;
        _tutorialView.ClickableZone.Clicked -= OnClickableClicked;
    }
    
    private void HandleStepChanged(TutorialStep step)
    {
        CleanupCurrentStep();
        
        switch (step)
        {
            case TutorialStep.Step1:
                HandleFirstClick();
                break;
            case TutorialStep.Completed:
                break;
        }
    }
    
    private void HandleFirstClick()
    {
        _currentFinger = _tutorialService.SpawnFinger(_tutorialView.Clickable.transform);
        //_tutorialView.Show("Click this cookie and start earning!");
        _tutorialView.Show(_localizationService.GetText(LocalizationConstants.FirstClickTutorialDescription));
        _tutorialView.Clickable.Clicked += OnClickableClicked;
        _tutorialView.ClickableZone.Clicked += OnClickableClicked;
    }
    
    private void OnClickableClicked(PointerEventData eventData)
    {
        CleanupCurrentStep();
        _tutorialService.AdvanceToNextStep();
    }
    
    private void PanelClicked()
    {
        _tutorialView.Hide();
    }
    
    private void CleanupCurrentStep()
    {
        if (_currentFinger != null)
        {
            Object.Destroy(_currentFinger);
            _currentFinger = null;
        }
        
        _tutorialView.Clickable.Clicked -= OnClickableClicked;
        _tutorialView.ClickableZone.Clicked -= OnClickableClicked;
        
        _tutorialView.Hide();
    }
}
