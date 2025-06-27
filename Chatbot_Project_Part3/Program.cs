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
            try
            {
                //Creating an instance of a class for Voice Greeting to allow the voice greeting to play
                new VoiceGreeting() { };

                //Creating an instance of a class for Display Logo to display the ASCII logo design
                new DisplayLogo() { };


                //************************* PART 2 *********************

                //Instatiating the checkTextFile 
                CheckTextFile checkExist = new CheckTextFile();

                //Using the object called checkExist, I will use this object to get the methods in the checkTextFile class

                //First class to call: 
                checkExist.checkFile();

                //Second class to call: Get what is stored in the text file
                List<string> memory = checkExist.returnMemory();

                //Display existing memory if any
                if (memory.Count > 0)
                {
                    Console.WriteLine("Previous conversation memory found:");
                    //Using a foreach to list all memory values
                    foreach (string check in memory)
                    {
                        Console.WriteLine(check);
                    }//end of foreach loop
                    Console.WriteLine(); // Add spacing
                }//End of if statement

                //Creating an instance of a class for ChatBot Interaction that handles the interactions between the user and the chatbot, this is where keyword recognition will occur 
                new ChatBotInteraction() { };


                // Third method to call: Save any new memory(this happens automatically within ChatBotInteraction)
                Console.WriteLine("Thank you for using the Cybersecurity Awareness Chatbot!");
                Console.WriteLine("Stay safe online!");

            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
        }//end of class 
}
