using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        switch (Input.touchCount)
        {
            case 1:
            {
                var ray = _camera.ScreenPointToRay(Input.touches[0].position);
                Debug.DrawRay(ray.origin, 100 * ray.direction);
                
                break;
            }
        }
    }
}
