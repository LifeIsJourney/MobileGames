using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControl : MonoBehaviour
{
    private GameObject targetObject = null;

    void Start()
    {
        targetObject = GameObject.Find("CubeTwo");
        Debug.Log(targetObject.transform.position);
    }
    
    void Update()
    {
        targetObject = GameObject.Find("CubeTwo");

        transform.position = Vector3.MoveTowards(transform.position, targetObject.transform.position, 0.3f);
    }
}
