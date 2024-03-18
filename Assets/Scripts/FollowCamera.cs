using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject camera;
    public Vector3 offset; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 newPos = camera.transform.position;
        newPos += offset; 
        transform.position = newPos;

        transform.forward = new Vector3(camera.transform.forward.x, 0,camera.transform.forward.z);
    }
}
