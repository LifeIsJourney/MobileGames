using System;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour
{
    public GameObject selectedUnit;
    public float panSpeed = 0.1f;
    public float zoomSpeed = 0.1f;
    public float rotateSpeed = 0.1f;

    private RaycastHit _rayHit;
    private Camera _camera;
    private float _fieldOfView;
    private bool _hasMoved;
    private Vector3 _panInitPos;
    
    private const float PinchThreshold = 0.3f;
    private const float RotationThreshold = 20f;

    private float _pinchDistDelta; // delta distance between distancing touch points
    private float _initPinchDist; // initial distance between distancing touches
    private static Vector3 _initRotation;

    private void Start()
    {
        Debug.Log("Camera Start()");
        _camera = GetComponent<Camera>();
        _fieldOfView = _camera.fieldOfView;
    }

    void Update()
    {
        if (selectedUnit == null) // If there is no unit selected
        {
            if (Input.touchCount == 1) // If there is one touch on the screen
            {
                Debug.Log("One Finger Touched - Nothing Selected");
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    _hasMoved = false;
                    _panInitPos = Input.touches[0].position; // Get initial pan position on each began
                }

                if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    Debug.Log("Finger Moved - Panning Camera");
                    PanCamera(Input.touches[0].position);
                    _hasMoved = true;
                }

                if (!_hasMoved && Input.touches[0].phase == TouchPhase.Ended &&
                    Physics.Raycast(_camera.ScreenPointToRay(Input.touches[0].position), out _rayHit))
                {
                    Debug.Log("Object Tapped - Assigning New Object");
                    if (_rayHit.transform.CompareTag("SelectableUnit"))
                    {
                        selectedUnit = _rayHit.transform.gameObject;
                        selectedUnit.transform.Find("Marker").gameObject.SetActive(true);
                    }
                }
            }

            if (Input.touchCount == 2)
            {
                ZoomRotate();
            }
        }
        else // If a unit is selected
        {
            if (Input.touchCount == 1) // If there is one touch
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    _hasMoved = false;
                }

                if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    _hasMoved = true;
                }
                
                if (!_hasMoved && Input.touches[0].phase == TouchPhase.Ended &&
                    Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.touches[0].position), out _rayHit)) // If a ray hits anything and touch has not moved
                {
                    if (_rayHit.transform.CompareTag("SelectableUnit")) // If it hits a selectable unit
                    {
                        var objectHit = _rayHit.transform.gameObject; // Save the hit object
                        
                        if (selectedUnit.transform.name == objectHit.transform.name) // If the currently selected unit is the same as the hit object
                        {
                            Debug.Log("Same Object - Deselecting");
                            
                            selectedUnit.transform.Find("Marker").gameObject.SetActive(false); // Deactivate unit marker
                            selectedUnit = null; // Set currently selected unit to null
                        }
                        else // If a new unit is hit
                        {
                            Debug.Log("Not Same Object - Switching Object");
                            
                            selectedUnit.transform.Find("Marker").gameObject.SetActive(false); // Set current unit marker to false
                            selectedUnit = null; // Set currently selected unit to null

                            selectedUnit = objectHit; // Set selected unit to the newly hit unit
                            selectedUnit.transform.Find("Marker").gameObject.SetActive(true); // Set the marker to true
                        }
                    }
                }
            }
        }
    }

    /*
     * Method for Panning Camera
     */
    private void PanCamera(Vector3 newTouchPosition)
    {
        var curPanPos = newTouchPosition; // The current pan position will equal to new touch position on moved
        var deltaPanPos = _camera.ScreenToViewportPoint(curPanPos - _panInitPos); // Difference between current and initial position
        deltaPanPos = -deltaPanPos; // Inverts the camera pan direction

        var moveCamera = new Vector3(deltaPanPos.x * panSpeed, 0, deltaPanPos.y * panSpeed); // How the camera will be panned
        
        transform.Translate(moveCamera, Space.World); // Pan the camera
    }

    /*
     * Method for Zooming Camera
     */
    private void ZoomCamera(Touch touchZero, Touch touchOne)
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
        _fieldOfView += magDiff * zoomSpeed;
        _camera.fieldOfView = _fieldOfView;
        _camera.fieldOfView = Mathf.Clamp(_fieldOfView, 0.1f, 119.9f);
    }

    /*
     * Method for Rotating Camera
     */
    private void RotateCamera(float rotationAngle)
    {
        transform.Rotate(0f, rotateSpeed/60 * rotationAngle, 0f, Space.World);
    }
    
    /*
     * Method for Handling Zoom and Rotate
     */
    private void ZoomRotate()
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
                    RotateCamera(rotationAngle);
                }
                else if (cross.z < 0)
                {
                    RotateCamera(-rotationAngle);
                }
            }
            else if (Math.Abs(_pinchDistDelta) >= PinchThreshold)
            {
                ZoomCamera(touchZero, touchOne);
            }
        }
    }
}