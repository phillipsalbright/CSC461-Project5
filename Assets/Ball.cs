using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private float reflectSpeed;
    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(1, 1, 1) * 5, ForceMode.Impulse);
        reflectSpeed = 5;
        velocity = new Vector3(1, 1, 1) * 5;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            rb.velocity = (Vector3.Reflect(velocity, collision.GetContact(0).normal).normalized) * reflectSpeed;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            reflectSpeed = rb.velocity.magnitude;
            velocity = rb.velocity;
        }
    }
}
