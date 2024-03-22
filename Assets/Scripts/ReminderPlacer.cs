using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ReminderPlacer : MonoBehaviour
{
    public GameObject reminder;
    public RectTransform[] machines;

    public TMP_InputField textInput;
    public TMP_Dropdown dropdown;

    public GameObject textInputObject;

    Transform currentReminder;

    public void createReminder()
    {
        RectTransform machine = machines[dropdown.value];
        currentReminder = Instantiate(reminder, machine).transform;
        Debug.Log(currentReminder.gameObject == null);
        string message = textInput.text + "\n \n - " + System.DateTime.Now.ToString();

        foreach (TextMeshProUGUI textMesh in currentReminder.GetComponentsInChildren<TextMeshProUGUI>())
        {
            textMesh.text = message;
        }

        textInput.text = "";

        LayoutRebuilder.ForceRebuildLayoutImmediate(machine);

        Debug.Log("create");
    }
}
