using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrdersManager : MonoBehaviour
{
    public SendOrder sendOrder;
    public CurrentOrders currentOrders;

    public Transform ordersParent;
    public Transform ordersSlidingParent;
    public GameObject orderInfo;

    public Scrollbar scrollbar;

    float slidingParentStartY;

    private void Start()
    {
        Refresh();
        slidingParentStartY = ordersSlidingParent.transform.position.y;
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
    }

    private void Update()
    {
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

    public void updateSlide()
    {
        float maxY = 320 * ordersParent.childCount - 286;
        ordersParent.transform.position = new Vector3(ordersSlidingParent.position.x, slidingParentStartY + scrollbar.value * maxY, ordersSlidingParent.position.z);
    }
}
