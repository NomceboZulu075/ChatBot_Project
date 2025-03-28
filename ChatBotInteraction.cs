using System;
using Microsoft.SqlServer.Server;

namespace ChatBot_Project
{
    public class ChatBotInteraction
    {

        //Declaring a global variable reponse handler to access the response handler class
        private ResponseHandler responseHandler;

        public ChatBotInteraction()
        {
            //Assigning variable responseHandler to responsehandler constructor so that all methods from that class are accessible
            responseHandler = new ResponseHandler();

            //Calling the method BegibnChat inside the constructor
            BeginChat();

        }//end of constructor

        //Creating a method to begin the program
        private void BeginChat()
        {
            //Declaring variable to store the bot's name and the user's name
            string userName = "You";
            string chatBotName = "CyberBot";

            //Displaying the welcome message
            Console.WriteLine("Hello fellow South African!");

            //Asking the user for their name
            Console.WriteLine($"{chatBotName}: What is your name");
            Console.Write(userName + ": ");
            userName = Console.ReadLine();

            //While loop to continue the chat until the user stops the program
            while (true)
            {
                Console.WriteLine($"{chatBotName}: How may I be of assistance to you {userName}?");
                Console.Write(userName + ": ");
                string userInput = Console.ReadLine(); //Declaring and assigning a variable userInput

                if (userInput.ToLower() == "exit")
                {
                    Console.WriteLine($"{chatBotName}: Goodbye {userName}!");
                    break;
                }

                string chatBotResponse = responseHandler.GetResponse(userInput);
                Console.WriteLine($"{chatBotName}: {chatBotResponse}");
            }


        }


    }//end of class
}//end of namespace