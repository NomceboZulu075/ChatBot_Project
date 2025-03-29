using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Creating an instance of a class to handle the responses and the text file
            new DiplayLogo() { };

            //Creating an instance of a class to allow the voice greeting to play
            new VoiceGreeting() { };

            //Creating an instance of a class that handles the interactions 
            new ChatBotInteraction() { };

        }
    }
}
