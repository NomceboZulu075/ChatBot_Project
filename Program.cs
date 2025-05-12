using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Project
{
    //Changing the internal keyword to public keyword
    public class Program
    {
        static void Main(string[] args)
        {
            //Creating an instance of a class for Voice Greeting to allow the voice greeting to play
            new VoiceGreeting() { };

            //Creating an instance of a class for Display Logo to display the ASCII logo design
            new DisplayLogo() { };

            //Creating an instance of a class for ChatBot Interaction that handles the interactions between the user and the chatbot 
            new ChatBotInteraction() { };

            //Creating a class that will have three methods that will:
            /*
             * 1. Check and create file
             * 2. Get what is stored in the text file 
             * 3. Add or write into the text file
             */
            
            checkTextFile checkExist = new checkTextFile();

        }
    }//end of class 
}
