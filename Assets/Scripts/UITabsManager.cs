using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UITabsManager : MonoBehaviour
{
    public RectTransform buttons;
    public RectTransform[] tabs;
    float buttonsStartX;
    bool buttonOpen;
    float[] startingX;
    bool[] open;

    private void Start()
    {
        buttonsStartX = buttons.position.x;

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

        if (opened)
        {
            buttons.DOMoveX(buttonsStartX, 1f);
        }
        else if (!buttonOpen)
        {
            buttons.DOMoveX(buttonsStartX + 300, 1f);
        }

        for (int tabIndex = 0; tabIndex < tabs.Length; tabIndex++)
        {
            if (tabIndex == tab && !opened)
            {
                open[tabIndex] = true;
                tabs[tabIndex].DOMoveX(startingX[tab] + 300, 1f);
            }
            else
            {
                open[tabIndex] = false;
                tabs[tabIndex].DOMoveX(startingX[tab], 1f);
            }
        }
    }
}
