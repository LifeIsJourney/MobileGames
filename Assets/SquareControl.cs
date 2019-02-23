using UnityEngine;
using UnityEngine.AI;

public class SquareControl : UnitBehaviour
{
    public float scaleSpeed = 0.1f;
    
    private CameraController _cameraController;
    private RaycastHit _hit; // RaycastHit variable
    private Camera _mainCamera; // Main camera variable
    private float _fieldOfView; // Camera field of view
    private bool _isRotating; // Track if unit is rotating
    private bool _isDragging; // Track if unit is being dragged
    private Vector3 _currentDragPos;

    void Start()
    {
        Debug.Log("Unit Start()");
        _mainCamera = Camera.main; // Assign camera variable
        if (_mainCamera != null)
        {
            _fieldOfView = _mainCamera.fieldOfView;
            _cameraController = _mainCamera.GetComponent<CameraController>(); // Assign camera controller
        }
    }

    void Update()
    {
        if (_cameraController != null && _cameraController.selectedUnit == gameObject
        ) // If a unit is selected and the unit is a gameObject
        {
            if (Input.touchCount == 1) // If one finger is touch
            {
                _currentDragPos = Input.GetTouch(0).position;
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
                Scale();
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

    public override void Rotate()
    {
        throw new System.NotImplementedException();
    }
}