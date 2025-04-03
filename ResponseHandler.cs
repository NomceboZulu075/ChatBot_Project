using System;
using System.Collections;
using System.IO;

namespace ChatBot_Project
{
    //Changing the internal keyword to public keyword
    public class ResponseHandler
    {
        //Declare a global variable to store the predefined responses from a file
        private ArrayList predefinedResponse;
        public ResponseHandler()
        {
            //Assigning predefindResponse variable to a method that loads the responses
            predefinedResponse = LoadPredefinedResponses("Predefined_Responses.txt");


        }//end of constructor

        //Creating a method to load the predefined responses from a file
        private ArrayList LoadPredefinedResponses(string responseFile)
        {
            //Declaring an arraylist to store the responses
            ArrayList chatBotResponses = new ArrayList();

            //Using a for each loop to load all the responses to an array
            foreach (string line in File.ReadAllLines(responseFile))
            {
                chatBotResponses.Add(line); 


            }

            return chatBotResponses;

        } //end of foreach

        //Creating a public method because it needs to used in other classes
        //A method to compare user input and finding the best responses

        public string GetResponse(string input)
        {

            //Look through the arraylist for user input and the corresponding response
            foreach (string line in predefinedResponse)
            {
                var parts = line.Split('-'); //Each line is going to split where there is a -
                //If statement checks if a line consists of a -
                if (parts.Length == 2 && input.ToLower().Contains(parts[0].ToLower()))
                {
                    return parts[1];
                    
                }//end of if statement

            } //end of foreach
            
            Console.ForegroundColor = ConsoleColor.Red;
            return "Sorry response not found :( Please try again!";
            Console.ResetColor();

        }// end of GetResponse method


    }//end of class

}//end of namespace