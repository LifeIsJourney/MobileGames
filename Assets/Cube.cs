using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Material red;
    [SerializeField] private Material green;

    [HideInInspector] public bool currentlySelected = false;

    private MeshRenderer _cubeMeshRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Cube Start()");
        
        _cubeMeshRenderer = GetComponent<MeshRenderer>();
        ChangeColor(); // Ensure Cube Color is Set
    }

    public void ChangeColor()
    {
        Debug.Log("Cube ChangeColor()");
        
        if (!currentlySelected)
        {
            _cubeMeshRenderer.material = red;
        }
        else
        {
            _cubeMeshRenderer.material = green;
        }
    }

    public void Move(Touch touch, Cube cube)
    {
        var toDrag = cube.transform;
        var dist = toDrag.position.z - GetComponent<Camera>().transform.position.z;
        
        touch.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
        touch.position = GetComponent<Camera>().ScreenToWorldPoint(touch.position);
        toDrag.position = touch.position;
    }

    public void rotate()
    {
        if (currentlySelected)
        {
            
        }
    }
}
