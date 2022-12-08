using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuTracking : MonoBehaviour
{
    [SerializeField] private Transform mainCam;
    [SerializeField] private float distanceFromCamera = 20;
    [SerializeField] private float yOffset;
    private Vector3 forward2D;
    private Vector3 angles;

    private void Start()
    {
        this.transform.SetParent(null);
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        forward2D = mainCam.forward;
        forward2D.y = 0;
        forward2D.Normalize();
        angles = mainCam.rotation.eulerAngles;
        angles.x = 0;
        angles.z = 0;
        this.transform.rotation = Quaternion.Euler(angles);
        this.transform.position = mainCam.position + forward2D * distanceFromCamera + new Vector3(0, yOffset, 0);
    }
}
