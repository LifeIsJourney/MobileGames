using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManipulation : Manipulation
{
    private float _rotationRate = 1f;

    public override void move()
    {
        throw new System.NotImplementedException();
    }

    public override void scale()
    {
        throw new System.NotImplementedException();
    }

    public override void rotate()
    {
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);

            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                transform.Rotate(0, touch.deltaPosition.x * _rotationRate, 0, Space.World);
            }
        }
    }
}