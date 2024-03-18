using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDTView : MonoBehaviour
{
    public GameObject AR;
    public GameObject DT;

    public void SwitchView()
    {
        AR.SetActive(!AR.activeSelf);
        DT.SetActive(!DT.activeSelf);
    }
}
