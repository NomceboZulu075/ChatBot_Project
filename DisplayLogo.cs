using System;
using System.IO;

namespace ChatBot_Project
{
    //Changing the internal keyword to public keyword
    public class DisplayLogo
    {
        public DisplayLogo()
        {
            string logoFilePath = "bot_logo.txt";

            //An if statement to handle an error if it happens that a file does not exist
            if (File.Exists(logoFilePath))
            {
                string botLogo = File.ReadAllText(logoFilePath);
                Console.WriteLine(botLogo);
            }
            else
            {
                Console.WriteLine("Sorry! File does not exist.");
            }

        }//end of constructor 
    }//end of class 
}//end of namespace