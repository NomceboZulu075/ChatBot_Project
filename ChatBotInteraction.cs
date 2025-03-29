﻿using System;
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
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------- ");
            Console.WriteLine("  *********************************************");
            
            
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("  Hello ChatBot User!  ");
            Console.ResetColor();
            Console.WriteLine("  *********************************************");

            //Telling the user the purpose of this ChatBot
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(" Welcome to the South African CyberSecurity Awareness Chatbot!");
            Console.WriteLine(" Here to help you spot phishing emails, craft uncrackable passwords and browse the web like a pro. \n ");
            Console.ResetColor();


            //Asking the user for their name
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($" {chatBotName}: What is your name? ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(userName + ": "); //Response of the user will be here
            userName = Console.ReadLine();
            Console.ResetColor(); //Colour resets back to white


            //While loop to continue the chat until the user stops the program
            while (true)
            {
                //Asking the user how the ChatBot should assist them 
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($" {chatBotName}: How may I be of assistance to you {userName}?");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write(userName + ": ");
                string userInput = Console.ReadLine(); //Declaring and assigning a variable userInput
                Console.ForegroundColor = ConsoleColor.DarkGray;


                //An if statement for when the user types out exit, the ChatBot will say "Goodbye" and the program will stop
                if (userInput.ToLower() == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($" {chatBotName}: Goodbye {userName}!");
                    break;
                }//end of if statement

                string chatBotResponse = responseHandler.GetResponse(userInput);
                Console.WriteLine($" {chatBotName}: {chatBotResponse}");

            }// end of while loop


        }// end of BeginChat method
    }//end of class
}//end of namespace