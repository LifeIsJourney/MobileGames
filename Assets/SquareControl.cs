using UnityEngine;
using UnityEngine.AI;

public class SquareControl : UnitBehaviour
{
    private CameraController _cameraController;
    
    private RaycastHit _hit; // RaycastHit variable
    private Camera _mainCamera; // Main camera variable
    // private readonly float _rotateSpeed = 1f; // Unit rotation speed
    private bool _isRotating; // Track if unit is rotating
    private bool _isDragging; // Track if unit is being dragged
    private Vector3 _currentDragPos;
    
    void Start()
    {
        Debug.Log("Unit Start()");
        _mainCamera = Camera.main; // Assign camera variable
        if (_mainCamera != null) _cameraController = _mainCamera.GetComponent<CameraController>(); // Assign camera controller
    }

    void Update()
    {
        if (_cameraController != null && _cameraController.selectedUnit == gameObject) // If a unit is selected and the unit is a gameObject
        {
            if (Input.touchCount == 1) // If one finger is touch
            {
                _currentDragPos = Input.GetTouch(0).position;
                if (Input.touches[0].phase == TouchPhase.Began && 
                    Physics.Raycast(_mainCamera.ScreenPointToRay(Input.touches[0].position), out _hit)) // If touch phase is began and it hits a unit
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
                        var newPos = _hit.point;
                        var position = _cameraController.selectedUnit.transform.position;
                        newPos.y = position.y;

                        position = Vector3.Lerp(position, newPos, 5f);
                        _cameraController.selectedUnit.transform.position = position;
                    }
                }

                if ((Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Ended) &&
                    _isDragging)
                {
                    Debug.Log("Finished Moving");
                    _isDragging = false;
                }
            }
        }
    }


    public override void Move()
    {
        throw new System.NotImplementedException();
    }

    public override void Scale()
    {
        throw new System.NotImplementedException();
    }

    public override void Rotate()
    {
        throw new System.NotImplementedException();
    }
}
