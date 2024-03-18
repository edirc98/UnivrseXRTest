using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Voice;


public class STTManager : MonoBehaviour
{
    #region PARAMETERS & VARIABLES
    //VTT (Speech to text) componentç
    [SerializeField] private AppVoiceExperience NPCVoice;
    [SerializeField] private NPCChatManager ChatManager;
    private bool CanTalk = true;
    private bool IsTalking = false; 
    #endregion

    #region AWAKE
    private void Awake()
    {
        NPCVoice = GetComponent<AppVoiceExperience>();
        ChatManager = transform.parent.GetComponent<NPCChatManager>(); 
    }
    #endregion
    #region START
    void Start()
    {
        NPCVoice.VoiceEvents.OnFullTranscription.AddListener(DebugString);
        NPCVoice.VoiceEvents.OnFullTranscription.AddListener(ChatManager.MakeRequest);
    }
    #endregion

    #region UPDATE
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanTalk)
        {
            IsTalking = true;
            CanTalk = false;
            Debug.Log("TALK:");
            NPCVoice.Activate();
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
