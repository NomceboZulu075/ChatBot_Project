using System.IO;
using System;
using System.Security.Policy;

namespace ChatBot_Project
{
    //Changing the internal keyword to public keyword
    public class checkTextFile
    {
        //Creating a string method that will return a path
        //AppDomain as a global gives an error, therefore, I will do a return method that will return the path
        private string returnPath()
        {
            //Using an AppDomain to get me the full path
            string fullPath = AppDomain.CurrentDomain.BaseDirectory;

            //Replacing the bin\debug\ so it can get the path of the text file
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
                //
            }


        }//end of checkFile method

    }//end of class
}//end of namespace