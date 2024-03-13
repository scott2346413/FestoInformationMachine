using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersManager : MonoBehaviour
{
    public SendOrder sendOrder;

    public void SendOrder()
    {
        sendOrder.partNumber = "3003";
        sendOrder.qty = "1";
        sendOrder.SendOrderToFactory();
        Debug.Log(sendOrder.newOrderMessage);
    }
}
