using System.Collections;
using UnityEngine;

public class SelectUnit : MonoBehaviour
{
    public GameObject selectedUnit;
    private RaycastHit _rayHit;
    
    // Update is called once per frame
    void Update()
    {
        if (selectedUnit == null)
        {
            if (Input.touchCount == 1)
            {
                if (Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.touches[0].position), out _rayHit))
                {
                    Debug.Log("Assigning New Object");
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
}
