using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; 

public class LocomotionController : MonoBehaviour
{

    [Header("Controllers")]
    public XRController leftTeleportRay;
    public XRController rightInteractor;
    [Header("Buttons")]
    public InputHelpers.Button interactorActivationButton; 
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.1f;
    public float interactionThreshold = 0.3f; 
    public bool enableLeftTeleport { get; set; } = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (leftTeleportRay)
        {
            leftTeleportRay.gameObject.SetActive(enableLeftTeleport && checkIfActivated(leftTeleportRay));
        }
        //if (rightInteractor)
        //{
        //    rightInteractor.gameObject.SetActive(checkIfGrabed(rightInteractor));
        //}

    }

    public bool checkIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }

    public bool checkIfGrabed(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, interactionThreshold);
        return !isActivated;
    }
}
