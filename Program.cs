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

            //Creating a instance of a class to handle the responses and the text file
            new DiplayLogo() { };

            new VoiceGreeting() { };

            //Creating an instance of a class that handles the interactions 
            new ChatBotInteraction() { };
        }
    }
}
