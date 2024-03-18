using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatSpawner : MonoBehaviour
{

    public GameObject mat;
    public Transform spawnTransform;
    public Transform spawnedMatsParent; 
    private bool canSpawn = false;

    private GameObject newMat; 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("spawnNewMat");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mat"))
        {
            Debug.Log("MAT Exited Spawner");
            StartCoroutine("spawnNewMat");
            //newMat.transform.parent = spawnedMatsParent;
            newMat.tag = "SpawnedMat";
        }
    }


    private IEnumerator spawnNewMat()
    {
        yield return new WaitForSeconds(1.0f);
        newMat = Instantiate(mat, spawnTransform.position, spawnTransform.rotation, transform);
        newMat.transform.localScale = spawnTransform.localScale;
        
        
    }
}
