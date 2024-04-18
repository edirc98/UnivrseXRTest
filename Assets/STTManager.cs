using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Voice;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class STTManager : MonoBehaviour
{
    #region PARAMETERS & VARIABLES
    //VTT (Speech to text) componentç
    [SerializeField] private AppVoiceExperience NPCVoice;
    [SerializeField] private NPCChatManager ChatManager;
    private bool CanTalk = true;
    private bool IsTalking = false;

    float start = 0, end = 0;
    private EventSystem eventSystem; 
    #endregion

    #region AWAKE
    private void Awake()
    {
        NPCVoice = GetComponent<AppVoiceExperience>();
        ChatManager = transform.parent.GetComponent<NPCChatManager>(); 
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>(); 
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
        
    }
    #endregion

    #region CUSTOM FUNCTIONS
    public void DebugString(string text)
    {
        Debug.Log("You said: " + text);
        //IsTalking = false; 
        //CanTalk = true; 
    }

    public void ActivateVoiceRecognition()
    {
        //IsTalking = true;
        //CanTalk = false;
        Debug.Log("TALK:");
        NPCVoice.Activate();
    }

    public void DeselectTalkButton()
    {
        eventSystem.SetSelectedGameObject(null); 
    }


    public void setStartTime()
    {
        start = Time.time;
    }
    public void setEndTime()
    {
        end = Time.time;
    }

    public void SetTimeSection()
    {
        ChatManager.timeManager.AddTimeSection("Speach To Text", start, end);
        ChatManager.timeManager.AddToTimer(end - start);
        ChatManager.timeManager.PrintLastSection();
    }
    #endregion
}
