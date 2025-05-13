using System;
using System.IO;
using System.Media;

namespace ChatBot_Project
{
    //Importing System.Media and System

    using System.Media;
    using System;
    //Changing the internal keyword to public keyword
    public class VoiceGreeting
    {
        public VoiceGreeting()
        {

            //Getting the location of the sound file 
            string soundLocation = AppDomain.CurrentDomain.BaseDirectory;

            //Checking if it is getting the Directory
            Console.WriteLine(soundLocation);

            //Replacing the bin\debug so it can get the audio
            string updatedPath = soundLocation.Replace("bin\\Debug\\", "");

            //Combining the wav name as sound.wav with the updated path
            string fullPath = Path.Combine(updatedPath, "Voice_Greeting.wav");

            //Passing to the method playWav
            playWav(fullPath);

        } //end of constructor


        //Creating a method to play the .wav sound file
        private void playWav(string fullPath)
        {
            //Try and catch

            try
            {
                //Playing the sound 
                using (SoundPlayer soundPlayer = new SoundPlayer(fullPath))
                {
                    //This is to play the sound till the end
                    soundPlayer.PlaySync();
                }

            } catch (Exception error ) {
                //Displaying the error message
                Console.WriteLine( error.Message);

            }//end of try and catch 

        }//end of playWav method
    }//end of class 
}//end of namespace