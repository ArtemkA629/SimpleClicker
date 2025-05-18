using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CustomButton : MonoBehaviour
{
    public virtual Action Clicked { get; }

    private Button _button;

    protected void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    protected virtual void OnClick()
    {
        Clicked?.Invoke();
    }
}

public class CustomButton<T> : CustomButton
{
    public new Action<T> Clicked;

    [SerializeField] protected T _argument;

    protected override void OnClick()
    {
        Clicked?.Invoke(_argument);
    }
}

public class CustomButton<T, K> : CustomButton
{
    public new Action<T, K> Clicked;

    [SerializeField] protected T _argument1;
    [SerializeField] protected K _argument2;

    protected override void OnClick()
    {
        Clicked?.Invoke(_argument1, _argument2);
    }
}
