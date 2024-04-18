using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon.Polly;
using Amazon.Runtime;
using Amazon;
using Amazon.Polly.Model;
using System;
using System.IO;
using UnityEditor.PackageManager;
using UnityEngine.Networking;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class TTSPolly : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private NPCChatManager chatManager; 
    private BasicAWSCredentials credentials;
    private AmazonPollyClient client; 

    void Start()
    {
        chatManager = GameObject.Find("NPC_Avatar").GetComponent<NPCChatManager>();
        audioSource = GetComponent<AudioSource>();
        //Create credentials for the client
        credentials = new BasicAWSCredentials("", "");
        client = new AmazonPollyClient(credentials,RegionEndpoint.EUWest2);

        //PollyRequest("Testing Amazon Polly in Unity, this looks promising"); 
  
    }

    public void MakeRequest(string text)
    {
        PollyRequest(text); 
    }

    private async void PollyRequest(string Text)
    {
        Debug.Log("Request to Polly with: " + Text); 
        SynthesizeSpeechRequest speechRequest = new SynthesizeSpeechRequest()
        {
            Text = Text,
            Engine = Engine.Neural,
            VoiceId = VoiceId.Arthur,
            OutputFormat = OutputFormat.Mp3
        };

        var res = await client.SynthesizeSpeechAsync(speechRequest);
        Debug.Log("Response to Polly Request recived");
        SaveToFile(res.AudioStream);

        using (var www = UnityWebRequestMultimedia.GetAudioClip($"{Application.persistentDataPath}/audio.mp3", AudioType.MPEG))
        {
            var operation = www.SendWebRequest(); 
            while(!operation.isDone) await Task.Yield();

            AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
            audioSource.clip = clip;
            audioSource.Play();
        }
    }


    private void SaveToFile(Stream stream)
    {
        using (var fileStream = new FileStream($"{Application.persistentDataPath}/audio.mp3", FileMode.Create))
        {
            byte[] buffer = new byte[8*1024];
            int bytesRead = 0;
            while ((bytesRead = stream.Read(buffer,0,buffer.Length))>0) 
            {
                fileStream.Write(buffer,0, bytesRead);
            }
        }
    }
}
