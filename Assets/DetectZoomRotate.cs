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
    public static float TurnAngle; // angle between touch points
    public static float PinchDistDelta; // delta distance between distancing touch points
    public static float PinchDist; // distance between distancing touches

    public static void Calculate()
    {
        PinchDist = PinchDistDelta = 0;
        TurnAngle = TurnAngleDelta = 0;

        if (Input.touchCount == 2)
        {
            var touchZero = Input.touches[0];
            var touchOne = Input.touches[1];

            if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
            {
                PinchDist = Vector2.Distance(touchZero.position, touchOne.position); // distance between touches
                var prevDist = Vector2.Distance(touchZero.position - touchZero.deltaPosition,
                    touchOne.position - touchOne.deltaPosition); // distance between previous touches

                PinchDistDelta = PinchDist - prevDist; // save pinch difference

                if (Mathf.Abs(PinchDistDelta) > MinPinchDist) // if greater than min threshold
                {
                    PinchDistDelta *= PinchRatio; // perform a pinch
                }
                else
                {
                    PinchDist = PinchDistDelta = 0; // reset the pinch distances to 0
                }

                TurnAngle = Angle(touchZero.position, touchOne.position); // angle between touches
                var prevTurnAngle = Angle(touchZero.position - touchZero.deltaPosition,
                    touchOne.position - touchOne.deltaPosition); // angle between previous touches
                TurnAngleDelta = Mathf.DeltaAngle(prevTurnAngle, TurnAngle); // difference between angles

                if (Mathf.Abs(TurnAngleDelta) > MinTurnAngle) // if greater than minimum turn angle
                {
                    TurnAngleDelta *= PinchTurnRatio; // perform a turn
                }
                else
                {
                    TurnAngle = TurnAngleDelta = 0; // reset the angles to 0
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
