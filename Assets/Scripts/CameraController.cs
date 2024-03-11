using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform[] cameraTransforms;
    public Transform _camera;
    Transform target;

    public void ChangeTransform(int t)
    {
        target = cameraTransforms[t];
    }

    private void Update()
    {
        if (target != null)
        {
            _camera.position = Vector3.Lerp(_camera.position, target.position, Time.deltaTime * 0.9f);
            _camera.rotation = Quaternion.Lerp(_camera.rotation, target.rotation, Time.deltaTime * 0.9f);
        }
    }
}
