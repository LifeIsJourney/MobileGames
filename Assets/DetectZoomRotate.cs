using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D;

public class DetectZoomRotate : MonoBehaviour
{
    private const float PinchTurnRatio = Mathf.PI;
    private const float MinTurnAngle = 0;
    private const float PinchRatio = 1;
    private const float MinPinchDist = 0;

    public static float TurnAngleDelta; // delta angle between touch points
    public static float InitTurnAngle; // initial angle between touch points
    public static float PinchDistDelta; // delta distance between distancing touch points
    public static float InitPinchDist; // initial distance between distancing touches

    public static void Calculate()
    {
        InitPinchDist = PinchDistDelta = 0;
        InitTurnAngle = TurnAngleDelta = 0;

        if (Input.touchCount == 2)
        {
            var touchZero = Input.touches[0];
            var touchOne = Input.touches[1];

            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                InitPinchDist = Vector3.Distance(touchZero.position, touchOne.position);
                InitTurnAngle = Angle(touchZero.position, touchOne.position);
            }
            
            if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
            {
                var newPinchDist = Vector3.Distance(touchZero.position, touchOne.position);
                PinchDistDelta = newPinchDist - InitPinchDist; // save pinch difference

                if (Mathf.Abs(PinchDistDelta) > MinPinchDist) // if greater than min threshold
                {
                    PinchDistDelta *= PinchRatio; // perform a pinch
                }
                else
                {
                    InitPinchDist = PinchDistDelta = 0; // reset the pinch distances to 0
                }

                var newTurnAngle = Angle(touchZero.position, touchOne.position); // angle between previous touches
                TurnAngleDelta = newTurnAngle - InitTurnAngle; // difference between angles

                if (Mathf.Abs(TurnAngleDelta) > MinTurnAngle) // if greater than minimum turn angle
                {
                    TurnAngleDelta *= PinchTurnRatio; // perform a turn
                }
                else
                {
                    InitTurnAngle = TurnAngleDelta = 0; // reset the angles to 0
                }
            }
        }
    }

    private static float Angle(Vector2 touchZeroPosition, Vector2 touchOnePosition)
    {
        var from = touchZeroPosition - touchOnePosition;
        var to = new Vector2(1, 0);

        var result = Vector2.Angle(from, to);
        var cross = Vector3.Cross(from, to);

        if (cross.z > 0)
        {
            result = 360f - result;
        }

        return result;
    }
}
