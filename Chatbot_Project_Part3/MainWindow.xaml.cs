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
using static Chatbot_Project_Part3.MainWindow;


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

        //A List to store all inforamtion of the tasks
        private List<taskInformation> cyberTasks = new List<taskInformation>();

        // Activity log for tracking actions
        private List<string> activityLog = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            AddToActivityLog("Cybersecurity Awareness Chatbot started");
            UpdateStatistics();
            ShowWelcomeMessage();
        }//end of MainWindow


        //A method to show a fun welcome message
        private void ShowWelcomeMessage()
        {
            AddChatbotResponse("Welcome to the South African Awareness CyberSecurity Awareness Chatbot! I'm here to help you stay safe online! 🛡️ ");
            AddChatbotResponse("✨ Try saying: 'add task', 'show tasks', 'help', or ask me about cybersecurity!");
        }

        //A method to update the fun statistics display
        private void UpdateStatistics()
        {
            int completedTasks = cyberTasks.Count(i => i.IsCompleted);
            int totalTasks = cyberTasks.Count;
            int securityScore = totalTasks > 0 ? (completedTasks * 100) / totalTasks : 0;

            completed_count.Text = completedTasks.ToString();
            total_count.Text = totalTasks.ToString();
            security_score.Text = securityScore + "%";

            // Update status based on score
            if (securityScore >= 80)
                status_text.Text = "🌟 Security Expert!";
            else if (securityScore >= 60)
                status_text.Text = "🔒 Getting Secure!";
            else if (securityScore >= 40)
                status_text.Text = "⚠️ Needs Work";
            else if (totalTasks > 0)
                status_text.Text = "🚨 Security Risk!";
            else
                status_text.Text = "I am ready to help!";

        }//end of update statistics method

        //A method to add entries to the activity log
        private void AddToActivityLog(string action)
        {
            string timestamp = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            activityLog.Add($"{timestamp}: {action}");

            // Keep only last 10 entries
            if (activityLog.Count > 10)
            {
                activityLog.RemoveAt(0);
            }//end of if statement
        }//end of add activity log method

        // When the task is double clicked on the list view
        private void show_chats_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (show_chats.SelectedItem == null) return;

            string selectedItem = show_chats.SelectedItem.ToString();

            // Check if this is a task item (contains task information)
            if (selectedItem.Contains("[PENDING]") || selectedItem.Contains("[COMPLETED]"))
            {
                // Find the corresponding task
                var task = cyberTasks.FirstOrDefault(t => selectedItem.Contains(t.Title));
                if (task != null)
                {
                    // Toggle completion status
                    task.IsCompleted = !task.IsCompleted;
                    string action = task.IsCompleted ? "completed" : "marked as pending";
                    AddToActivityLog($"Task '{task.Title}' {action}");

                    // Refresh the display
                    RefreshTaskDisplay();

                    // Add chatbot response
                    string response = task.IsCompleted ?
                        $"Amazing work! Task '{task.Title}' has been marked as completed. Keep up the good cybersecurity practices!" :
                        $"Task '{task.Title}' has been marked as pending again.";

                    AddChatbotResponse(response);
                }//end of inner if statement
            }//end of other outer if statement
        }//end of method showchatsMouseDoubleClick


        //A method to refresh task display in ListView
        private void RefreshTaskDisplay()
        {
            // Clear current display
            var chatItems = show_chats.Items.Cast<string>().Where(item =>
                !item.Contains("[PENDING]") && !item.Contains("[COMPLETED]")).ToList();

            show_chats.Items.Clear();

            // Add back non-task chat items
            foreach (var item in chatItems)
            {
                show_chats.Items.Add(item);
            }

            // Add all tasks
            foreach (var task in cyberTasks)
            {
                show_chats.Items.Add(task.ToString());
            }

            // Update stats after refreshing
            UpdateStatistics();

            // Auto scroll to bottom
            if (show_chats.Items.Count > 0)
            {
                show_chats.ScrollIntoView(show_chats.Items[show_chats.Items.Count - 1]);
            }
        }

        // Method to add chatbot responses
        private void AddChatbotResponse(string response)
        {
            DateTime now = DateTime.Now;
            string timestamp = now.ToString("yyyy-MM-dd HH:mm");
            show_chats.Items.Add($"Chatbot [{timestamp}]: {response}");
            show_chats.ScrollIntoView(show_chats.Items[show_chats.Items.Count - 1]);
        }









