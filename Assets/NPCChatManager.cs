using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using OpenAI;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class NPCChatManager : MonoBehaviour
{

    #region NPC TRAITS
    [Header("NPC CONFIGURATION")]
    [TextArea(5, 20)]
    public string NPC_Personality;
    [TextArea(5, 20)]
    public string NPC_Scene;
    public int MaxResponseTokens = 32; 
    #endregion

    #region PARAMETERS & VARIABLES
    public string TestMessage = null; 
    //OpenAI Api constructor
    private OpenAIApi openAI = new OpenAIApi();

    //List of request messages
    private List<ChatMessage> messages = new List<ChatMessage>();

    public TimeManager timeManager = new TimeManager();

    [SerializeField] private TTSPolly Polly; 
    #endregion


    #region EVENTS
    [System.Serializable]
    public class OnResponseEvent : UnityEvent<string> { }

    public OnResponseEvent OnResponse; 
    #endregion

    #region AWAKE
    private void Awake()
    {
        Polly = GameObject.Find("TTS Polly").GetComponent<TTSPolly>();
    }
    #endregion

    #region START
    void Start()
    {
        //MakeRequest(TestMessage); 
    }
    #endregion

    #region UPDATE
    void Update()
    {
        
    }
    #endregion

    #region OPEN AI API FUNCTIONS
    public async void MakeRequest(string userMessageText)
    {
        float startTime = Time.time;
        //Create message 
        ChatMessage newMessage =  CreateChatMessage("user", userMessageText);
        Debug.Log("ROLE: " + newMessage.Role);
        Debug.Log("MESSAGE:" + newMessage.Content);

        //Check if this is the first message, if true, add the instructions before the message
        if (messages.Count == 0) newMessage.Content = SetNPCInstructions() + "\n" + userMessageText;
        messages.Add(newMessage);

        //Create Request 
        CreateChatCompletionRequest request = new CreateChatCompletionRequest();
        request.Model = "gpt-3.5-turbo"; 
        request.Messages = messages;
        request.MaxTokens = MaxResponseTokens; 

        //Send Request and await response
        var response = await openAI.CreateChatCompletion(request);
        
        //Response comes back with a list of choices that can be selected
        if(response.Choices != null && response.Choices.Count > 0)
        {
            ChatMessage responseMessage = response.Choices[0].Message;
            messages.Add(responseMessage);

            Debug.Log("ROLE: "+ responseMessage.Role); 
            Debug.Log("RESPONSE:" + responseMessage.Content);

            float endTime = Time.time;
            timeManager.AddToTimer(endTime - startTime);
            timeManager.AddTimeSection("Chat GPT Response", startTime, endTime);
            timeManager.PrintLastSection();

            Polly.MakeRequest(responseMessage.Content); 
        }
        
    }

    private ChatMessage CreateChatMessage(string role, string content)
    {
        return new ChatMessage()
        {
            Role = role,
            Content = content
        };

    }
    #endregion

    #region CUSTOM FUNCTIONS
    public string SetNPCInstructions() {

        string instruction = "You are a samurai warrior who lives in the Edo period and you are going to provide information about that time and answer the questions they ask you.\n"
                           + "You have to answer the questions using the information about your personality and the scene around you described below.\n"
                           + "Do not invent or create answers that are not mentioned or described in that information.\n"
                           + "Don't break character. Don´t ever mention that you're an AI model, computer program or video game character.\n"
                           //+ "You must answer in less than " + MaxResponseTokens + "words.\n"
                           + "Here is your presonnality: \n" + NPC_Personality + "\n"
                           + "Here is the information of your sourroundign scene: \n" + NPC_Scene + "\n"; 

        return instruction; 
    }

    
    #endregion
}
