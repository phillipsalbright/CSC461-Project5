using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerMenuManager : MonoBehaviour
{
    public GameObject Menu;
    [SerializeField] private GameObject[] paddles;
    public InputHelpers.Button[] inputHelpers;
    public XRNode controller = XRNode.LeftHand;
    private bool buttonPressed = false;

    private void Update()
    {
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(controller), inputHelpers[1], out bool isPressed1);
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(controller), inputHelpers[0], out bool isPressed);
        if ((isPressed) && (isPressed1) && !buttonPressed)
        {
            buttonPressed = true;
            if (Menu.activeInHierarchy)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
        else if (!(isPressed))
        {
            buttonPressed = false;
        }
    }
    public void OpenMenu()
    {
        Menu.SetActive(true);
        foreach (GameObject g in paddles)
        {
            g.SetActive(false);
        }
    }

    public void CloseMenu()
    {
        Menu.SetActive(false);
        foreach (GameObject g in paddles)
        {
            g.SetActive(true);
        }
    }
}
