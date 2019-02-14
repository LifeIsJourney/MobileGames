using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _cameraOffset;
    private float _smoothness = 0.5f;
    private float _zoomSpeed;
    private float _rotationSpeed = 5;
    private float _fieldOfView;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        _cameraOffset = transform.position - target.position;
        _zoomSpeed = 0.5f;
        _fieldOfView = _camera.fieldOfView;
        transform.LookAt(target);
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
                    float touchDelta = Mathf.Clamp(Input.GetTouch(0).deltaPosition.x, -1.0f, 1.0f);
                    Quaternion camAngle = Quaternion.AngleAxis(touchDelta * _rotationSpeed, Vector3.up);

                    Vector3 newPos = target.position + _cameraOffset;
                    _cameraOffset = camAngle * _cameraOffset;

                    transform.position = Vector3.Slerp(transform.position, newPos, _smoothness);
                    transform.LookAt(target);
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