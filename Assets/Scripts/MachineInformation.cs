using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MachineInformation : MonoBehaviour
{
    [Header("Text Meshes")]
    public TextMeshProUGUI recentPanelText;
    public TextMeshProUGUI panelsDoneText;

    string recentPanel = "#";
    int panelsDone = 0;

    public void PanelPassed(string panelName)
    {
        recentPanel = panelName;
        recentPanelText.text = recentPanel;

        panelsDone++;
        panelsDoneText.text = panelsDone.ToString();
    }
}
