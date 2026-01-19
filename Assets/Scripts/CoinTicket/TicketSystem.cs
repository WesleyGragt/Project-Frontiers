using UnityEngine;
using UnityEngine.Events;

public class TicketSystem : MonoBehaviour
{
    [SerializeField] private IntVariable variableToCheck;

    [Space]
    public UnityEvent getTicket;
    public static int tickets;

    public void getTickets()
    {
        getTicket.Invoke();

        Debug.Log($"{tickets} gained");
        variableToCheck.Increase(tickets);
    }
}
