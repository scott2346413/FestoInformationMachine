using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RuntimeInspectorNamespace;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.UI;
using System.ComponentModel;

public class InformationHandler : MonoBehaviour
{
    public Transform machineTransformParent;
    public Transform[] machineTransforms;
    public GameObject component;
    public MachineInformation[] machineInformation;

    Dictionary<string, Transform> components = new Dictionary<string, Transform>();

    public Animator Robot;
    public RobotArmInformation armInformation;

    public Image IconImage;

    List<RFIDInData> RFIDInDatas = new List<RFIDInData>();
    List<EmgStopData> emgStopDatas = new List<EmgStopData>();
    List<bool> roboBusyDatas = new List<bool>();

    public void updateInformation(int interfaceToRead, string node, object data)
    {
        switch (node)
        {
            case "RFIDIn":
                RFIDIn(interfaceToRead, data);
                break;

            case "EmgStop":
                EmergencyStop(interfaceToRead, data);
                break;

            case "RobotBusy":
                RobotBusy(data);
                break;

            case "Icon":
                Icon(data);
                break;
        }
    }

    void RFIDIn(int interfaceToRead, object data)
    {
        RFIDInData currentRFIDIn = new RFIDInData();
        Transform target = machineTransforms[interfaceToRead - 1];
        bool newComponent = !components.ContainsKey(data.ToString());

        if (newComponent)
        {
            components.Add(data.ToString(), null);
        }

        currentRFIDIn.Create(data, newComponent, target, interfaceToRead);
        RFIDInDatas.Add(currentRFIDIn);
    }

    void EmergencyStop(int interfaceToRead, object data)
    {
        EmgStopData emgStopData = new EmgStopData();
        emgStopData.pressed = !Convert.ToBoolean(data);
        emgStopData.interfaceToRead = interfaceToRead;
        emgStopDatas.Add(emgStopData);
    }

    void RobotBusy(object data)
    {
        Debug.Log("hello");
        Debug.Log("IActStep " + Convert.ToInt16(data));
        Int16Converter converter = new Int16Converter();
        bool busy = Convert.ToInt16(data) == 130;
        roboBusyDatas.Add(busy);
    }

    void Icon(object data)
    {
        IconImage.sprite = data.ConvertTo<Sprite>();
    }

    private void Update()
    {
        if (RFIDInDatas.Count > 0)
        {

            RFIDInData RFIDInData = RFIDInDatas[0];
            object data = RFIDInData.data;

            machineInformation[RFIDInData.interfaceToRead - 1].PanelPassed(data.ToString());

            string componentKey = data.ToString();
            Transform target = RFIDInData.target;

            Transform componentTransform;

            if (RFIDInData.newComponent)
            {
                componentTransform = Instantiate(component, machineTransformParent).transform;
                components[componentKey] = componentTransform;
                componentTransform.position = target.position;
                componentTransform.rotation = target.rotation;
            }
            else
            {
                componentTransform = components[componentKey];
            }

            ComponentManager comManager = componentTransform.GetComponent<ComponentManager>();
            comManager.updateTransform(target);
            comManager.updateText(componentKey);

            RFIDInDatas.RemoveAt(0);
        }

        if (emgStopDatas.Count > 0)
        {
            EmgStopData emgStopData = emgStopDatas[0];
            machineInformation[emgStopData.interfaceToRead - 1].EmgStopPressed(emgStopData.pressed);
            emgStopDatas.RemoveAt(0);
        }

        if (roboBusyDatas.Count > 0)
        {
            Debug.Log("receiving robot busy data");

            bool robotBusy = roboBusyDatas.ElementAt(0);

            Debug.Log(robotBusy);

            if (!Robot.IsNull())
            {
                Robot.SetBool("Busy", robotBusy);
            }

            armInformation.UpdateRobotBusy(robotBusy);

            roboBusyDatas.RemoveAt(0);
        }
    }
}
