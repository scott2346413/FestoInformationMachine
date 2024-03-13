using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFIDInData : MonoBehaviour
{
    public object data;
    public bool newComponent;
    public Transform target;
    public int interfaceToRead;

    public void Create(object _data, bool _newComponent, Transform _target, int _interfaceToRead)
    {
        data = _data;
        newComponent = _newComponent;
        target = _target;
        interfaceToRead = _interfaceToRead;
    }
}
