using System;
using System.IO;
using System.Media;

namespace ChatBot_Project
{
    //Changing the internal keyword to public keyword
    public class VoiceGreeting
    {
        public VoiceGreeting()
        {
            //Declaring a string variable 
            string playSound = "Voice_Greeting.wav";

            //If statement to handle error if the sound file is not found
            if (File.Exists(playSound))
            {
                SoundPlayer chatBotAudio = new SoundPlayer(playSound);
                chatBotAudio.Play();

            }
            else
            {
                Console.WriteLine("Sorry :( File does not exist.");
           }
            
        }//end of constructor 
    }//end of class 
}//end of namespace