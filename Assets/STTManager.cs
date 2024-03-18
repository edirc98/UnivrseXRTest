using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Voice;


public class STTManager : MonoBehaviour
{
    #region PARAMETERS & VARIABLES
    //VTT (Speech to text) componentç
    [SerializeField] private AppVoiceExperience STT;

    private bool CanTalk = true;
    private bool IsTalking = false; 
    #endregion

    #region AWAKE
    private void Awake()
    {
        STT = GetComponent<AppVoiceExperience>(); 
    }
    #endregion
    #region START
    void Start()
    {
        STT.VoiceEvents.OnFullTranscription.AddListener(DebugString);
    }
    #endregion

    #region UPDATE
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanTalk)
        {
            IsTalking = true;
            CanTalk = false; 
            Debug.Log("TALK...");
            STT.Activate();
        }
    }
    #endregion

    #region CUSTOM FUNCTIONS
    public void DebugString(string text)
    {
        Debug.Log("You said: " + text);
        IsTalking = false; 
        CanTalk = true; 
    }
    #endregion
}
