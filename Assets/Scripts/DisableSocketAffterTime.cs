using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DisableSocketAffterTime : MonoBehaviour
{

    private XRSocketInteractor socket; 
    // Start is called before the first frame update
    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
    }


    public void DisableSocket(float time)
    {
        if(socket != null)
        {
            StartCoroutine(waitTime(time));
        }
    }

    private IEnumerator waitTime(float time)
    {
        yield return new WaitForSeconds(time);
        socket.socketActive = false; 
    }
}
