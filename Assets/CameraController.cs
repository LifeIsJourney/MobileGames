using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject selectedUnit;
    public float panSpeed = 10f;
    public float zoomSpeed = 0.1f;
    public float rotateSpeed = 1f;

    private RaycastHit _rayHit;
    private Camera _camera;
    private float _fieldOfView;

    private bool _hasMoved;
    // private Vector3 _prevTouchPos;

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
                    // _prevTouchPos = Input.touches[0].position;
                    _hasMoved = false;
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
                // TODO: Differentiate Between Pinch and Rotate
                Debug.Log("Two Fingers Touched - Nothing Selected");
                if (Input.touches[0].phase == TouchPhase.Moved && Input.touches[1].phase == TouchPhase.Moved)
                {
                    Debug.Log("Zooming Camera");
                    ZoomCamera(Input.touches[0], Input.touches[1]);
                }
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
        var touchDeltaPosition = Input.touches[0].deltaPosition; // Get position since last change
        transform.Translate(-touchDeltaPosition.x * panSpeed * Time.deltaTime,
            0, -touchDeltaPosition.y * panSpeed * Time.deltaTime); // Move camera around x and z axis

        /*
        var offset = _camera.ScreenToViewportPoint(_prevTouchPos - newTouchPosition);
        var move = new Vector3(offset.x * panSpeed, 0, offset.y * panSpeed);
        transform.Translate(move, Space.World);
        _prevTouchPos = newTouchPosition;
        */
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
    private void RotateCamera(Touch touchZero, Touch touchOne)
    {
        // transform.Rotate(0, Input.touches[0].deltaPosition.x * rotateSpeed, 0, Space.World);
    }
}