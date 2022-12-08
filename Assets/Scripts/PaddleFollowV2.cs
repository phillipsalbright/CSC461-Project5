using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleFollowV2 : MonoBehaviour
{
    private Rigidbody rb;
    private Transform target;
    [SerializeField] private GameObject objectToFollow;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;
    private float frequency = 50f;
    private float damping = 1f;
    private float rotationFrequency = 100f;
    private float rotationDamping = .9f;
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
        rb.maxAngularVelocity = float.PositiveInfinity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float kp = (6f * frequency) * (6f * frequency) * .25f;
        float kd = 4.5f * frequency * damping;
        float g = 1 / (1 + kd * Time.fixedDeltaTime + kp * Time.fixedDeltaTime * Time.fixedDeltaTime);
        float ksg = kp * g;
        float kdg = (kd + kp * Time.fixedDeltaTime) * g;
        Vector3 force = (target.position + positionOffset - transform.position) * ksg - rb.velocity * kdg;
        rb.AddForce(force, ForceMode.Acceleration);


         kp = (6 * rotationFrequency) * (6f * rotationFrequency) * .25f;
         kd = 4.5f * rotationFrequency * rotationDamping;
         g = 1 / (1 + kd + Time.fixedDeltaTime + kp * Time.fixedDeltaTime * Time.fixedDeltaTime);
         ksg = kp * g;
        kdg = (kd + kp * Time.fixedDeltaTime) * g;
        Quaternion q = target.rotation * Quaternion.Inverse(transform.rotation);
        if (q.w < 0)
        {
            q.x = -q.x;
            q.y = -q.y;
            q.z = -q.z;
            q.w = -q.w;
        }
        q.ToAngleAxis(out float angle, out Vector3 axis);
        axis.Normalize();
        axis *= Mathf.Deg2Rad;
        Vector3 torque = ksg * axis * angle + -rb.angularVelocity * kdg;
        rb.AddTorque(torque, ForceMode.Acceleration);

    }
}
