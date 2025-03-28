using System;
using System.IO;

namespace ChatBot_Project
{
    public class DiplayLogo
    {
        public DiplayLogo()
        {
            string filePath = "bot_logo.txt";

            //An if statement to handle an error if it happens that a file does not exist
            if (File.Exists(filePath))
            {
                string botLogo = File.ReadAllText(filePath);
                Console.WriteLine(botLogo);
            }
            else
            {
                Console.WriteLine("Sorry! File does not exist.");
            }

        }
    }
}