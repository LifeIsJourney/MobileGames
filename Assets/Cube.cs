using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Material red;
    [SerializeField] private Material green;

    private MeshRenderer _cubeMeshRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Cube Start()");
        _cubeMeshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeColor()
    {
        Debug.Log("Cube ChangeColor()");
        _cubeMeshRenderer.material.color = Random.ColorHSV();
    }
}
