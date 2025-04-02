using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;

namespace ChatBot_Project
{
    //Changing the internal keyword to public keyword
    public class DisplayLogo
    {
        public DisplayLogo()
        {
            //Get the full path of the file
            string logoPath = AppDomain.CurrentDomain.BaseDirectory;

            //Replace the bin\\Debug\\
            string newLogoPath = logoPath.Replace("bin\\Debug", "");

            //
            string fullPath = Path.Combine(newLogoPath, "Cybersecurity_Picture.jpg");

            //Time to start working on the logo using the ASCII

            Bitmap logoImage = new Bitmap(fullPath);
            logoImage = new Bitmap(logoImage, new Size(110, 50));

            //A nested for loop for the inner and outer 

            for(int height = 0; height < logoImage.Height; height++)
                //This outer for loop is for the height of the logo
            {
                //Working on the inner loop for the width of the logo
                for (int width = 0; width < logoImage.Width; width++)
                {
                    //Inside the inner loop, we are going to work on the ASCII design

                    Color pixelColor = logoImage.GetPixel(width, height);
                    int colour = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;


                    //Now make use of char
                    char ascii_design = colour > 200 ? '.' : colour > 150 ? '*' : colour > 100 ? 'O' : colour > 50 ? '#' : '@';

                    Console.Write(ascii_design); //Displaying the ascii design 


                }//end of inner loop 

                Console.WriteLine(); //Skipping the line 
                

            }//end of outer for loop 
            
            
            
            
            
            
            
            
            
            
         
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            /* string logoFilePath = "bot_logo.txt";

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

            */
        }//end of constructor 
    }//end of class 
}//end of namespace