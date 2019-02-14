using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera _camera;
    private float _zoomSpeed;
    private float _fieldOfView;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        _zoomSpeed = 0.5f;
        _fieldOfView = _camera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        switch (Input.touchCount)
        {
            case 1:
            {
                if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    
                }
                
                break;
            }
            case 2:
            {
                // Save the touches
                var touchZero = Input.touches[0];
                var touchOne = Input.touches[1];
                
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
                
                break;
            }
        }
    }
}
