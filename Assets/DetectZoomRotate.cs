using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D;

public class DetectZoomRotate : MonoBehaviour
{
    private const float PinchThreshold = 0.3f;
    private const float RotationThreshold = 20f;

    public static float TurnAngleDelta; // delta angle between touch points
    public static Vector3 InitTurnAngle; // initial angle between touch points
    public static float PinchDistDelta; // delta distance between distancing touch points
    public static float InitPinchDist; // initial distance between distancing touches
    public static Vector3 InitRotation;
    
    public static void Calculate()
    {
        if (Input.touchCount == 2)
        {
            var touchZero = Input.touches[0];
            var touchOne = Input.touches[1];

            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                InitPinchDist = Vector3.Distance(touchZero.position, touchOne.position);
                InitTurnAngle = touchZero.position - touchOne.position;
            }
            
            if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
            {
                var newPinchDist = Vector3.Distance(touchZero.position, touchOne.position);
                PinchDistDelta = newPinchDist - InitPinchDist; // save pinch difference

                var rotationVector = touchZero.position - touchOne.position;
                var rotationAngle = Vector3.Angle(rotationVector, InitRotation);
                var cross = Vector3.Cross(InitRotation, rotationVector);

                if (rotationAngle > RotationThreshold)
                {
                    
                }
            }
        }
    }
}
