using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RuntimeInspectorNamespace;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class InformationHandler : MonoBehaviour
{
    public Transform machineTransformParent;
    public Transform[] machineTransforms;
    public GameObject component;
    public MachineInformation[] machineInformation;

    Dictionary<string, Transform> components = new Dictionary<string, Transform>();

    List<RFIDInData> RFIDInDatas = new List<RFIDInData>();
    List<EmgStopData> emgStopDatas = new List<EmgStopData>();

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
        }
    }

    void RFIDIn(int interfaceToRead, object data)
    {
        RFIDInData currentRFIDIn = new RFIDInData();
        Transform target = machineTransforms[interfaceToRead - 1];
        bool newComponent = !components.ContainsKey(data.ToString());

        if(newComponent)
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

    private void Update()
    {
        if(RFIDInDatas.Count > 0)
        {

            RFIDInData RFIDInData = RFIDInDatas[0];
            object data = RFIDInData.data;

            machineInformation[RFIDInData.interfaceToRead-1].PanelPassed(data.ToString());

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

        if(emgStopDatas.Count > 0)
        {
            EmgStopData emgStopData = emgStopDatas[0];
            machineInformation[emgStopData.interfaceToRead - 1].EmgStopPressed(emgStopData.pressed);
            emgStopDatas.RemoveAt(0);
        }
    }
}
