using System.IO;
using System;

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
        }
        
    }//end of class
}//end of namespace