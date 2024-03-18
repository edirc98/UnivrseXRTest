using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit;

public class Slicer : MonoBehaviour
{
    #region PUBLIC PARAMETERS
    public GameObject hullsParent;
    public Material crossSectionMaterial;
    public bool isCutting = false;
    //public GameObject ObjectToCut; 
    #endregion
    #region PROTECTED PARAMETERS
    [SerializeField]
    public GameObject _cuttingPlane;
    #endregion

    #region PRIVATE PARAMETERS
    private GameObject resultUp;
    private GameObject resultLow;

    private XRController activeController;
    private XRGrabInteractable interactable;
    private IXRSelectInteractor interactor; 

    private void Start()
    {
        interactable = transform.parent.GetComponent<XRGrabInteractable>();
        
    }
    #endregion
    private void Update()
    {
        interactor = interactable.firstInteractorSelecting;
        if(interactable != null && interactor != null)
        {
            Debug.Log(interactor.transform.gameObject.name);
            activeController = interactor.transform.gameObject.GetComponent<XRController>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!isCutting)
        {
            isCutting = true;
            if (other.gameObject.layer == 6 && other.CompareTag("SpawnedMat"))
            {
                SlicedHull hull = sliceObject(other.gameObject, crossSectionMaterial);
                if(activeController != null) activeController.SendHapticImpulse(0.7f, 0.2f);
                if (hull != null)
                {
                    resultLow = hull.CreateLowerHull(other.gameObject, crossSectionMaterial);
                    resultUp = hull.CreateUpperHull(other.gameObject, crossSectionMaterial);

                }
                else
                {
                    Debug.Log("Hull was null");
                }
                if (resultUp != null && resultLow != null)
                {
                    Destroy(other.gameObject);
                    updateResultObject(resultUp);
                    updateResultObject(resultLow);
                }
                else
                {
                    Destroy(resultLow);
                    Destroy(resultUp);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isCutting = false; 
    }

    private SlicedHull sliceObject(GameObject obj,Material crossSectionMat)
    {
        return obj.Slice(_cuttingPlane.transform.position, _cuttingPlane.transform.up, crossSectionMat);
    }

    private void updateResultObject(GameObject result)
    {
        if(result != null)
        {
            //result.layer = 6;
            result.transform.parent = hullsParent.transform;
            //Add Collider to the result
            MeshCollider collider =  result.AddComponent<MeshCollider>();
            if(collider != null)
            {
                collider.convex = true;
                //Add rigidbody if collider is not null to avoid problems
                Rigidbody rb = result.AddComponent<Rigidbody>();
                if (rb != null)
                { 
                    rb.mass = 2;
                    rb.useGravity = true;
                    rb.AddForce(new Vector3(0.5f, 0.1f, 0), ForceMode.Impulse);
                }
            }
        }
        //if result was null destroy it
        else
        {
            Destroy(result);
        }
        
    }

}
