using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameEntryPoint : IStartable
{
    [Inject] private Clicker _clicker;
    [Inject] private SaveSystem _saveSystem;

    public void Start()
    {
        _clicker.Init();
        _saveSystem.Init();
    }
}
