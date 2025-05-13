using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatBot_Project
{
    //Changing the internal keyword to public keyword
    public class keywordRecognition
    {
        // Dictionary to store cybersecurity keywords and associated responses
        private Dictionary<string, string> keywordResponses;

        //A constructor to initialize keyword-response dictionary
        public keywordRecognition()
        {
            keywordResponses = new Dictionary<string, string>()
            {
                { "password", "Ensure your passwords are strong an unique" },
                { "password security", "" },
                { "scam", "Be careful when sharing your personal information online. Always look out for phishing scams!" },
                { "privacy", "Adjust your privacy settings regularly and avoid oversharing on social media." },
                { "phishing", "" },
                { "", "" },
                { "", "" },
                { "", "" },
                { "", "" },

            };
        }//end of constructor

        //Creating a method to to recognize cybersecurity-related keywords from the input of the user
        public void recognizeKeywords(string userInput)
        {
            bool found = false; //This checks if a keyword match is found

            //A foreach loop to look through predefined keywords and check if they exist in the user's input
            foreach (string keyword in keywordResponses.Keys)
            {
                //Using StringComparison.OrdinalIgnoreCase to use case-insensitive comparison and to detect keyword presence within larger phrases
                //An if statement to check if the user inpout contains a keyword, and if found, a message will be displayed
                if (userInput.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Keyword Detected: '{keyword}'"); // Informs user of detected keyword
                    Console.WriteLine($"Cybersecurity Tip: {keywordResponses[keyword]}"); // Displays relevant cybersecurity tip
                    found = true;
                    break;
                }//end of if-statement
            }//end of foreach loop

            //An if statement if there is no keyword match is found, and guidance is provided for the user
            if (!found)
                {
                    Console.WriteLine("No cybersecurity-related keywords detected!");
                    Console.WriteLine("Try asking about password safety, scams, or privacy");
                }//end of if-statement
        }//end of method recognizeKeywords
    }//end of keywordRecognition class
}//end of namespace