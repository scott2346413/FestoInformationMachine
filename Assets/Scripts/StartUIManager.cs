using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUIManager : MonoBehaviour
{
    public GameObject AR;
    public GameObject DT;
    public GameObject UI;

    private void Awake()
    {
        AR.SetActive(false);
        DT.SetActive(false);
    }

    public void enableAR(bool enableAR)
    {
        AR.SetActive(enableAR);
        DT.SetActive(!enableAR);
        UI.SetActive(false);
    }
}
