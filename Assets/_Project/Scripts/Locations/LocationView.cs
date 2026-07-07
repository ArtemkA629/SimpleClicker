using UnityEngine;
using UnityEngine.UI;

public class LocationView : MonoBehaviour
{
    [SerializeField] private Image _background;
    
    public void SetLocation(Sprite location)
    {
        _background.sprite = location;
    }
}