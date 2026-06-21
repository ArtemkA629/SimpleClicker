using System.Numerics;
using UnityEngine;

public class MoneyModel
{
    private BigInteger _amount;
    
    public MoneyModel(ISaveSystem saveSystem)
    {
        _amount = BigIntegerStatic.Parse(saveSystem.Load<string>(SavingConstants.MoneyId));
    }

    public BigInteger Amount => _amount;
    
    public void AddMoney(BigInteger addingAmount)
    {
        if (addingAmount < 0)
        {
            Debug.LogError("Can't add less than 0 money");
            return;
        }
        
        _amount += addingAmount;
    }

    public bool TrySubtractMoney(BigInteger subtractingAmount)
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