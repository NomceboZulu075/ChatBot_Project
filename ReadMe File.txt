---------------------------------------
| CYBER SECURITY AWARENESS AI CHATBOT |
---------------------------------------

**********************************************************************************************************************************************
## DESCRIPTION

This project is a chatbot console application designed using C# to raise awareness on cybersecurity.
The purpose of this project is to educate users by providing basic cybersecurity knowledge, such as recognizing phishing emails, setting up strong passwords and avoiding online scams. It also allows users to ask any question related to cybersecurity and gives them answers on the best practices for staying safe online.


**********************************************************************************************************************************************
## FEATURES

- User Interaction: The chatbot interacts with the user using simple and easy-to-understand language to ask for the user's name and guide them through a conversation about cybersecurity. It also interacts with the user using a command-line interface (CLI).

- Cyber Security Threat Awareness: The chatbot responds to the user's questions about cybersecurity and the common cyber threats such as phishing, safe browsing, malware, man-in-the-middle attacks and more.

- Typing Effect: The chatbot provides a typing effect to enhance user experience (making the chatbot more human-like), improves readability by preventing information overload and it also builds anticipation for the users.

- Newly Added Feature: Sentiment Detection:The chatbot now detects user emotions like worry, curiosity, frustration, or positivity based on keywords in the conversation. It adjusts its tone to respond more empathetically or supportively.

- Newly Added Feature: Keyword-Based Cybersecurity Tips: The bot includes a dictionary of common cybersecurity topics with multiple randomized educational responses for Password, Scam, Privacy and Phishing. 
Each topic is recognized via keyword matching, and a different tip is selected randomly for each interaction to keep the conversation dynamic.

- Newly Added Feature: Memory Recall System: The chatbot remembers past user interests during the session.
Users can ask: "What do you remember?" and CyberBot will list the topics they’ve shown interest in, and these interests are stored in a file (MemoryRecallFile.txt) to simulate persistent memory.

- Exit Functionality: The user can end the chat by typing "exit".


**********************************************************************************************************************************************
## PRECONDITIONS

Before running the project, it should be ensured that the following are installed on your machine:
- .NET Framework or any other .NET runtime that is compatible with your version of C#.
- A Text File for the responses: A file named predefined.txt with the responses in this format: keyword-response

**********************************************************************************************************************************************
## SETUP AND INSTALLATION

1. Clone the repository to your local machine. The reason for cloning the repository is to create a local copy of the code to our local machine, and this allows us to work on the project locally.
2. Open the project in a C# IDE that you prefer.
3. Ensure that the predefined.txt file placed in the root directory of your project.
If the file is missing, you can create another using the keyword-response format in order for the chatbot to recognize it. For example, cybersecurity-Cybersecurity is the practice of protecting digital devices...
4. Build you project and thereafter run it.

**********************************************************************************************************************************************
## HOW TO USE

1. Firstly, run the application.
2. The bot will thereafter welcome you and tell you what it's purpose is.
3. The chatbot will then greet you personally and tell you its name. It will also ask you what your name is.
4. After getting your response, it will ask the user how they are feeling on that day and the user should give an appropriate response.
5. According to how the user responded on how they are feeling, the chatbot will give an appropriate response to the user's feelings:
ChatBot: How are you feeling today?
User: I am feeling good
ChatBot: I'm glad to hear that!
6. Start asking questions about cybersecurity such as "What is phishing" or "What are some safe password practices".
7. The chatbot will respond based on the questions and the data stored in the Predefined_Responses.txt file.
8. To end the conversation, the user should type "exit".


**********************************************************************************************************************************************
## CODE EXPLANATION

1. Program Class- This is the main class and it is where the instance classes for Display Logo, Voice Greeting and ChatBot Interaction are created.
2. Display Logo Class-  This class allows the ASCII art logo to be displayed right before the actual program begins to show the users that this AI chatbot is about cybersecurity.
3. Voice Greeting Class- This class allows the welcome message of the chatbot to play as the user begins the program.
4. PlayWav Method- A method to allow the .WAV audio to play till the end.
5. Response Handler Class- This class handles the responses by firstly loading the responses on a text file and finding the best response for the user, allowing for an excellent user experience.
6. LoadPredefinedResponses Method- method that loads predefined responses from the Predefined_Responses.txt file
7. Chatbot Interaction Class- This class is responsible for managing how users can interact with the chatbot.
8. TypingEffect Method- simulates a typing effect when the chatbot sends a response back to the user.  
9. Newly Added Text File- called MemoryRecallFile.txt and it stores user-entered messages and interests.


**********************************************************************************************************************************************
## INTERACTION EXAMPLE

ChatBot: Hello! My name is CyberBot!
ChatBot: What is your name?
You: Brynnah
ChatBot: How are you feeling today Brynnah?
Brynnah: I am feeling good CyberBot!
ChatBot: I'm glad to hear that Brynnah!
ChatBot: So, how may I be of assistance to you Brynnah?
Brynnah: I would like to ask about phishing
ChatBot: Phishing involves tricking people into revealing information such as passwords, usernames, or credit card numbers by pretending to be a trusted entity.
ChatBot: So, how may I be of assistance to you Brynnah?
Brynnah: exit
ChatBot: Goodbye Brynnah!

**********************************************************************************************************************************************