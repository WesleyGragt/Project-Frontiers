using UnityEngine;

[CreateAssetMenu(fileName = "NewIntVariable", menuName = "Variables/IntVariable")]
public class IntVariable : ScriptableObject
{
    [Header("Value")]
    public int value = 0;

    // Set the variable to a new value
    public void SetValue(int newValue)
    {
        value = newValue;
        Debug.Log($"IntVariable set to {value}");
    }

    // Increase the variable by amount
    public void Increase(int amount = 1)
    {
        value += amount;
        Debug.Log($"IntVariable increased to {value}");
    }

    // Decrease the variable by amount
    public void Decrease(int amount = 1)
    {
        value -= amount;
        Debug.Log($"IntVariable decreased to {value}");
    }
}
