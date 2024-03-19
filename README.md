# UnivrseXRTest

## Implementation of an AI-driven NPC 
 The NPC has been integrated into a self-made already existing VR project of a Japanese House with a garden environment used to test some basic VR interactions and mechanics. 
## NPC functionalities Implemented: 
1. Response generation
   - Implemented using OpenAI API for Unity. [(API GitHub Page)](https://github.com/hexthedev/OpenAi-Api-Unity/)
   - Response generation is based on an initial prompt that provides basic instructions to the text model and a personality as well as a scene description. 
> [!IMPORTANT]
> To have access to the Open AI API is needed to set up an account and the corresponding authentication file. It is well explained in the  [(API GitHub Page)](https://github.com/hexthedev/OpenAi-Api-Unity/) under the Authenticate section.
2. Speech to Text (STT) and Text to Speech (TTS)
   - Both STT and TTS have been implemented using Meta Voice SDK.
3. LipSync
   - Lipsync implemented with the OVR LipSync package, that take advantage of the blend shapes defined in the NPC model from the Microsoft Rocketbox library.  [(RocketBox)](https://github.com/microsoft/Microsoft-Rocketbox) 
## Application flow
 Once you enter the VR scenario the NPC will be standing in your right. 
 
 To create a proactive NPC, when the user gets closer, the NPC tries to get the user's attention telling the user to get closer and performing a hand wave animation. Once the user is closer enough the NPC starts a self-presentation. 
 This can be seen in the editor with the 2 blue spheres, delimiting the distances to attract the user (big sphere in cian) and to do the presentation (sphere in blue). 

 In case the user is not close enough to the blue sphere, the NPC will keep trying to get the user's attention every 30 seconds. 

 Also, spacialized audio can be used to guide the users, to provide more cues from who is calling their attention and from where it is coming. 

 In front of the NPC there is a button "Press to Talk" that can be activated with the right ray interaction, by pressing the right trigger button in the VR controller. 

 This button activates voice recognition and lets you ask or talk to the NPC. The color of the button indicates when the system is listening (in red) and when can be pressed (in green) to talk.
 As an improvement, the button can be turned into a physical button to avoid the use of a ray interactor and provide feedback to the user in different ways, instead of usign color. 

 > [!NOTE]
 > Keeping in mind the situation where there may be more than one user at a time and they are chatting with each other, the NPC gets their attention in a paced way, so once all present users are closer, they can start a conversation.
 > The button is there to help the user know when to talk and when it is being listened, to avoid confusion and frustration when trying to talk to the NPC. It is true that this limits the NPC usage for the users, as they have to speak one by one and makes the NPC a little bit less human. 

 The supported language for the conversation is English. Spanish has been tried but voice recognition results are quite bad. 

 The latency between question and answer is about 3 to 5 seconds, depending on how long the question and answer are. This latency can be minimized using better models for Speech-to-text, text generation and Text-to-speech parts. 
 In this project, the wait time of silence to start processing the captured voice has been reduced. By default is 2 seconds and has been reduced to 1. 
 
 ## TECHNICAL DETAILS
 Project developed in Unity 2022.3.13f1
 
 Base VR interactions were developed with XR Interaction Toolkit. 
 
 Project tested with Oculus / Meta Quest 1 and Oculus Link with Cable. 
