using System;

public class CoinsModel
{
    private int _amount;

    public event Action Changed;

    public int Amount => _amount;

    public void SetAmount(int amount)
    {
        if (amount < 0)
            throw new Exception("Invalid coinsAmount");

        _amount = amount;
        Changed?.Invoke();
    }
}
