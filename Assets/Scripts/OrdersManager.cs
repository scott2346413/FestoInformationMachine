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

    float slidingParentStartY;

    private void Start()
    {
        Refresh();
    }

    public void SendOrder()
    {
        sendOrder.partNumber = "3003";
        sendOrder.qty = "1";
        sendOrder.SendOrderToFactory();
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
