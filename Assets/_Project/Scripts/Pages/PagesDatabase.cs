using System.Collections.Generic;
using UnityEngine;

public class PagesDatabase
{
    [SerializeField] private List<string> _unlockedPages = new();
    
    public List<string> UnlockedPages => _unlockedPages;
    
    public void Add(string id) => _unlockedPages.Add(id);
    public bool Contains(string id) => _unlockedPages.Contains(id);
}