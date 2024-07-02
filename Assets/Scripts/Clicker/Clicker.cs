using UnityEngine;
using VContainer;

public class Clicker : MonoBehaviour
{
    [Inject] private ClickerView _view;

    private ClickerPresenter _presenter;
    private ClickerModel _model;

    public void Init()
    {
        _model = new ClickerModel(_view);
        _presenter = new(_model);
        _view.Init(_presenter);
    }

    public void OnLoad(SaveData saveData)
    {
        _presenter.OnLoad(saveData);
    }

    public SaveData GetSaveData()
    {
        return _model.GetSaveData();
    }
}
