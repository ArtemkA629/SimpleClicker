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
            Debug.LogError("Can't add more than 0 money");
            return;
        }
        
        _amount += addingAmount;
    }
}