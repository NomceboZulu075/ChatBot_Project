using System;
using System.IO;
using System.Media;

namespace ChatBot_Project
{
    public class VoiceGreeting
    {
        public VoiceGreeting()
        {
            //Declaring a string variable 
            string playSound = "Voice_Greeting.wav";

            //If statement
            if (File.Exists(playSound))
            {
                SoundPlayer chatBotAudio = new SoundPlayer(playSound);
                chatBotAudio.Play();

            }
            else
            {
                Console.WriteLine("Sorry! File does not exist.");
           }
            
        }
    }
}