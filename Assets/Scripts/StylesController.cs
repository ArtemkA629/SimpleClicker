using System;
using System.Collections.Generic;
using UnityEngine;

public class StylesController : MonoBehaviour
{
    [SerializeField] private BuyStyleButton[] _buyStyleButtons;
    [SerializeField] private RectTransform _stylesParent;

    private List<int> _boughtStylesNumbers;
    private int _chosenStyle;

    public event Action DataChanged;

    public List<int> BoughtStylesNumbers => _boughtStylesNumbers;
    public int ChosenStyle => _chosenStyle;

    public void Init(List<int> boughtStylesNumbers, int chosenStyle)
    {
        _boughtStylesNumbers = boughtStylesNumbers;
        _chosenStyle = chosenStyle;

        UpdateView();

        foreach (var styleButton in _buyStyleButtons)
        {
            styleButton.Bought += OnStyleBought;
            styleButton.Chosen += OnStyleChosen;
        }
    }

    private void OnDisable()
    {
        foreach (var styleButton in _buyStyleButtons)
        {
            styleButton.Bought -= OnStyleBought;
            styleButton.Chosen -= OnStyleChosen;
        }
    }

    private void UpdateView()
    {
        for (int i = 1; i <= _stylesParent.childCount; i++)
        {
            if (_boughtStylesNumbers.Contains(i))
            {
                _buyStyleButtons[i - 1].SetBoughtState(true, _boughtStylesNumbers);
                if (i == _chosenStyle)
                    _buyStyleButtons[i - 1].ActivateStyle();
                else
                    _buyStyleButtons[i - 1].InactivateStyle();
            }
        }
    }

    private void OnStyleBought(int number)
    {
        _boughtStylesNumbers.Add(number);
        DataChanged?.Invoke();
        YGAdsProvider.TryShowFullScreenAdWithChance();
    }

    private void OnStyleChosen(int number)
    {
        _chosenStyle = number;
        DataChanged?.Invoke();
    }
}
