using System;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.SqlServer.Server;

namespace ChatBot_Project
{
    //Changing the internal keyword to public keyword
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
            Console.WriteLine("  **************************************************************");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("  Welcome to the South African CyberSecurity Awareness Chatbot!");
            Console.ResetColor();
            Console.WriteLine("  **************************************************************");

            //Telling the user the purpose of this ChatBot
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.WriteLine(" Here to help you spot phishing emails, craft uncrackable passwords and browse the web like a pro. \n ");
            Console.ResetColor();


            //Asking the user for their name
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($" {chatBotName}: Hello! My name is CyberBot.");
            Console.WriteLine($" {chatBotName}: What is your name? ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(userName + ": "); //Response of the user will be here
            userName = Console.ReadLine();
            Console.ResetColor(); //Colour resets back to white

            //Declaring a variable to store the regex pattern 
            string pattern = @"^[a-zA-Z ]";

            //Creating a while loop to handle an error for when the user dos not type anything on the field or if a user uses numbers and characters

            while (string.IsNullOrEmpty(userName) || !Regex.IsMatch(userName, pattern)) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" {chatBotName}: Please your name!! Your name must not contain any numbers of charecters ");
                Console.ResetColor() ;

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("You: "); //Response of the user will be here
                userName = Console.ReadLine();
                Console.ResetColor(); //Colour resets back to white
            }

            //Asking the user how they are feeling today
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($" {chatBotName}: How are you feeling today {userName}?");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(userName + ": ");
            string userFeeling = Console.ReadLine(); //Declaring and assigning a variable userInput
            Console.ForegroundColor = ConsoleColor.DarkGray;

            /*
             * An if-else statement after the user has entered how they are feeling, if they are good,
             * an acceptable message will be displayed, if they are not doing good, an acceptable message will be displayed 
             */
            if (userFeeling.Contains("good") || userFeeling.Contains("great") || userFeeling.Contains("happy"))
            {
                Console.WriteLine($"{chatBotName}: I'm glad to hear that {userName}!");
            }
            else if (userFeeling.Contains("sad") || userFeeling.Contains("not good") || userFeeling.Contains("bad"))
            {
                Console.WriteLine($"{chatBotName}: I'm sorry to hear that {userName}. If you need a distraction, I can share some cybersecurity.");
            }
            else if (userFeeling.Contains("tired") || userFeeling.Contains("stressed"))
            {
                Console.WriteLine($"{chatBotName}: Take a break and relax {userName}! Also, remember to stay safe from cyber threats.");
            }
            else
            {
                Console.WriteLine($"{chatBotName}: I see! If you'd like, I can share some security tips or fun facts.");
            }



            //While loop to continue the chat until the user stops the program
            while (true)
            {

                //Asking the user how the ChatBot should assist them 
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($" {chatBotName}: So, how may I be of assistance to you today {userName}?");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write(userName + ": ");
                string userInput = Console.ReadLine(); //Declaring and assigning a variable userInput
                Console.ForegroundColor = ConsoleColor.DarkGray;


                //An if statement for when the user types out exit, the ChatBot will say "Goodbye" and the program will stop
                if (userInput.ToLower() == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($" {chatBotName}: Goodbye {userName}!");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------------- ");
                    break;
                }//end of if statement

                
                string chatBotResponse = responseHandler.GetResponse(userInput);
                Console.WriteLine($" {chatBotName}: {chatBotResponse}");

            }// end of while loop


        }// end of BeginChat method
    }//end of class
}//end of namespace