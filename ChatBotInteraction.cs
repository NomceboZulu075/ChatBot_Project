using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ChatBot_Project
{
    //Change internal keyword to public
    public class ChatBotInteraction
    {
        // Handles natural language responses
        private ResponseHandler responseHandler;

        //Used to select random responses
        private Random random = new Random();

        // Stores topics the user has shown interest in
        private List<string> userInterests = new List<string>();

        // Dictionary mapping keywords to lists of pre-defined cybersecurity responses
        private Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>
        {
            { "password", new List<string> {
                "Strong passwords are your first line of defense! Use a mix of letters, numbers, and symbols.",
                "I recommend using different passwords for each account to prevent widespread breaches.",
                "Consider using a password manager to keep track of your complex passwords safely.",
                "Make sure to use strong, unique passwords for each account. Avoid using personal details in your passwords."
            }},
            { "scam", new List<string> {
                "Be wary of offers that seem too good to be true - they usually are!",
                "Legitimate organizations won't ask for sensitive information via email or text.",
                "When in doubt about a message, contact the company directly using their official website.",
                "Scammers often create fake websites that look legitimate. Always verify URLs carefully."

            }},
            { "privacy", new List<string> {
                "Regularly review privacy settings on your social media accounts.",
                "Consider using a VPN when connecting to public Wi-Fi networks.",
                "Be mindful of what personal information you share online - it can be difficult to remove.",
                "Privacy is a crucial part of staying safe online. Always read privacy policies before sharing data."
            }},
            { "phishing", new List<string> {
                "Phishing is a cyber-attack where hackers trick you into revealing personal info by pretending to be someone you trust.",
                "Hover over links before clicking to verify the actual URL destination.",
                "Check for spelling errors or unusual formatting in emails - these are red flags.",
                "Be suspicious of urgent requests demanding immediate action."
            }},
        }; //end of random response dictionary

        // Sentiment categories and the words associated with each feeling
        private Dictionary<string, List<string>> sentiments = new Dictionary<string, List<string>>
        {
            { "worried", new List<string> { "worried", "concerned", "afraid", "scared", "anxious", "nervous", "fear", "frightened" } },
            { "curious", new List<string> { "curious", "interested", "wondering", "want to know", "tell me about", "learn", "how", "what" } },
            { "frustrated", new List<string> { "frustrated", "confused", "difficult", "hard", "complicated", "annoying", "struggle", "don't understand" } },
            { "positive", new List<string> { "thanks", "helpful", "great", "good", "excellent", "amazing", "appreciate", "wonderful", "awesome" } }
        }; //end of sentiment dictionary

        // A method that adjusts chatbot tone based on detected sentiment
        private void AdjustResponseForSentiment(string sentiment)
        {
            switch (sentiment)
            {
                case "worried":
                    Console.WriteLine("I understand this may be concerning. Let me help you feel more secure...", ConsoleColor.Yellow);
                    break;
                case "frustrated":
                    Console.WriteLine("I appreciate your patience. Let us work through this together step by step.", ConsoleColor.Yellow);
                    break;
                case "curious":
                    Console.WriteLine("Curiosity is the first step to discovery—let’s explore this together!", ConsoleColor.Yellow);
                    break;
                case "positive":
                    Console.WriteLine("I love that enthusiasm! Keep spreading the good energy!", ConsoleColor.Yellow);
                    break;
            }//end of switch
        }//end of method adjust response for sentiment

        // Memory of the conversation and file for persistent storage
        private List<string> conversationMemory;
        private string memoryFilePath = "MemoryRecallFile.txt";

        // Constructor: initializes memory, response handler, and starts chat
        public ChatBotInteraction()
        {
            responseHandler = new ResponseHandler();
            string fullPath = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(fullPath, memoryFilePath);
            conversationMemory = LoadMemory(path);
            BeginChat();
        }//end of constructor

        // Returns a random response for a given topic keyword
        private string GetRandomResponse(string topic)
        {
            if (keywordResponses.ContainsKey(topic))
            {
                var responses = keywordResponses[topic];
                return responses[random.Next(responses.Count)];
            }
            return "I need to learn more about that topic. Could you ask something else related to Cybersecurity?";
        }//end of method get random response

        // Main interaction loop of the chatbot
        private void BeginChat()
        {
            string userName = "You";
            string chatBotName = "CyberBot";

            // Display welcome message
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------- ");
            Console.WriteLine("  **************************************************************");
            typingEffect("  Welcome to the South African CyberSecurity Awareness Chatbot!", ConsoleColor.DarkCyan);
            Console.WriteLine("  **************************************************************");

            // Ask for and validate the user's name
            typingEffect(" Here to help you spot phishing emails, craft uncrackable passwords and browse the web like a pro. \n ", ConsoleColor.DarkCyan);
            typingEffect($" {chatBotName}: Hello! My name is CyberBot.", ConsoleColor.DarkGray);
            typingEffect($" {chatBotName}: What is your name? ", ConsoleColor.DarkGray);

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(userName + ": ");
            userName = Console.ReadLine();
            Console.ResetColor();

            string pattern = @"^[a-zA-Z ]+$";

            while (string.IsNullOrEmpty(userName) || !Regex.IsMatch(userName, pattern))
            {
                typingEffect($" {chatBotName}: Invalid! Your name must not contain any numbers or special characters.", ConsoleColor.Red);
                typingEffect($" {chatBotName}: What is your name? ", ConsoleColor.DarkGray);
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("You: ");
                userName = Console.ReadLine();
                Console.ResetColor();
            }

            // Ask how the user is feeling
            typingEffect($" {chatBotName}: How are you feeling today {userName}?", ConsoleColor.DarkGray);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(userName + ": ");
            string userFeeling = Console.ReadLine();

            // Respond based on mood keywords
            if (userFeeling.Contains("good") || userFeeling.Contains("great") || userFeeling.Contains("happy") || userFeeling.Contains("okay"))
            {
                typingEffect($"{chatBotName}: I'm glad to hear that {userName}!", ConsoleColor.DarkGray);
            }
            else if (userFeeling.Contains("sad") || userFeeling.Contains("not good") || userFeeling.Contains("bad"))
            {
                typingEffect($"{chatBotName}: I'm sorry to hear that {userName}. If you need a distraction, I can share some cybersecurity tips.", ConsoleColor.DarkGray);
            }
            else if (userFeeling.Contains("tired") || userFeeling.Contains("stressed"))
            {
                typingEffect($"{chatBotName}: Take a break and relax {userName}! Also, remember to stay safe from cyber threats.", ConsoleColor.DarkGray);
            }
            else
            {
                typingEffect($"{chatBotName}: I see! If you'd like, I can share some security tips or fun facts.", ConsoleColor.DarkGray);
            }

            // Main conversation loop
            while (true)
            {
                typingEffect($" {chatBotName}: So, how may I be of assistance to you today {userName}?", ConsoleColor.DarkGray);
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write(userName + ": ");
                string userInput = Console.ReadLine();
                Console.ResetColor();

                // Error handling for empty input
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    typingEffect($" {chatBotName}: I didn't catch that. Could you please say something?", ConsoleColor.Red);
                    continue;
                }

                // End chat on "exit"
                if (userInput.ToLower() == "exit")
                {
                    typingEffect($" {chatBotName}: Goodbye {userName}! Stay safe online!", ConsoleColor.DarkGray);
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------------- ");
                    break;
                }//end of if statement

                //Save the message to memory
                conversationMemory.Add($"{userName}: {userInput}");
                SaveMemory();



                // Check for sentiment and adjust response

                string detectedSentiment = DetectSentiment(userInput);
                if (!string.IsNullOrEmpty(detectedSentiment))
                {
                    AdjustResponseForSentiment(detectedSentiment);
                }

                // Check if the user asks what the bot remembers
                if (userInput.ToLower().Contains("what do you remember") || userInput.ToLower().Contains("do you remember"))
                {
                    // Respond with remembered topics, if any, I used an if statement
                    if (userInterests.Count > 0)
                    {
                        typingEffect($"{chatBotName}: I remember you're interested in: {string.Join(", ", userInterests)}.", ConsoleColor.DarkGray);
                    }
                    else
                    {
                        typingEffect($"{chatBotName}: I don't remember you mentioning any specific interests yet.", ConsoleColor.DarkGray);
                    }
                    continue;
                }//end of if-else statement

                // Generate response via response handler
                string chatBotResponse = responseHandler.GetResponse(userInput);
                typingEffect($" {chatBotName}: {chatBotResponse}", ConsoleColor.DarkGray);

                // Check if the user mentions interest in a topic
                Match interestMatch = Regex.Match(userInput, @"interested in (\w+)", RegexOptions.IgnoreCase);
                if (interestMatch.Success)
                {
                    // Extract the topic mentioned by the user
                    string interest = interestMatch.Groups[1].Value.ToLower();

                    // Add the interest to memory if it's not already recorded
                    if (!userInterests.Contains(interest))
                    {
                        userInterests.Add(interest);
                        typingEffect($"{chatBotName}: Great! I'll remember that you're interested in {interest}. It's a crucial part of staying safe online.", ConsoleColor.DarkGray);


                        // Provide personalized follow-up based on interest
                        if (keywordResponses.ContainsKey(interest))
                        {
                            typingEffect($"Cybersecurity Tip: {GetRandomResponse(interest)}", ConsoleColor.Green);

                        }
                        continue;
                    }//end of if statement 2
                }//end of if statement 1


                // Check for keywords and give related tips
                bool foundKeyword = false;

                foreach (var keyword in keywordResponses.Keys)
                {
                    if (userInput.ToLower().Contains(keyword))
                    {
                        Console.WriteLine($"Keyword Detected: '{keyword}'");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{chatBotName} is randomising responses...");
                        Console.ResetColor();

                        // Update memory with detected interest
                        UpdateMemory(userInput);

                        // Provide personalized response if user has shown interest before
                        if (userInterests.Contains(keyword))
                        {
                            typingEffect($"As someone interested in {keyword}, here's another tip:", ConsoleColor.Yellow);
                        }//end of if-statement

                        typingEffect($"Cybersecurity Tip: {GetRandomResponse(keyword)}", ConsoleColor.Green);
                        foundKeyword = true;
                        break;
                    }//end of if statement
                }//end of foreach loop

                // If no known keyword, try response handler first, then recall
                if (!foundKeyword)
                {
                    string response = responseHandler.GetResponse(userInput);
                    if (!response.Contains("Sorry response not found"))
                    {
                        typingEffect($" {chatBotName}: {response}", ConsoleColor.DarkGray);
                    }
                    else
                    {
                        // Try recall if response handler fails
                        string recallResponse = RecallPreviousMessage(userInput);
                        typingEffect($" {chatBotName}: {recallResponse}", ConsoleColor.DarkGray);
                    }
                }//End of if statement

                    }//end of while loop
                }//end of beginChat method

        // Detects sentiment from user input
        private string DetectSentiment(string userInput)
        {
            string input = userInput.ToLower();
            foreach (var sentimentCategory in sentiments)
            {
                foreach (var keyword in sentimentCategory.Value)
                {
                    if (input.Contains(keyword))
                    {
                        return sentimentCategory.Key;
                    }
                }
            }
            return null;
        }//end of detect sentiment method

        // Attempts to recall previous similar messages from the conversation
        private string RecallPreviousMessage(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                return "I'm not sure about that topic, but always prioritize online security!";
            }

            foreach (string pastMessage in conversationMemory)
            {
                if (pastMessage.Contains(userInput))
                {
                    return $"Great! I'll remember that you're interested in {userInput}. It is an important part of staying safe online.";
                }
            }

            return "That's an interesting point. Let me gather more on that topic for next time.";
        }//end of if statement recall previous message

        // Saves the chat history to a file
        private void SaveMemory()
        {
            string fullPath = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(fullPath, memoryFilePath);
            File.WriteAllLines(path, conversationMemory); 
        }//end of save memory method

        // Loads memory from file if it exists, or creates a new file
        private List<string> LoadMemory(string path)
        {
            if (File.Exists(path)) // 
            {
                return new List<string>(File.ReadAllLines(path));
            }
            else
            {
                File.CreateText(path).Close();
                return new List<string>();
            }
        }// end of loadmemory method

        // Updates interest memory based on keyword match
        private void UpdateMemory(string userInput)
        {
            foreach (string topic in keywordResponses.Keys)
            {
                if (userInput.Contains(topic) && !userInterests.Contains(topic))
                {
                    userInterests.Add(topic);
                }
            }
        }//end of update memory


        // Displays a typing animation for bot responses
        private void typingEffect(string response, ConsoleColor color)
        {
            int speed = 40;
            Console.ForegroundColor = color;

            foreach (char character in response)
            {
                Console.Write(character);
                System.Threading.Thread.Sleep(speed);
            }

            Console.WriteLine();
            Console.ResetColor();
        }//end of typing effect method
    }//end of class
}//end of namespace
