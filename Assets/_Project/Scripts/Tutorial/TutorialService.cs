using System;
using UnityEngine;
using Object = UnityEngine.Object;

public enum TutorialStep
{
    Step1,
    Completed
}

public class TutorialService
{
    private readonly ISaveSystem _saveSystem;
    private readonly TutorialSaveData _saveData;
    private readonly GameObject _fingerPrefab;
    private readonly PagesStateHandler _pagesStateHandler;
    private readonly PagesConfig _pagesConfig;
    private readonly PagesPresenter _pagesPresenter;
    private readonly TutorialView _tutorialView;
    private readonly TemporaryBoostSpawner _temporaryBoostSpawner;
    private readonly TemporaryBoostAnimator _temporaryBoostAnimator;
    private readonly TemporaryBoostController _temporaryBoostController;
    private readonly TemporaryBoostEventsHandler _temporaryBoostEventsHandler;
    
    private TemporaryBoost _currentTutorialBoost;
    private GameObject _currentTutorialFinger;
    
    public event Action TutorialCompleted;
    public event Action<TutorialStep> StepChanged;

    public TutorialService(ISaveSystem saveSystem, GameObject fingerPrefab, 
        PagesStateHandler pagesStateHandler, IConfigProvider configProvider, PagesPresenter pagesPresenter,
        TutorialView tutorialView, TemporaryBoostSpawner temporaryBoostSpawner, TemporaryBoostAnimator temporaryBoostAnimator,
        TemporaryBoostController temporaryBoostController, TemporaryBoostEventsHandler temporaryBoostEventsHandler)
    {
        _saveSystem = saveSystem;
        _fingerPrefab = fingerPrefab;
        _pagesStateHandler = pagesStateHandler;
        _pagesConfig = configProvider.Get<PagesConfig>();
        _pagesPresenter = pagesPresenter;
        _tutorialView = tutorialView;
        _temporaryBoostSpawner = temporaryBoostSpawner;
        _temporaryBoostAnimator = temporaryBoostAnimator;
        _temporaryBoostController = temporaryBoostController;
        _temporaryBoostEventsHandler = temporaryBoostEventsHandler;
        _saveData = _saveSystem.Load(SavingConstants.TutorialId, new TutorialSaveData());
    }

    public void Initialize()
    {
        if (_saveData.Step == TutorialStep.Completed)
        {
            TutorialCompleted?.Invoke();
        }
        else
        {
            ShowTutorialStep();
        }
        
        _pagesStateHandler.OnPageUnlocked += OnPageUnlocked;
        _temporaryBoostSpawner.OnBoostSpawned += OnBoostSpawned;
        _tutorialView.PanelClicked += TutorialPanelClicked;
    }
    
    public void AdvanceToNextStep()
    {
        if (_saveData.Step < TutorialStep.Completed)
        {
            TutorialStep nextStep = (TutorialStep)((int)_saveData.Step + 1);
            _saveData.SetStep(nextStep);
            SaveSettings();
            ShowTutorialStep();
        }
    }
    
    private void ShowTutorialStep()
    {
        StepChanged?.Invoke(_saveData.Step);
        
        if (_saveData.Step == TutorialStep.Completed)
        {
            TutorialCompleted?.Invoke();
        }
    }

    private void SaveSettings()
    {
        _saveSystem.Save(SavingConstants.TutorialId, _saveData);
    }
    
    public GameObject SpawnFinger(Transform targetTransform)
    {
        if (_fingerPrefab == null)
        {
            Debug.LogError("TutorialFinger prefab is not assigned!");
            return null;
        }
        
        GameObject finger = Object.Instantiate(_fingerPrefab, targetTransform);
        finger.transform.localPosition = Vector3.zero;
        return finger;
    }
    
    public void MarkFirstGoldenCookieSpawned()
    {
        _saveData.MarkFirstGoldenCookieSpawned();
        SaveSettings();
    }
    
    private void OnPageUnlocked(string pageId)
    {
        for (int i = 0; i < _pagesConfig.PagesInfo.Length; i++)
        {
            if (_pagesConfig.PagesInfo[i].Description == pageId)
            {
                int pageNumber = _pagesConfig.PagesInfo[i].Number;
                _pagesPresenter.SelectPage(pageNumber);
                _tutorialView.MoveToPage(pageId);
                
                if (string.IsNullOrEmpty(_pagesConfig.PagesInfo[i].TutorialDescription) == false)
                {
                    _tutorialView.Show(_pagesConfig.PagesInfo[i].TutorialDescription);
                }
                
                break;
            }
        }
    }
    
    private void OnBoostSpawned(TemporaryBoost boost)
    {
        if (_saveData.FirstGoldenCookieSpawned)
            return;

        MarkFirstGoldenCookieSpawned();
        HandleFirstBoostSpawned(boost);
    }
    
    private void HandleFirstBoostSpawned(TemporaryBoost boost)
    {
        _currentTutorialBoost = boost;
        
        _temporaryBoostSpawner.ReleaseBoostFromSpawnerControl(boost);
        _temporaryBoostSpawner.BlockSpawning();
        
        int firstPageNumber = _pagesConfig.PageAtStartNumber;
        _pagesPresenter.SelectPage(firstPageNumber);
        
        _temporaryBoostAnimator.StopAnimations(boost);
        _temporaryBoostAnimator.CancelScheduledDisappearAnimation();
        boost.PauseTimer();
        
        _tutorialView.SetHidePanelActive(true);
        _currentTutorialFinger = SpawnFinger(boost.transform);
        _tutorialView.Show("Click golden cookie to get your boost!");
        
        boost.Clicked += OnTutorialBoostClicked;
    }
    
    private void OnTutorialBoostClicked(TemporaryBoost boost)
    {
        if (_currentTutorialFinger != null)
        {
            Object.Destroy(_currentTutorialFinger);
            _currentTutorialFinger = null;
        }
        
        if (_currentTutorialBoost != null)
        {
            _currentTutorialBoost.Clicked -= OnTutorialBoostClicked;
            _currentTutorialBoost = null;
        }
        
        _tutorialView.Hide();
        _tutorialView.SetHidePanelActive(false);
        
        _temporaryBoostController.CollectBoost();
        _temporaryBoostEventsHandler.DestroyBoost(boost);
        _temporaryBoostSpawner.UnblockSpawning();
    }
    
    private void TutorialPanelClicked()
    {
        _tutorialView.Hide();
        _tutorialView.SetHidePanelActive(false);
        _temporaryBoostSpawner.UnblockSpawning();
    }
}
