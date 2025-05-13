using System.IO;
using System;
using System.Security.Policy;
using System.Collections.Generic;
using System.Linq;

namespace ChatBot_Project
{
    //Changing the internal keyword to public keyword
    public class CheckTextFile
    {
        //Creating a string method that will return a path
        //AppDomain as a global gives an error, therefore, I will do a return method that will return the path
        private string returnPath()
        {
            //Using an AppDomain to get me the full path
            string fullPath = AppDomain.CurrentDomain.BaseDirectory;

            //Replacing the bin/debug/ so it can get the path of the text file
            string newPath = fullPath.Replace("bin\\Debug\\", "");

            //Combining the path of the text file with the updated path
            string path = Path.Combine(newPath, "MemoryRecallFile.txt");

            return path;
        }// end of return path method

        //Here, I will be creating the three methods. First method will check and create the file if not found
        public void checkFile()
        {
            //Getting the path of the Memory Recall text file
            string textPath = returnPath();

            //Checking if the file exists or not, and if not found it will be created
            if (!File.Exists(textPath)) 
            { 
                //The ! tells me that if the file is not found in the path, it will be created
                File.CreateText(textPath);
                Console.WriteLine("Memory Recall Text File created successfully!");    
            }
            else
            {
                //If the file is found in the path, a message will be displayed
                Console.WriteLine("File is found...");
            }//End of if-else statement

        }//end of checkFile method

        //Here, I will be doing the second method where I will get what is stored in the text file 
        public List<string> returnMemory()
        {
            //Getting the path of the file
            string path = returnPath();
            List<string> fileContents = new List<string>();

            try { 
            if (File.Exists(path))
            {
                fileContents = File.ReadAllLines(path).ToList();
            }
            else
            {
                Console.WriteLine("File not found...");
            }
        }
        catch (Exception e)
            {
                Console.WriteLine($"Error Reading File: {e.Message}");
            }//end of try and catch

            return fileContents;
            
        }//end of returnMemory method

        //Here, I will be doing the third method where I will be writing and adding to the text file
        //For the parameter, I am going to pass the List
        public void saveMemory(List<string> saveNew)
        {
            //Getting the path of the text file
            string path = returnPath();

            //Writing into the text file
            File.WriteAllLines(path, saveNew);

        } //end of saveMemory method


    }//end of class
}//end of namespace