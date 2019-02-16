using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class CameraControl : MonoBehaviour
{
    private Camera _camera;
    private float _fieldOfView;
    
    private float _zoomSpeed = 0.1f;
    
    private float _panSpeed = 5f;
    private Vector3 _lastTouchPosition;
    private int _panFingerId;

    private GameObject _selectUnit;
    
    void Awake()
    {
        _camera = GetComponent<Camera>();
        _fieldOfView = _camera.fieldOfView;
        _selectUnit = GetComponent<SelectUnit>().selectedUnit;
    }

    // Update is called once per frame
    void Update()
    {
        if (_selectUnit == null)
        {
            if (Input.touchCount == 1)
            {
                switch (Input.GetTouch(0).phase)
                {
                    case TouchPhase.Began:
                        Debug.Log("Entered Began Phase");
                        _lastTouchPosition = Input.GetTouch(0).position;
                        _panFingerId = Input.GetTouch(0).fingerId;
                        break;

                    case TouchPhase.Moved:
                        Debug.Log("Entered Moved Phase");
                        if (Input.GetTouch(0).fingerId == _panFingerId)
                        {
                            Debug.Log("Panning Camera");
                            PanCamera(Input.GetTouch(0).position);
                        }

                        break;

                    case TouchPhase.Stationary:
                        Debug.Log("Entered Stationary Phase");
                        break;

                    case TouchPhase.Ended:
                        Debug.Log("Entered Ended Phase");
                        break;
                }
            }
            else if (Input.touchCount == 2)
            {
                if (Input.touches[0].phase == TouchPhase.Moved && Input.touches[1].phase == TouchPhase.Moved)
                {
                    Debug.Log("Zooming Camera");
                    ZoomCamera(Input.touches[0], Input.touches[1]);
                }
            }
        }
    }

    void ZoomCamera(Touch touchZero, Touch touchOne)
    {
        // Store touch position from previous frame using delta position
        var touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        var touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        // Distance between the touches in each frame
        var prevTouchDistance = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        var curTouchDistance = (touchZero.position - touchOne.position).magnitude;

        // Difference in distances between each frame
        var magDiff = prevTouchDistance - curTouchDistance;

        // Change field of view in relation to distance between touches
        _fieldOfView += magDiff * _zoomSpeed;
        _camera.fieldOfView = _fieldOfView;
        _camera.fieldOfView = Mathf.Clamp(_fieldOfView, 0.1f, 119.9f);
    }

    void PanCamera(Vector3 newTouchPosition)
    {
        // Determine how much to move the camera
        var offset = _camera.ScreenToViewportPoint(_lastTouchPosition - newTouchPosition);
        var move = new Vector3(offset.x * _panSpeed, 0, offset.y * _panSpeed);
        
        // Perform panning
        transform.Translate(move, Space.World);

        // Cache last position
        _lastTouchPosition = newTouchPosition;
    }
}