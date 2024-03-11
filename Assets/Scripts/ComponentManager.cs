using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComponentManager : MonoBehaviour
{
    Transform target;
    
    public TextMeshProUGUI textMesh;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime*0.5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * 0.5f);
        }
    }

    public void updateTransform(Transform newTransform)
    {
        target = newTransform;
    }

    public void updateText(string text)
    {
        textMesh.text = text;
    }
}
