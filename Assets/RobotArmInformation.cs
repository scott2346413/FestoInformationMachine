using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotArmInformation : MonoBehaviour
{
    public Image robotBusyImage;

    public Color robotBusy;
    public Color robotNotBusy;

    // Start is called before the first frame update
    void Start()
    {
        robotBusyImage.color = robotNotBusy;
    }

    public void UpdateRobotBusy(bool busy)
    {
        if (busy)
        {
            robotBusyImage.color = robotBusy;
        }
        else
        {
            robotBusyImage.color = robotNotBusy;
        }
    }
}
