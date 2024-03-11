using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UITabsManager : MonoBehaviour
{
    public RectTransform[] tabs;
    float[] startingX;
    bool[] open;

    private void Start()
    {
        startingX = new float[tabs.Length];
        open = new bool[tabs.Length];

        for (int tab = 0; tab < tabs.Length; tab++)
        {
            startingX[tab] = tabs[tab].position.x;
            open[tab] = false;
        }
    }

    public void openTab(int tab)
    {
        bool opened = open[tab];

        for (int tabIndex = 0; tabIndex < tabs.Length; tabIndex++)
        {
            if (tabIndex == tab && !opened)
            {
                Debug.Log("OPEN");
                open[tabIndex] = true;
                tabs[tabIndex].DOMoveX(startingX[tab] + 300, 1);
            }
            else
            {
                open[tabIndex] = false;
                tabs[tabIndex].DOMoveX(startingX[tab] + 0, 1f);
            }
        }
    }
}
