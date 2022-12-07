using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private float reflectSpeed;
    private Vector3 velocity;
    private float minReflectSpeed = 1;

    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreLabel;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(1, 1, 1), ForceMode.Impulse);
        reflectSpeed = 5;
        velocity = new Vector3(0, 0, 0);
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
            Destroy(collision.gameObject);
            score += 100;
            scoreLabel.text = "Score: " + score;
            if (rb.velocity.magnitude < 5)
            {
                rb.velocity = rb.velocity * 5 / rb.velocity.magnitude;
            }
        } else if (collision.gameObject.layer == 8)
        {
            rb.velocity = (Vector3.Reflect(velocity, collision.GetContact(0).normal).normalized) * reflectSpeed;
            if (rb.velocity.magnitude < 5)
            {
                rb.velocity = rb.velocity * 5 / rb.velocity.magnitude;
            }
        } else if (collision.gameObject.layer == 9)
        {
            GameObject.FindObjectOfType<GameManager>().LifeLost();
            Destroy(this.gameObject);
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
