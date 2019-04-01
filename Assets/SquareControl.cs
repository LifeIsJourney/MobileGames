using System;
using UnityEngine;
using UnityEngine.AI;

public class SquareControl : UnitBehaviour
{
    public float scaleSpeed = 0.1f;
    public float rotateSpeed = 0.1f;
    
    private CameraController _cameraController;
    private RaycastHit _hit; // RayCastHit variable
    private Camera _mainCamera; // Main camera variable
    private bool _isRotating; // Track if unit is rotating
    private bool _isDragging; // Track if unit is being dragged
    
    private const float PinchThreshold = 0.3f;
    private const float RotationThreshold = 20f;

    private float _pinchDistDelta; // delta distance between distancing touch points
    private float _initPinchDist; // initial distance between distancing touches
    private static Vector3 _initRotation;

    void Start()
    {
        Debug.Log("Unit Start()");
        _mainCamera = Camera.main; // Assign camera variable
        if (_mainCamera != null) _cameraController = _mainCamera.GetComponent<CameraController>(); // Assign camera controller
        GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update()
    {
        if (_cameraController != null && _cameraController.selectedUnit == gameObject
        ) // If a unit is selected and the unit is a gameObject
        {
            if (Input.touchCount == 1) // If one finger is touch
            {
                if (Input.touches[0].phase == TouchPhase.Began &&
                    Physics.Raycast(_mainCamera.ScreenPointToRay(Input.touches[0].position), out _hit)
                ) // If touch phase is began and it hits a unit
                {
                    if (_hit.transform.CompareTag("SelectableUnit")) // If the unit is selectable
                    {
                        Debug.Log("Ready To Drag");
                        _isDragging = true;
                    }
                }

                if (Input.touches[0].phase == TouchPhase.Moved && _isDragging &&
                    Physics.Raycast(_mainCamera.ScreenPointToRay(Input.touches[0].position), out _hit))
                {
                    if (_hit.collider.CompareTag("SelectableUnit"))
                    {
                        Debug.Log("Moving");
                        Move();
                    }
                }

                if ((Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Ended) &&
                    _isDragging)
                {
                    Debug.Log("Finished Moving");
                    _isDragging = false;
                }
            }

            if (Input.touchCount == 2)
            {
                ScaleOrRotate();
            }
        }
    }


    public override void Move()
    {
        var newPos = _hit.point;
        var position = _cameraController.selectedUnit.transform.position;
        newPos.y = position.y;

        position = Vector3.Lerp(position, newPos, 5f);
        _cameraController.selectedUnit.transform.position = position;
    }
    
    public override void ScaleOrRotate()
    {
        var touchZero = Input.touches[0];
        var touchOne = Input.touches[1];

        if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
        {
            _initPinchDist = Vector3.Distance(touchZero.position, touchOne.position);
            _initRotation = touchZero.position - touchOne.position;
        }
            
        if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
        {
            var newPinchDist = Vector3.Distance(touchZero.position, touchOne.position);
            _pinchDistDelta = newPinchDist - _initPinchDist; // save pinch difference

            var rotationVector = touchZero.position - touchOne.position;
            var rotationAngle = Vector3.Angle(rotationVector, _initRotation);
            var cross = Vector3.Cross(_initRotation, rotationVector);

            if (rotationAngle > RotationThreshold)
            {
                if (cross.z > 0)
                {
                    Rotate(rotationAngle);
                }
                else if (cross.z < 0)
                {
                    Rotate(-rotationAngle);
                }
            }
            else if (Math.Abs(_pinchDistDelta) >= PinchThreshold)
            {
                Scale();
            }
        }
    }

    public override void Scale()
    {
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

        // Change the scale of the object
        _cameraController.selectedUnit.transform.localScale += Vector3.one * magDiff * scaleSpeed;
    }

    public override void Rotate(float rotationAngle)
    {
        _cameraController.selectedUnit.transform.Rotate(0f, rotateSpeed * rotationAngle, 0f, Space.World);
    }
}