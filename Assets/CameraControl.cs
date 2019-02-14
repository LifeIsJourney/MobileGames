using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraControl : MonoBehaviour
{
    private Camera _camera;
    private float _fieldOfView;
    
    private float _zoomSpeed = 0.1f;
    private float _panSpeed = 10f;

    private Vector3 _lastTouchPosition;
    private int _panFingerId;
    
    void Awake()
    {
        _camera = GetComponent<Camera>();
        _fieldOfView = _camera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        switch (Input.touchCount)
        {
            case 1:
            {
                var touch = Input.GetTouch(0);
                
                if (touch.phase == TouchPhase.Began) {
                    _lastTouchPosition = touch.position;
                    _panFingerId = touch.fingerId;
                } else if (touch.fingerId == _panFingerId && touch.phase == TouchPhase.Moved) {
                    // Camera Panning
                    PanCamera(touch.position);
                }

                break;
            }
            case 2:
            {
                // Save the touches
                var touchZero = Input.touches[0];
                var touchOne = Input.touches[1];

                if ((touchZero.phase == TouchPhase.Stationary || touchZero.phase == TouchPhase.Moved) && touchOne.phase == TouchPhase.Moved)
                {
                    // Camera Zoom
                    ZoomCamera(touchZero, touchOne);
                }

                break;
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