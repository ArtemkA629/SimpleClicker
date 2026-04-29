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
        int descriptionLevel = isImprovementLevelMax ? level : level + 1;
        string description = _presenter.GetDescription(improvementName, descriptionLevel);
        
        item.UpdateLevel(level);
        item.UpdatePrice(improvementPrice);
        item.UpdateDescription(description);

        if (isImprovementLevelMax)
        {
            item.UpdateMaxLevelReachedState();
        }
        else
        {
            item.UpdateCanBuyState(canBuyImprovement);
        }
    }

    public void UpdateAllItemsView(ImprovementsDatabase database)
    {
        foreach (var item in _improvementItemsDictionary.Keys)
        {
            int level = database.GetData(item.Name).Level;
            bool isImprovementLevelMax = _presenter.IsLevelMax(item.Name, level);
            
            if (isImprovementLevelMax)
                continue;
            
            int price = _presenter.GetImprovementPrice(item.Name, level + 1);
            bool canBuyImprovement = _presenter.CanBuyImprovement(price);
            item.UpdateCanBuyState(canBuyImprovement);
        }
    }
    
    private void OnItemClicked(string improvementName)
    {
        _presenter.TryBuyImprovement(improvementName);
    }
}