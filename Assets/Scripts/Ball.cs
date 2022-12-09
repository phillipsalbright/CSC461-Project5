using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private float reflectSpeed;
    private Vector3 velocity;
    private float minReflectSpeed = 1;
    private Vector3 originPoint;

    private bool paused = false;
    private Vector3 storedVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        originPoint = this.transform.position;
        rb = this.GetComponent<Rigidbody>();
        //rb.AddForce(new Vector3(1, 1, 1) * 2, ForceMode.Impulse);
        reflectSpeed = 5;
        velocity = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = rb.velocity;
    }

    public void PauseBall()
    {
        paused = true;
        storedVelocity = velocity;
        rb.velocity = Vector3.zero;
        Debug.Log("paused");
    }

    public void UnpauseBall()
    {
        paused = false;
        rb.velocity = storedVelocity;
        Debug.Log("not paused");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            rb.velocity = (Vector3.Reflect(velocity, collision.GetContact(0).normal).normalized) * reflectSpeed;
            if (rb.velocity.magnitude < 5)
            {
                rb.velocity = rb.velocity * 5 / rb.velocity.magnitude;
            } else if (rb.velocity.magnitude > 8.5f)
            {
                rb.velocity = rb.velocity * 8.5f / rb.velocity.magnitude;
            }
            if (collision.gameObject.GetComponent<Brick>() != null)
            {
                BrickManager.BreakBrick(collision.gameObject);
            }
            else
            {
                Debug.Log("non brick on brick collision layer");
            }
        } else if (collision.gameObject.layer == 8)
        {
            rb.velocity = (Vector3.Reflect(velocity, collision.GetContact(0).normal).normalized) * reflectSpeed;
            if (rb.velocity.magnitude < 5)
            {
                rb.velocity = rb.velocity * 5 / rb.velocity.magnitude;
            }
            else if (rb.velocity.magnitude > 8.5f)
            {
                rb.velocity = rb.velocity * 8.5f / rb.velocity.magnitude;
            }
        } else if (collision.gameObject.layer == 9)
        {
            GameObject.FindObjectOfType<GameManager>().LifeLost();
            rb.velocity = Vector3.zero;
            this.transform.position = originPoint;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            reflectSpeed = rb.velocity.magnitude;
            velocity = rb.velocity;
            Debug.Log(velocity);
        }
    }
}
