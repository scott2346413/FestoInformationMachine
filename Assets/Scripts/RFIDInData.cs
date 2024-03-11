using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFIDInData
{
    public object data;
    public bool newComponent;
    public Transform target;
    public int interfaceToRead;

    public void Create(object _data, bool _newComponent, Transform _target, int _interfaceToRead)
    {
        this.data = _data;
        this.newComponent = _newComponent;
        this.target = _target;
        this.interfaceToRead = _interfaceToRead;
    }
}
