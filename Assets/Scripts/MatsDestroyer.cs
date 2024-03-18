using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatsDestroyer : MonoBehaviour
{
    public float timeBetweenDestroy = 5.0f;

    private bool destroyingChild = false; 
    // Update is called once per frame
    void Update()
    {
        if(transform.childCount > 0)
        {
            if (!destroyingChild)
            {
                destroyingChild = true;
                StartCoroutine("DestroyChildMat");
            }
        }
    }

    private IEnumerator DestroyChildMat()
    {
        yield return new WaitForSeconds(timeBetweenDestroy);
        Destroy(transform.GetChild(0).gameObject);
        destroyingChild = false; 
    }
}
