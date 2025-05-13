using System;
using System.Collections.Generic; //Adding an import

namespace ChatBot_Project
{
    //Changing the internal keyword to public keyword
    //This class will include a return method, and thid method will return the index found when the system randomises
    public class Randomisation
    {

        //Creating a string method that will return the final index
        public string returnIndex()
        {
            //Create an instance class for the List, this is called generic
            List <string> answers = new List<string> ();

            //Adding cybersecurity-responses that I will be randomising
            answers.Add("password:A password is a secret combination of characters used to authenticate and gain access to a system, device, application or online account.");
            answers.Add("scam:Dishonest or fraudulent scheme that tries to trick people into giving money or something of value.");
            answers.Add("privacy:Adjust your privacy settings regularly and avoid oversharing on social media.");
            answers.Add("phishing:Phishing involves tricking people into revealing sensitive information such as passwords, usernames, or credit card numbers by pretending to be a trustworthy entity.");
            answers.Add("cybersecurity:");
            answers.Add("social engineering-Social engineering is when attackers use deception to try to manipulate people into revealing sensitive information.");
            answers.Add("firewall: An internet traffic filter meant to stop unauthorized incoming and outgoing traffic.");
            answers.Add("hacker: A cyber attacker who uses software and social engineering methods to steal data and information.");
            answers.Add("firmware: Code that is embedded into the hardware of a computer.");

            //Creating the instance for the random class
            Random getIndex = new Random ();

            //Declaring a temporary variable to hold the value randomized
            int indexFound = getIndex.Next (0, answers.Count);

            //Returning the found value
            return answers [indexFound];

        }//end of return index method
















    }//end of class
}//end of namespace