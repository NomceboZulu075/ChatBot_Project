using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
        private Dictionary<string, string> keywordResponses = new Dictionary<string, string>()
        {
            {"password", "Ensure your passwords are strong and unique. Use two-factor authentication for added security."},
            {"scam", "Be cautious when sharing personal information online. Look out for phishing scams!"},
            {"privacy", "Adjust your privacy settings regularly and avoid oversharing on social media."}
    };

        // Dictionary containing multiple cybersecurity topics with different sets of tips
        private Dictionary<string, List<string>> securityTips;

        public ChatBotInteraction()
        {
            //Assigning variable responseHandler to responsehandler constructor so that all methods from that class are accessible
            responseHandler = new ResponseHandler();

            //Calling the method BegibnChat inside the constructor
            BeginChat();

            // Defining different cybersecurity categories with randomized tips
                securityTips["phishing"] = new List<string>(
                    "Be careful of emails requesting for personal information.|" +
                    "Verify the sender's email address before responding.|" +
                    "Look for spelling and grammatical errors in suspicious emails.|" +
                    "Be alert of urgent or threatening language in messages.".Split('|')),


                ["password"] = new List<string>(
                    "Use a mix of uppercase, lowercase, numbers, and symbols in your passwords.|" +
                    "Enable two-factor authentication whenever possible.|" +
                    "Avoid using personal information (like your name or birthdate) in passwords.|" +
                    "Use a password manager to keep track of complex passwords.".Split('|').ToList()
                    ),
                ["privacy"] = new List<string>(
                   "Adjust privacy settings on social media to limit who can view your information.|" +
                   "Be cautious about sharing sensitive data online, even in private messages.|" +
                   "Turn off location tracking on apps that don’t require it.|" +
                   "Regularly review what data apps collect and remove unnecessary permissions.".Split('|').ToList()

                    )
            };

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

                
                string chatBotResponse = responseHandler.GetResponse(userInput);
                typingEffect($" {chatBotName}: {chatBotResponse}", ConsoleColor.DarkGray);

                //Section for Part 2, random responses
                foreach (var topic in securityTips.Keys)
                {
                    if (userInput.ToLower().Contains(topic))
                    {
                        Console.WriteLine(getRandomSecurityTip(topic));
                        continue; //Continue to next input without having to check other keywords
                    }//end of if-statement
                }//end of foreach loop

                //Section for Part 2, keyword recognition
                bool foundKeyword = false;

                //A foreach loop to look through predefined keywords and check if they exist in the user's input
                foreach (var keyword in keywordResponses.Keys)
                {
                    //Using StringComparison.OrdinalIgnoreCase to use case-insensitive comparison and to detect keyword presence within larger phrases
                    //An if statement to check if the user inpout contains a keyword, and if found, a message will be displayed
                    if (userInput.ToLower().Contains(keyword))
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
                    Console.WriteLine("I'm not sure about that topic, but always prioritize online security!");
                }//end of if-statement
            }// end of while loop
        }// end of BeginChat method

        //A method to retrieve a random tip for a given cybersecurity topic
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



        //Creating a typing effect method

        private void typingEffect(string response, ConsoleColor color)
        {

            //Declaring a variable to store the speed 
            int speed = 60;

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