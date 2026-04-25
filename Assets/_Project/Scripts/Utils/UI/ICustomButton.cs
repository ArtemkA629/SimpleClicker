using UnityEngine.Events;

public interface ICustomButton
{
    void AddListener(UnityAction action);
    void RemoveListener(UnityAction action);
}