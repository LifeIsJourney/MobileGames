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
    private RaycastHit _rayHit;
    
    private float _zoomSpeed = 0.1f;
    
    private float _panSpeed = 5f;
    private Vector3 _lastTouchPosition;
    private int _panFingerId;

    private bool _isTap;
    
    void Awake()
    {
        _camera = GetComponent<Camera>();
        _fieldOfView = _camera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Debug.Log("Entered Began Phase");
                    _lastTouchPosition = touch.position;
                    _panFingerId = touch.fingerId;
                    _isTap = true;
                    break;
                
                case TouchPhase.Moved:
                    Debug.Log("Entered Moved Phase");
                    if (touch.fingerId == _panFingerId)
                    {
                        Debug.Log("Panning Camera");
                        PanCamera(touch.position);

                        _isTap = false;
                    }
                    break;
                    
                case TouchPhase.Stationary:
                    Debug.Log("Entered Stationary Phase");
                    break;
                
                case TouchPhase.Ended:
                    Debug.Log("Entered Ended Phase");

                    if (_isTap)
                    {
                        if (Physics.Raycast(_camera.ScreenPointToRay(touch.position), out _rayHit))
                        {
                            Debug.Log("Entered Tap Area");
                            Debug.Log(_rayHit.collider.name);
                            _rayHit.collider.GetComponent<Cube>().ChangeColor();
                        }
                    }
                    break;
            }
        }
        else if (Input.touchCount == 2)
        {
            var touchZero = Input.touches[0];
            var touchOne = Input.touches[1];

            if (touchZero.phase == TouchPhase.Moved && touchOne.phase == TouchPhase.Moved)
            {
                Debug.Log("Zooming Camera");
                ZoomCamera(touchZero, touchOne);
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