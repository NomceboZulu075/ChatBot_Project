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

            //Creating an instance of a class for ChatBot Interaction that handles the interactions between the user and the chatbot, this is where keyword recognition will occur 
            new ChatBotInteraction() { };

            //************************* PART 2 *********************
            //1. 

            //2. Creating a class that will have three methods that will:
            /*
             * 1. Check and create file
             * 2. Get what is stored in the text file 
             * 3. Add or write into the text file
             */

            //Instatiating the checkTextFile 
            CheckTextFile checkExist = new CheckTextFile();

            //Using the object called checkExist, I will use this object to get the methods in the checkTextFile class

            //First class to call:
            checkExist.checkFile();

            //Second class to call:
            List<string> memory = checkExist.returnMemory();

            //Using a foreach to list all memory values
            foreach (string check in memory)
            {
                Console.WriteLine(check);
            }//end of foreach loop

            //Prompting the user and saving what is entered by the user in the MemoryRecallFile



            //Third class to call: 




        }
    }//end of class 
}
