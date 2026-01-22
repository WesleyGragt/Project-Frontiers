using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CoinSystem : MonoBehaviour
{
    [SerializeField] private IntVariable variableToCheck;
    [SerializeField] private GameObject HideUI;
    
    [Space]
    public UnityEvent enoughCoins;
    public UnityEvent notEnoughCoins;


    private void CheckAmount(IntVariable pIntVariable, int pGameCost)
    {
        if (variableToCheck != null)
        {
            if (variableToCheck.value - pGameCost >= 0)
            {
                if (HideUI != null)
                {
                    HideUI.SetActive(false);
                }

                variableToCheck.Decrease(pGameCost);
                enoughCoins?.Invoke();
            } else
            {
                notEnoughCoins?.Invoke();
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
