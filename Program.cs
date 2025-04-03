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

        }
    }//end of class 
}
