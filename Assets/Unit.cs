﻿using UnityEngine;
using UnityEngine.AI;

public class SquareControl : UnitBehaviour
{
    public SelectUnit selectUnit;
    private NavMeshAgent _agent;
    private RaycastHit _hit;
    private Camera _mainCamera;
    
    private readonly float _rotateSpeed = 1f;

    private bool _isRotating;
    
    void Start()
    {
        Debug.Log("Unit Start()");
        _mainCamera = Camera.main;
        if (_mainCamera != null) selectUnit = _mainCamera.GetComponent<SelectUnit>();
        _agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (selectUnit != null && selectUnit.selectedUnit == gameObject)
        {
            if (Input.touchCount == 1)
            {
                if (Input.touches[0].phase != TouchPhase.Moved && 
                    Physics.Raycast(_mainCamera.ScreenPointToRay(Input.touches[0].position), out _hit) && !_isRotating)
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
                
                if (Input.touches[0].phase == TouchPhase.Moved && 
                    Physics.Raycast(_mainCamera.ScreenPointToRay(Input.touches[0].position), out _hit))
                {
                    if (_hit.transform.CompareTag("SelectableUnit"))
                    {
                        _isRotating = true;
                        var prevPos = Vector3.zero;
                        var posDelta = _mainCamera.ScreenToWorldPoint(Input.touches[0].position) - prevPos;
                        
                        transform.Rotate(transform.up, Vector3.Dot(posDelta, _mainCamera.transform.right), Space.World);

                        prevPos = Input.touches[0].position;
                    }

                    _isRotating = false;
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

    public override void OnSelect()
    {
        throw new System.NotImplementedException();
    }

    public override void OnDeselect()
    {
        throw new System.NotImplementedException();
    }
}
