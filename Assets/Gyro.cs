using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyro : MonoBehaviour
{
    private bool _gyroEnabled;
    private Gyroscope _gyroscope;

    private GameObject _cameraContainer;
    private Quaternion _rotation;
    
    void Start()
    {
        _cameraContainer = new GameObject("Camera Container");
        _cameraContainer.transform.position = transform.position;
        transform.SetParent(_cameraContainer.transform);
        _gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            _gyroscope = Input.gyro;
            _gyroscope.enabled = true;
            
            _cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0);
            _rotation = new Quaternion(0, 0, 1, 0);
            
            return true;
        }

        return false;
    }

    private void Update()
    {
        if (_gyroEnabled)
        {
            transform.localRotation = _gyroscope.attitude * _rotation;
        }
    }
}
