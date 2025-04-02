﻿using System;
using System.IO;

namespace ChatBot_Project
{
    //Changing the internal keyword to public keyword
    public class DisplayLogo
    {
        public DisplayLogo()
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