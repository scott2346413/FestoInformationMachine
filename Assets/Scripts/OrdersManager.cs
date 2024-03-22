using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrdersManager : MonoBehaviour
{
    public SendOrders sendOrder;
    public CurrentOrders currentOrders;

    public Transform ordersParent;
    public GameObject orderInfo;

    public TextMeshProUGUI orderConfirmation;

    public Toggle autoRefresh;
    public TMP_Dropdown ordersDropdown;
    public Button RefreshButton;
    public string[] partIDs;

    private void Start()
    {
        Refresh();
    }

    private void FixedUpdate()
    {
        RefreshButton.interactable = !autoRefresh.isOn;

        if (autoRefresh.isOn)
        {
            Refresh();
        }
    }

    public void SendOrder()
    {
        sendOrder.partNumber = partIDs[ordersDropdown.value];
        sendOrder.qty = "1";
        sendOrder.SendOrderToFactory();
        orderConfirmation.text = sendOrder.newOrderMessage + "\n ------- \n Order should appear on Refresh shortly";
        Refresh();
        Debug.Log(sendOrder.newOrderMessage);
    }

    public void Refresh()
    {
        currentOrders.GetCurrentOrders();

        foreach (Transform t in ordersParent.GetComponentsInChildren<Transform>())
        {
            if (t != ordersParent)
            {
                Destroy(t.gameObject);
            }
        }

        foreach (string orderInfoText in currentOrders.CurrentOrderData)
        {
            Instantiate(orderInfo, ordersParent).GetComponentInChildren<TextMeshProUGUI>().text = orderInfoText;
        }
    }
}
