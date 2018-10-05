using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampVelocity : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float maxSpeed = 20f;

    private void Clamp()
    {
	    // Trying to Limit Speed
        if (rb.velocity.magnitude > maxSpeed)
        {
           rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }

    private void Update()
    {
        Clamp();
    }
}
