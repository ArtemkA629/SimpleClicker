using System.Numerics;
using TMPro;
using UnityEngine;

public class GemsView : MonoBehaviour
{
    [SerializeField] private GameObject _gemsField;
    [SerializeField] private TextMeshProUGUI _gemsText;
    
    public void DisplayGems(int amount)
    {
        _gemsText.text = amount.ToShortValue();
    }

    public void ShowGemsActive(bool active)
    {
        _gemsField.SetActive(active);
    }
}
