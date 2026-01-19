using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinAmountText;
    [SerializeField] TextMeshProUGUI ticketAmountText;
    [Space]
    [SerializeField] IntVariable coins;
    [SerializeField] IntVariable tickets;

    private string coinText;
    private string ticketText;

    private void Start()
    {
        coinText = coinAmountText.text;
        ticketText = ticketAmountText.text;
    }

    private void Update()
    {
        coinAmountText.text = coinText + " " + coins.value;
        ticketAmountText.text = ticketText + " " + tickets.value;
    }
}
