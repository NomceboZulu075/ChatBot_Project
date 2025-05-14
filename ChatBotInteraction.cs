using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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

        //Dictionary to store cybersecurity keywords and associated responses
        private Dictionary<string, string> keywordResponses = new Dictionary<string, string>
        {
            { "password", "Ensure your passwords are strong and unique. Use two-factor authentication for added security."},
            { "scam", "Be cautious when sharing personal information online. Look out for phishing scams!"},
            { "privacy", "Adjust your privacy settings regularly and avoid oversharing on social media."},
            { "phishing", "gfgfgfgfgfgfgfgdfddgdfgdgdgdgdgdgdfg" }

        };

        //List to store conversation memory for recall functionality
        private List<string> conversationMemory;

        // File path to store conversation history
        private string memoryFilePath = "MemoryRecallFile.txt";


        public ChatBotInteraction()
        {
            //Assigning variable responseHandler to responsehandler constructor so that all methods from that class are accessible
            responseHandler = new ResponseHandler();

            //Calling the method BeginChat inside the constructor
            BeginChat();

            string fullPath = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(fullPath, "MemoryRecallFile.txt");

            // Initializing memory storage and loading previous chats if available
            conversationMemory = new List<string>();
            LoadMemory(path);

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
            typingEffect("  Welcome to the South African CyberSecurity Awareness Chatbot!", ConsoleColor.DarkCyan);
            Console.WriteLine("  **************************************************************");

            //Telling the user the purpose of this ChatBot

            typingEffect(" Here to help you spot phishing emails, craft uncrackable passwords and browse the web like a pro. \n ", ConsoleColor.DarkCyan);


            //Asking the user for their name
            typingEffect($" {chatBotName}: Hello! My name is CyberBot.", ConsoleColor.DarkGray);
            typingEffect($" {chatBotName}: What is your name? ", ConsoleColor.DarkGray);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(userName + ": "); //Response of the user will be here
            userName = Console.ReadLine();
            Console.ResetColor(); //Colour resets back to white

            //

            //Declaring a variable to store the regex pattern 
            string pattern = @"^[a-zA-Z ]";

            //Creating a while loop to handle an error for when the user dos not type anything on the field or if a user uses numbers and characters

            while (string.IsNullOrEmpty(userName) || !Regex.IsMatch(userName, pattern))
            {
                typingEffect($" {chatBotName}: Please your name!! Your name must not contain any numbers of charecters ", ConsoleColor.Red);

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("You: "); //Response of the user will be here
                userName = Console.ReadLine();
                Console.ResetColor(); //Colour resets back to white
            }

            //Asking the user how they are feeling today
            typingEffect($" {chatBotName}: How are you feeling today {userName}?", ConsoleColor.DarkGray);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(userName + ": ");
            string userFeeling = Console.ReadLine(); //Declaring and assigning a variable userInput

            /*
             * An if-else statement after the user has entered how they are feeling, if they are good,
             * an acceptable message will be displayed, if they are not doing good, an acceptable message will be displayed 
             */
            if (userFeeling.Contains("good") || userFeeling.Contains("great") || userFeeling.Contains("happy"))
            {
                typingEffect($"{chatBotName}: I'm glad to hear that {userName}!", ConsoleColor.DarkGray);
            }
            else if (userFeeling.Contains("sad") || userFeeling.Contains("not good") || userFeeling.Contains("bad"))
            {
                typingEffect($"{chatBotName}: I'm sorry to hear that {userName}. If you need a distraction, I can share some cybersecurity.", ConsoleColor.DarkGray);
            }
            else if (userFeeling.Contains("tired") || userFeeling.Contains("stressed"))
            {
                typingEffect($"{chatBotName}: Take a break and relax {userName}! Also, remember to stay safe from cyber threats.", ConsoleColor.DarkGray);
            }
            else
            {
                typingEffect($"{chatBotName}: I see! If you'd like, I can share some security tips or fun facts.", ConsoleColor.DarkGray);
            }



            //While loop to continue the chat until the user stops the program
            while (true)
            {

                //Asking the user how the ChatBot should assist them 
                typingEffect($" {chatBotName}: So, how may I be of assistance to you today {userName}?", ConsoleColor.DarkGray);
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write(userName + ": ");
                string userInput = Console.ReadLine(); //Declaring and assigning a variable userInput


                //An if statement for when the user types out exit, the ChatBot will say "Goodbye" and the program will stop
                if (userInput.ToLower() == "exit")
                {
                    typingEffect($" {chatBotName}: Goodbye {userName}!", ConsoleColor.DarkGray);
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------------- ");
                    break;
                }//end of if statement

                if (conversationMemory == null)
                {
                    conversationMemory = new List<string>();
                }


                conversationMemory.Add(userInput);
                SaveMemory();


                string chatBotResponse = responseHandler.GetResponse(userInput);
                typingEffect($" {chatBotName}: {chatBotResponse}", ConsoleColor.DarkGray);


                //Section for Part 2, keyword recognition
                bool foundKeyword = false;

                //A foreach loop to look through predefined keywords and check if they exist in the user's input
                foreach (var keyword in keywordResponses.Keys)
                {

                    //An if statement to check if the user inpout contains a keyword, and if found, a message will be displayed
                    if (userInput.Contains(keyword))
                    {
                        Console.WriteLine($"Keyword Detected: '{keyword}'"); // Informs user of detected keyword

                        Console.WriteLine($"Cybersecurity Tip: {keywordResponses[keyword]}"); // Displays relevant cybersecurity tip

                        foundKeyword = true;
                        break;
                    }//end of if-statement
                }//end of foreach loop

                //A message will be displayed if no keyword or topic matches, an if statement will be used
                if (!foundKeyword)
                {
                    Console.WriteLine(RecallPreviousMessage(userInput));
                   
                }//end of if-statement
            }// end of while loop
        }// end of BeginChat method

        /*A method to retrieve a random tip for a given cybersecurity topic
        private string getRandomSecurityTip(string topic)
        {
            if (securityTips.ContainsKey(topic))
            {
                Random random = new Random();
                return securityTips[topic][random.Next(securityTips[topic].Count)];
            }
            else
            {
                return "I don't have tips for that topic, but always prioritize cybersecurity!";
            }//end of if-else 

        }//end of method getRandomSecurityTip
        */
        //Searches memory to recall previous user inquiries related to the current input
        private string RecallPreviousMessage(string userInput)
        {
            //A foreach loop to iterate through all saved user inputs stored in the conversationMemory list
            //Chatbot will search whether the current user input matches any previously recorded message

            if (string.IsNullOrWhiteSpace(userInput))
            {
                return "I'm not sure about that topic, but always prioritize online security!";
            }

            foreach (string pastMessage in conversationMemory)
            {
                //If-statement to 
                if(pastMessage.Contains(userInput))
                {
                    
                    return $"Great! I'll remember that you're interested in {userInput}. It is an important part of staying safe online.";
                }//end of if statement 
            }//end of foreach loop 

            return "";

        }//end of recall previous memory method 

        //Saving conversation history to a file for persistence across sessions
        private void SaveMemory()
        {
            File.WriteAllLines(memoryFilePath, conversationMemory);
        }//end of save memory method

        //Loading conversation history from file if it exists
        private List<string> LoadMemory(string path) 
        {
            //Checking if the file exists using an if statement 
            if(File.Exists(memoryFilePath))
            {
                return new List<string>(File.ReadAllLines(memoryFilePath));
            }//end of if statement

            else
            {
                //Creating the text file 
                File.CreateText(path);
                return new List<string>();
            }
        }//end of load memory method 




        //Creating a typing effect method

        private void typingEffect(string response, ConsoleColor color)
        {

            //Declaring a variable to store the speed 
            int speed = 40;

            //Declare the foreground colour 
            Console.ForegroundColor = color;

            //A for each loop that will loop through the string

            foreach (char character in response)
            {

                //Displaying each character
                Console.Write(character);

                //Delay the display of each character
                System.Threading.Thread.Sleep(speed);

            }//end of foreach loop
            Console.WriteLine();
            Console.ResetColor();
        }//end of typing effect method
    }//end of class
}//end of namespace