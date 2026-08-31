using System;
using UnityEngine;

[Serializable]
public class TutorialSaveData
{
    [SerializeField] private TutorialStep _step;
    [SerializeField] private bool _firstGoldenCookieSpawned;

    public TutorialStep Step => _step;
    public bool FirstGoldenCookieSpawned => _firstGoldenCookieSpawned;

    public void SetStep(TutorialStep step)
    {
        _step = step;
    }

    public void MarkFirstGoldenCookieSpawned()
    {
        _firstGoldenCookieSpawned = true;
    }
}
