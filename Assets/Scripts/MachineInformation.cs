using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MachineInformation : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI recentPanelText;
    public TextMeshProUGUI panelsDoneText;
    public Image emgStopSprite;

    public Color emgStopPressed;
    public Color emgStopReleased;


    string recentPanel = "#";
    int panelsDone = 0;

    private void Awake()
    {
        EmgStopPressed(false);
    }

    public void PanelPassed(string panelName)
    {
        recentPanel = panelName;
        recentPanelText.text = recentPanel;

        panelsDone++;
        panelsDoneText.text = panelsDone.ToString();
    }

    public void EmgStopPressed(bool pressed)
    {
        if(pressed)
        {
            emgStopSprite.color = emgStopPressed;
        }
        else
        {
            emgStopSprite.color = emgStopReleased;
        }
    }
}
