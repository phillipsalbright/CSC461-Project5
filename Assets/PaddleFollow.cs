using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleFollow : MonoBehaviour
{
    private Rigidbody rb;
    private Transform target;
    [SerializeField] private GameObject objectToFollow;
    private float followSpeed = 40;
    private float rotateSpeed = 3000;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        target = objectToFollow.transform;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.mass = 20f;

        rb.position = target.position;
        rb.rotation = target.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionWithOffset = target.position + positionOffset;
        float distance = Vector3.Distance(positionWithOffset, this.transform.position);
        if (distance > .03)
        {
            rb.velocity = (positionWithOffset - this.transform.position).normalized * (followSpeed * distance);

        } else
        {
            rb.velocity = (positionWithOffset - this.transform.position).normalized * (followSpeed * distance);
            Debug.Log("froggy");
        }

        Quaternion rotationWithOffset = target.rotation * Quaternion.Euler(rotationOffset);
        Quaternion q = rotationWithOffset * Quaternion.Inverse(this.transform.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        if (Mathf.Abs(axis.magnitude) != Mathf.Infinity && angle > 4)
        {
            if (angle > 180.0f) { angle -= 360.0f; }
            rb.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);
        } else if (angle <= 3) {
            rb.angularVelocity = Vector3.zero;
        }
        
        
    }
}
