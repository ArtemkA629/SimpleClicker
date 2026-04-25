using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class ImprovementsView
{
    private ImprovementsPresenter _presenter;
    
    private Dictionary<ImprovementItem, UnityAction> _improvementItemsDictionary = new();
    
    public void Init(ImprovementsPresenter presenter, List<ImprovementItem> improvementItems)
    {
        _presenter = presenter;
        
        foreach (var improvementItem in improvementItems)
        {
            UnityAction handler = () => OnItemClicked(improvementItem.Name);
            improvementItem.AddListener(handler);
            _improvementItemsDictionary.Add(improvementItem, handler);
        }
    }

    public void DisplayNewImprovementLevel(string improvementName, int level)
    {
        ImprovementItem item = _improvementItemsDictionary.Keys.First(x => x.Name == improvementName);
        int improvementPrice = _presenter.GetImprovementPrice(improvementName, level + 1);
        bool canBuyImprovement = _presenter.CanBuyImprovement(improvementPrice);
        bool isImprovementLevelMax = _presenter.IsLevelMax(improvementName, level);
        item.UpdateLevel(level);
        item.UpdatePrice(improvementPrice);

        if (isImprovementLevelMax)
        {
            item.UpdateMaxLevelReachedState();
        }
        else
        {
            item.UpdateCanBuyState(canBuyImprovement);
        }
    }
    
    private void OnItemClicked(string improvementName)
    {
        _presenter.TryBuyImprovement(improvementName);
    }
}