﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public SelectUnit selectUnit;
    private NavMeshAgent _agent;
    private RaycastHit _hit;
    private Camera _mainCamera;
    
    private readonly float _rotateSpeed = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Unit Start()");
        _mainCamera = Camera.main;
        if (_mainCamera != null) selectUnit = _mainCamera.GetComponent<SelectUnit>();
        _agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectUnit != null && selectUnit.selectedUnit == gameObject)
        {
            if (Input.touchCount == 1)
            {
                if (Input.touches[0].phase != TouchPhase.Moved && Physics.Raycast(_mainCamera.ScreenPointToRay(Input.touches[0].position), out _hit))
                {
                    if (_hit.transform.CompareTag("Floor"))
                    {
                        Debug.Log("Ray Hit Floor");
                        _agent.destination = _hit.point;
                    }
                }

                if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    transform.Rotate(0, Input.touches[0].deltaPosition.x * _rotateSpeed, 0, Space.World);
                }
            }
        }
    }
}
