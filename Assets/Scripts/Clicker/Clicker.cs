using UnityEngine;
using VContainer;

public class Clicker : MonoBehaviour
{
    [Inject] private ClickerView _view;

    private ClickerPresenter _presenter;
    private ClickerModel _model;

    public ClickerModel Model => _model;

    public void Init(SaveData saveData)
    {
        _model = new ClickerModel(_view);
        _presenter = new(_model);
        _view.Init(_presenter);
        _model.LoadData(saveData);
    }
}
