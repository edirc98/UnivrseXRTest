using OpenAI;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Samples.Whisper
{
    public class Whisper : MonoBehaviour
    {
        [SerializeField] private NPCChatManager chatManager;
        [SerializeField] private Button recordButton1;
        [SerializeField] private Button recordButton2;
        [SerializeField] private Image progressBar;
        [SerializeField] private Text message;
        [SerializeField] private Dropdown dropdown;

        private float start = 0, end = 0; 
        
        private readonly string fileName = "output.wav";
        public readonly int duration = 5;
        
        private AudioClip clip;
        private bool isRecording;
        private float time;
        private OpenAIApi openai = new OpenAIApi();

        //#region EVENTS
        //[System.Serializable]
        //public class OnTranscriptionCompleted : UnityEvent<string> { }

        //public OnTranscriptionCompleted OnTranscriptionComplete;
        //#endregion

        private void Start()
        {
            #if UNITY_WEBGL && !UNITY_EDITOR
            dropdown.options.Add(new Dropdown.OptionData("Microphone not supported on WebGL"));
            #else
            chatManager = GameObject.Find("NPC_Avatar").GetComponent<NPCChatManager>();
            foreach (var device in Microphone.devices)
            {
                dropdown.options.Add(new Dropdown.OptionData(device));
            }
            recordButton1.onClick.AddListener(StartRecording);
            recordButton2.onClick.AddListener(StartRecording);
            dropdown.onValueChanged.AddListener(ChangeMicrophone);
            
            var index = PlayerPrefs.GetInt("user-mic-device-index");
            dropdown.SetValueWithoutNotify(index);
            #endif
        }

        private void ChangeMicrophone(int index)
        {
            PlayerPrefs.SetInt("user-mic-device-index", index);
        }
        
        private void StartRecording()
        {
            isRecording = true;
            recordButton1.enabled = false;
            recordButton2.enabled = false;

            var index = PlayerPrefs.GetInt("user-mic-device-index");
            
            #if !UNITY_WEBGL
            clip = Microphone.Start(dropdown.options[index].text, false, duration, 44100);
            #endif
        }

        private async void EndRecording()
        {
            start = Time.time;
            message.text = "Transcripting...";
            
            #if !UNITY_WEBGL
            Microphone.End(null);
            #endif
            
            byte[] data = SaveWav.Save(fileName, clip);
            
            var req = new CreateAudioTranscriptionsRequest
            {
                FileData = new FileData() {Data = data, Name = "audio.wav"},
                Model = "whisper-1",
                Language = "es"
            };
            Debug.Log("Detected Request Lenguage:" + req.Language); 
            var res = await openai.CreateAudioTranscription(req);

            end = Time.time;
            chatManager.MakeRequest(res.Text);
            chatManager.timeManager.AddTimeSection("Speach To Text", start, end);
            chatManager.timeManager.AddToTimer(end - start);
            chatManager.timeManager.PrintLastSection(); 

            progressBar.fillAmount = 0;
            message.text = res.Text;
            recordButton1.enabled = true;
            recordButton2.enabled = true;
             
        }

        private void Update()
        {
            if (isRecording)
            {
                time += Time.deltaTime;
                progressBar.fillAmount = time / duration;
                
                if (time >= duration)
                {
                    time = 0;
                    isRecording = false;
                    EndRecording();
                }
            }
        }
    }
}
