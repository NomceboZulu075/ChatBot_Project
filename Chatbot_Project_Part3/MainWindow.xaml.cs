using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Chatbot_Project_Part3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Task class to store task information
        public class taskInformation
        {
           public string Title { get; set; }
           public string Description { get; set; }
           public DateTime? ReminderDate { get; set; }
           public bool IsCompleted { get; set; }
           public DateTime CreatedDate { get; set; }

            public override string ToString()
            {
                string status = IsCompleted ? "[COMPLETED]" : "[PENDING]";
                string reminder = ReminderDate.HasValue ? $" (Reminder: {ReminderDate.Value:MM/dd/yyyy})" : "";
                return $"{status} {Title} - {Description}{reminder}";
            }//end of ToString method
        }//end of taskInformation method

        public MainWindow()
        {
            InitializeComponent();
        }

    }


        

    }


