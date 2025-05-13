using System;
using System.Collections.Generic;
using System.IO; //Required for file handling
using System.Linq;

namespace ChatBot_Project
{
    //Changing the internal keyword to public keyword
    public class KeywordRecognition
    {
        //This defined the text file path for storage
        private string filePath = "keywordResponses.txt"; 

        //A constructor to initialize keyword-response dictionary
        public KeywordRecognition()
        {
            ensureResponseFileExists(); // Make sure response file is available

        }//end of constructor

        //A method load responses from text file into dictionary
        private void ensureResponseFileExists()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Keyword responses text file is missing! Creating a new one...");
                File.WriteAllText(filePath, "password: Ensure your passwords are strong and unique. Use two-factor authentication for added security. \nscam: Be cautious when sharing personal information online. Look out for phishing scams! \nprivacy: Adjust your privacy settings regularly and avoid oversharing on social media.");
            }//end of if statement
        }//end of method ensureResponseFileExists

        //Creating a method to to recognize cybersecurity-related keywords from the input of the user
        public void recognizeKeywords(string userInput)
        {
            // Dictionary to store cybersecurity keywords and associated responses
            Dictionary<string, string> keywordResponses = new Dictionary<string, string>();

            //Reading the predefined responses text file and populating the dictionary
            foreach (string line in File.ReadLines(filePath))
            {
                string[] parts = line.Split(':'); //Splitting keyword and the response
                if (parts.Length == 2)
                {
                    keywordResponses[parts[0].Trim().ToLower()] = parts[1].Trim();
                }//end of if statement
            }//end of foreach loop

            bool found = false; //This checks if a keyword match is found

            //A foreach loop to look through predefined keywords and check if they exist in the user's input
            foreach (var keyword in keywordResponses.Keys)
            {
                //Using StringComparison.OrdinalIgnoreCase to use case-insensitive comparison and to detect keyword presence within larger phrases
                //An if statement to check if the user inpout contains a keyword, and if found, a message will be displayed
                if (userInput.ToLower().Contains(keyword))
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