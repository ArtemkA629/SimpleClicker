using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class TemporaryBoostSpawner : ITickable
{
    private readonly RectTransform[] _spawnPanels;
    private readonly TemporaryBoostController _controller;
    private readonly TemporaryBoostAnimator _animator;
    private readonly TemporaryBoostEventsHandler _eventsHandler;
    private readonly TemporaryBoostConfig _config;
    
    private List<TemporaryBoost> _activeBoosts = new();
    private float _spawnTimer;
    private bool _isSpawnBlocked;
    
    public event Action<TemporaryBoost> OnBoostSpawned;
    
    public TemporaryBoostSpawner(RectTransform[] spawnPanels, TemporaryBoostController controller, 
        TemporaryBoostAnimator animator, TemporaryBoostEventsHandler eventsHandler, IConfigProvider configProvider)
    {
        _spawnPanels = spawnPanels;
        _controller = controller;
        _animator = animator;
        _eventsHandler = eventsHandler;
        _config = configProvider.Get<TemporaryBoostConfig>();
    }
    
    public void Initialize()
    {
        _spawnTimer = _config.SpawnInterval;
    }
    
    public void Tick()
    {
        if (_isSpawnBlocked)
            return;
        
        _spawnTimer -= Time.deltaTime;
        
        if (_spawnTimer <= 0f)
        {
            SpawnBoost();
            _spawnTimer = _config.SpawnInterval;
        }
        
        CleanupDestroyedBoosts();
    }
    
    public void ReleaseBoostFromSpawnerControl(TemporaryBoost boost)
    {
        boost.Clicked -= OnBoostClicked;
        _activeBoosts.Remove(boost);
        _eventsHandler.UnregisterBoost(boost);
    }
    
    public void BlockSpawning()
    {
        _isSpawnBlocked = true;
    }
    
    public void UnblockSpawning()
    {
        _isSpawnBlocked = false;
    }
    
    private void SpawnBoost()
    {
        RectTransform[] panels = _spawnPanels;
        if (panels == null || panels.Length == 0)
            return;
        
        RectTransform randomPanel = panels[Random.Range(0, panels.Length)];
        Vector2 randomPosition = GetRandomPositionInPanel(randomPanel);
        
        TemporaryBoost boost = Object.Instantiate(_config.BoostPrefab, randomPanel);
        boost.RectTransform.anchoredPosition = randomPosition;
        
        boost.Initialize(_controller, _config.BoostLifetime);
        _animator.StartAnimations(boost, _config.BoostLifetime);
        _activeBoosts.Add(boost);
        _eventsHandler.RegisterBoost(boost);
        
        boost.Clicked += OnBoostClicked;
        
        OnBoostSpawned?.Invoke(boost);
    }
    
    private void OnBoostClicked(TemporaryBoost boost)
    {
        _controller.CollectBoost();
        _eventsHandler.DestroyBoost(boost);
        _activeBoosts.Remove(boost);
        boost.Clicked -= OnBoostClicked;
    }
    
    private Vector2 GetRandomPositionInPanel(RectTransform panel)
    {
        Rect panelRect = panel.rect;
        float randomX = Random.Range(-panelRect.width / 2f, panelRect.width / 2f);
        float randomY = Random.Range(-panelRect.height / 2f, panelRect.height / 2f);
        return new Vector2(randomX, randomY);
    }
    
    private void CleanupDestroyedBoosts()
    {
        _activeBoosts.RemoveAll(boost => boost == null);
    }
}
