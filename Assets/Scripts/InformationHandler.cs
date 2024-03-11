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
    List<(String, Transform)> objectsToSpawn = new List<(String, Transform)>(); // string: component key. transform: target transform
    List<(String, Transform)> objectsToMove = new List<(String, Transform)>(); // string: component key. transform: new transform
    List<RFIDInData> RFIDInDatas = new List<RFIDInData>();

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

        Debug.Log("component: " + data.ToString() + " has passed machine " + interfaceToRead);

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
        string stopped = "stopped";

        if (!Convert.ToBoolean(data))
        {
            stopped = "unstopped";
        }

        Debug.Log("component " + interfaceToRead + " has been " + stopped);
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

            Debug.Log("moving or spawning object: " + data.ToString());
            Debug.Log("new component: " + RFIDInData.newComponent);
            Debug.Log("-----");
        }
    }
}