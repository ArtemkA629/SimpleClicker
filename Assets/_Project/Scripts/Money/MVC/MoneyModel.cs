using UnityEngine;

public class MoneyModel
{
    private int _amount;
    
    public MoneyModel(ISaveSystem saveSystem)
    {
        _amount = saveSystem.Load<int>(SavingConstants.MoneyId);
    }

    public int Amount => _amount;
    
    public void AddMoney(int addingAmount)
    {
        if (addingAmount < 0f)
        {
            Debug.LogError("Can't add less than 0 money");
            return;
        }
        
        _amount += addingAmount;
    }

    public bool TrySubtractMoney(int subtractingAmount)
    {
        if (subtractingAmount < 0)
        {
            Debug.Log("Can't subtract less than 0 money");
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