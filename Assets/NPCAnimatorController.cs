using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Voice;

public class NPCAnimatorController : MonoBehaviour
{
    #region PARAMETERS & VARIABLES
    [Header("NPC Animator")]
    [SerializeField] private Animator NPC_Animator;

    [Header("NPC Interaction radius")]
    [SerializeField] private float NPC_GetAtentionRadius = 4.0f;
    [SerializeField] private float NPC_PresentationRadius = 1.5f;
    [Header("Get User Atention parameters")]
    [SerializeField] private float timeBetweenGetAttention = 30.0f;
    private bool needsToBeAttracted = true;
    private bool AttractionGet = false;

    [Header("Chat & Voice Manager")]
    [SerializeField] private NPCChatManager ChatManager;

    private GameObject user = null; 
    #endregion

    #region AWAKE
    private void Awake()
    {
        NPC_Animator = GetComponent<Animator>();
        ChatManager = GetComponent<NPCChatManager>();

        user = GameObject.FindGameObjectWithTag("Player"); 

    }
    #endregion

    #region START
    void Start()
    {

    }
    #endregion

    #region UPDATE
    void Update()
    {
        //User cross the first radius, call his atention to get closer to the NPC
        if(Vector3.Distance(transform.position,user.transform.position) < NPC_GetAtentionRadius && needsToBeAttracted && !AttractionGet)
        {
            
            Debug.Log("HEY YOU, COME HERE");
            ChatManager.MakeRequest("Tell me to get closer");
            NPC_Animator.SetTrigger("HandWave");
            
            needsToBeAttracted = false;
            if (!AttractionGet)
            {
                StartCoroutine(WaitForNextAttraction()); 
            }
        }
        if(Vector3.Distance(transform.position, user.transform.position) < NPC_PresentationRadius && !AttractionGet)
        {
            AttractionGet = true;
            needsToBeAttracted = false; 
            Debug.Log("HI I AM...");
            ChatManager.MakeRequest("Present yourself");
            StopCoroutine(WaitForNextAttraction()); 
        }
    }
    #endregion

    #region FUNCTIONS
    private IEnumerator WaitForNextAttraction()
    {
        yield return new WaitForSeconds(timeBetweenGetAttention);
        needsToBeAttracted = true; 
    }
    #endregion

    #region GIZMOS
    private void OnDrawGizmos()
    {

        //Trigger Spheres
        Gizmos.color = Color.cyan; 
        Gizmos.DrawWireSphere(transform.position, NPC_GetAtentionRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, NPC_PresentationRadius);

        //Player traking
        Gizmos.color = Color.red;
        if(user != null) Gizmos.DrawLine(transform.position, user.transform.position); 
    }
    #endregion
}
