using UnityEngine;
using UnityEngine.Events;

public class CoinSystem : MonoBehaviour
{
    [SerializeField] private IntVariable variableToCheck;
    
    [Space]
    public UnityEvent enoughCoins;


    private void CheckAmount(IntVariable pIntVariable, int pGameCost)
    {
        if (variableToCheck != null)
        {
            if (variableToCheck.value - pGameCost >= 0)
            {
                variableToCheck.Decrease(pGameCost);
                enoughCoins?.Invoke();
            } else
            {
                Debug.Log("Not enough coins!");
            }
        }
        else
        {
            Debug.LogWarning("No variable set!");
        }
    }

    public void CheckCost(int pGameCost)
    {
        CheckAmount(variableToCheck, pGameCost);
    }
}
