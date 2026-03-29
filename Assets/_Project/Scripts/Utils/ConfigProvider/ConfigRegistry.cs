using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ConfigRegistry", fileName = "ConfigRegistry")]
public class ConfigRegistry : ScriptableObject
{
    [SerializeField] private List<ScriptableObject> _configs = new();

    public IEnumerable<ScriptableObject> Configs => _configs;
}