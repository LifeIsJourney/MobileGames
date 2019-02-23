using UnityEngine;

/**
 * Class for enable gyroscope and allowing you to look around on your phone
 */

public class Gyro : MonoBehaviour
{
    private bool _gyroEnabled; // if gyro is enabled
    private Gyroscope _gyroscope; // gyro structure

    private GameObject _cameraContainer; // camera container object
    private Quaternion _rotation; // save rotation
    
    void Start()
    {
        _cameraContainer = new GameObject("Camera Container"); // create new game object
        _cameraContainer.transform.position = transform.position; // move container to where camera is
        transform.SetParent(_cameraContainer.transform); // set camera as child of created object
        _gyroEnabled = EnableGyro(); // check if gyro can be enabled
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope) // if system supports gyro
        {
            _gyroscope = Input.gyro; // assign gyro
            _gyroscope.enabled = true; // enable the gyro
            
            // set up rotation details and point camera in front of us on start
            _cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0);
            _rotation = new Quaternion(0, 0, 1, 0);
            
            return true;
        }

        return false; // if system doesn't support gyro
    }

    private void Update()
    {
        if (_gyroEnabled)
        {
            transform.localRotation = _gyroscope.attitude * _rotation; // transform local rotation of camera
        }
    }
}
