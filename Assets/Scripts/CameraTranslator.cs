using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CameraTranslator : MonoBehaviour
{
    public InputHelpers.Button[] inputHelpers;
    private XRNode controllerL = XRNode.LeftHand;
    private XRNode controllerR = XRNode.RightHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float deltat = Time.deltaTime;
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(controllerL), inputHelpers[0], out bool isPressed1);
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(controllerR), inputHelpers[0], out bool isPressed);
        float pos = this.transform.localPosition.y;
        if (isPressed)
        {
            pos -= deltat * 4;
        }

        if (isPressed1)
        {
            pos += deltat * 4;
        }
        pos = Mathf.Clamp(pos, 0, 4);
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, pos, this.transform.localPosition.z);
        
    }
}
