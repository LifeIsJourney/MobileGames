/**
 * Script for enabling tilt
 */

using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    public bool isFlat = true;
    private Rigidbody _rigidBody;
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var tilt = Input.acceleration;

        if (isFlat)
        {
            tilt = Quaternion.Euler(90, 0, 0) * tilt;
        }
        
        _rigidBody.AddForce(tilt);
        Debug.DrawRay(transform.position + Vector3.up, tilt, Color.red);
    }
}
