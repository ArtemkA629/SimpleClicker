using UnityEngine;

public class GemsModel
{
    private int _amount;
    
    public GemsModel(ISaveSystem saveSystem)
    {
        _amount = saveSystem.Load<int>(SavingConstants.GemsId);
    }

    public int Amount => _amount;
    
    public void AddGems(int addingAmount)
    {
        if (addingAmount < 0)
        {
            Debug.LogError("Can't add less than 0 gems");
            return;
        }
        
        _amount += addingAmount;
    }

    public bool TrySubtractGems(int subtractingAmount)
    {
        if (subtractingAmount < 0)
        {
            Debug.Log("Can't subtract less than 0 gems");
            return false;
        }

        if (_amount - subtractingAmount < 0)
        {
            return false;
        }
        
        _amount -= subtractingAmount;
        return true;
    }
}
