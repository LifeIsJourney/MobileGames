using UnityEngine;

public class SelectUnit : MonoBehaviour
{
    public GameObject selectedUnit;
    public float panSpeed = 10f;

    private RaycastHit _rayHit;
    private Camera _camera;
    private bool _hasMoved;
    private Vector3 _prevTouchPos;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (selectedUnit == null) // If there is no unit selected
        {
            Debug.Log("Nothing Currently Selected");
            if (Input.touchCount == 1) // If there is one touch on the screen
            {
                Debug.Log("One Finger Touched");
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    _prevTouchPos = Input.touches[0].position;
                }
                
                if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    Debug.Log("Finger Moved - Panning Camera");
                    PanCamera(Input.touches[0].position);
                    _hasMoved = true;
                }

                if (!_hasMoved && Physics.Raycast(_camera.ScreenPointToRay(Input.touches[0].position), out _rayHit))
                {
                    Debug.Log("Object Tapped - Assigning New Object");
                    if (_rayHit.transform.CompareTag("SelectableUnit"))
                    {
                        selectedUnit = _rayHit.transform.gameObject;
                        selectedUnit.transform.Find("Marker").gameObject.SetActive(true);
                    }
                }
            }
        }
        else
        {
            if (Input.touchCount == 1)
            {
                if (Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.touches[0].position), out _rayHit))
                {
                    if (_rayHit.transform.CompareTag("SelectableUnit"))
                    {
                        Debug.Log("Switching Selected Object");
                        selectedUnit.transform.Find("Marker").gameObject.SetActive(false);
                        selectedUnit = null;
                        selectedUnit = _rayHit.transform.gameObject;
                        selectedUnit.transform.Find("Marker").gameObject.SetActive(true);
                    }
                }
                else if (!_rayHit.collider)
                {
                    selectedUnit.transform.Find("Marker").gameObject.SetActive(false);
                    selectedUnit = null;
                }
            }
        }
    }

    /*
     * Method for Panning Camera
     */
    private void PanCamera(Vector3 newTouchPosition)
    {
        var touchDeltaPosition = Input.touches[0].deltaPosition;
        transform.Translate(-touchDeltaPosition.x * panSpeed * Time.deltaTime,
            0, -touchDeltaPosition.y * panSpeed * Time.deltaTime);
        
        /*
        var offset = _camera.ScreenToViewportPoint(_prevTouchPos - newTouchPosition);
        var move = new Vector3(offset.x * panSpeed, 0, offset.y * panSpeed);
        transform.Translate(move, Space.World);
        _prevTouchPos = newTouchPosition;
        */
    }
}