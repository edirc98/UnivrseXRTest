using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI; 

public class NPCChatManager : MonoBehaviour
{
    #region PARAMETERS & VARIABLES
    public string TestMessage = null; 
    //OpenAI Api constructor
    private OpenAIApi openAI = new OpenAIApi();

    //List of request messages
    private List<ChatMessage> messages = new List<ChatMessage>();
    #endregion
    #region START
    void Start()
    {
        MakeRequest(TestMessage); 
    }
    #endregion
    #region UPDATE
    void Update()
    {
        
    }
    #endregion
    #region OPEN AI API FUNCTIONS
    public async void MakeRequest(string newText)
    {
        //Create message 
        ChatMessage newMessage =  CreateChatMessage("user", newText);
        Debug.Log("ROLE: " + newMessage.Role);
        Debug.Log("MESSAGE:" + newMessage.Content);
        messages.Add(newMessage);

        //Create Request 
        CreateChatCompletionRequest request = new CreateChatCompletionRequest();
        request.Model = "gpt-3.5-turbo"; 
        request.Messages = messages;

        //Send Request and await response
        var response = await openAI.CreateChatCompletion(request);
        
        //Response comes back with a list of choices that can be selected
        if(response.Choices != null && response.Choices.Count > 0)
        {
            ChatMessage responseMessage = response.Choices[0].Message;
            messages.Add(responseMessage);

            Debug.Log("ROLE: "+ responseMessage.Role); 
            Debug.Log("RESPONSE:" + responseMessage.Content); 
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
}
