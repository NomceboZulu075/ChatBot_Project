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

        //A method to add chatbot responses
        private void AddChatbotResponse(string response)
        {
            DateTime now = DateTime.Now;
            string timestamp = now.ToString("yyyy-MM-dd HH:mm");
            show_chats.Items.Add($"Chatbot [{timestamp}]: {response}");
            show_chats.ScrollIntoView(show_chats.Items[show_chats.Items.Count - 1]);
        }

        // Method to add user messages
        private void AddUserMessage(string message)
        {
            DateTime now = DateTime.Now;
            string timestamp = now.ToString("yyyy-MM-dd HH:mm");
            show_chats.Items.Add($"User [{timestamp}]: {message}");
            show_chats.ScrollIntoView(show_chats.Items[show_chats.Items.Count - 1]);
        }

        //A method for when the user asks a question or gives a command
        private void ask_question(object sender, RoutedEventArgs e)
        {
            string userInput = user_question.Text.Trim();

            if (string.IsNullOrEmpty(userInput))
            {
                MessageBox.Show("Please enter a question or command!");
                return;
            }

            // Add user message to chat
            AddUserMessage(userInput);

            // Clear input field
            user_question.Text = "";

            // Process the user input
            ProcessUserInput(userInput);
        }

        // Main method to process user input and determine intent
        private void ProcessUserInput(string input)
        {
            string lowerInput = input.ToLower();

            // Check for task-related commands
            if (lowerInput.Contains("add task") || lowerInput.Contains("create task") ||
                lowerInput.Contains("new task") || lowerInput.Contains("add reminder"))
            {
                HandleAddTask(input);
            }
            else if (lowerInput.Contains("show tasks") || lowerInput.Contains("view tasks") ||
                     lowerInput.Contains("list tasks") || lowerInput.Contains("my tasks"))
            {
                HandleShowTasks();
            }
            else if (lowerInput.Contains("delete task") || lowerInput.Contains("remove task"))
            {
                HandleDeleteTask(input);
            }
            else if (lowerInput.Contains("activity log") || lowerInput.Contains("what have you done") ||
                     lowerInput.Contains("show log") || lowerInput.Contains("recent actions"))
            {
                HandleShowActivityLog();
            }
            else if (lowerInput.Contains("help") || lowerInput.Contains("commands"))
            {
                HandleHelp();
            }
            else
            {
                // Handle general cybersecurity questions or provide default response
                HandleGeneralQuery(input);
            }
        }//end of process user input method

        // A method to handle adding new tasks
        private void HandleAddTask(string input)
        {
            // Getting the title of a task from user input and generating a task description about that task
            string taskTitle = ExtractTaskTitle(input);
            string taskDescription = GenerateTaskDescription(taskTitle);

            // Create new task
            var newTask = new taskInformation
            {
                Title = taskTitle,
                Description = taskDescription,
                IsCompleted = false,
                CreatedDate = DateTime.Now
            };

            // Check if user wants a reminder
            DateTime? reminderDate = ExtractReminderDate(input);
            if (reminderDate.HasValue)
            {
                newTask.ReminderDate = reminderDate;
            }

            // Add task to the list
            cyberTasks.Add(newTask);
            AddToActivityLog($"Task added: '{taskTitle}'");

            // Refreshing display and update statistics
            RefreshTaskDisplay();

            // Provide response with emojis
            string response = $"✅ Task added: '{taskTitle}' - {taskDescription}";
            if (reminderDate.HasValue)
            {
                response += $" ⏰ I'll remind you on {reminderDate.Value:MM/dd/yyyy}.";
            }
            else
            {
                response += " Would you like to set a reminder for this task?";
            }

            AddChatbotResponse(response);

            // Fun encouragement based on task count
            if (cyberTasks.Count == 1)
                AddChatbotResponse(" Great start! Your first cybersecurity task is logged!");
            else if (cyberTasks.Count == 5)
                AddChatbotResponse(" You're on fire! 🔥 5 tasks and counting - security champion!");

        }//end of method handle add task

        // Extract task title from user input
        private string ExtractTaskTitle(string input)
        {
            // Simple extraction - look for common cybersecurity tasks
            string lowerInput = input.ToLower();

            if (lowerInput.Contains("two-factor") || lowerInput.Contains("2fa"))
                return "Enable Two-Factor Authentication";
            else if (lowerInput.Contains("password"))
                return "Update Password";
            else if (lowerInput.Contains("privacy"))
                return "Review Privacy Settings";
            else if (lowerInput.Contains("backup"))
                return "Create Data Backup";
            else if (lowerInput.Contains("antivirus"))
                return "Update Antivirus Software";
            else if (lowerInput.Contains("firewall"))
                return "Check Firewall Settings";
            else if (lowerInput.Contains("software update"))
                return "Install Software Updates";

            else
            {
                // Try to extract from the input after "add task"
                string[] parts = input.Split(new string[] { "add task", "create task", "new task" },
                    StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1)
                {
                    return parts[1].Trim();
                }
                return "General Cybersecurity Task";
            }//end of else statement
        }//end of extract task title method

        // Generate appropriate descriptions for each task
        private string GenerateTaskDescription(string title)
        {
            switch (title.ToLower())
            {
                case "enable two-factor authentication":
                    return "Set up 2FA on your important accounts to add an extra layer of security";
                case "update password":
                    return "Change your password to a strong, unique combination of special characteres, numbers and letters";
                case "review privacy settings":
                    return "Check and update privacy settings on your social media and online accounts";
                case "create data backup":
                    return "Backup important files to prevent data loss";
                case "update antivirus software":
                    return "Ensure your antivirus software is up to date at all times";
                case "check firewall settings":
                    return "Verify your firewall is properly configured";
                case "install software updates":
                    return "Install the latest security updates for your software";
                default:
                    return "Complete this cybersecurity task to improve your digital safety";
            }//end of switch case
        }//end of generate task description

        // Extract reminder date from user input
        private DateTime? ExtractReminderDate(string input)
        {
            string lowerInput = input.ToLower();

            if (lowerInput.Contains("tomorrow"))
                return DateTime.Now.AddDays(1);
            else if (lowerInput.Contains("next week"))
                return DateTime.Now.AddDays(7);
            else if (lowerInput.Contains("3 days"))
                return DateTime.Now.AddDays(3);
            else if (lowerInput.Contains("5 days"))
                return DateTime.Now.AddDays(5);
            else if (lowerInput.Contains("1 week"))
                return DateTime.Now.AddDays(7);
            else if (lowerInput.Contains("2 weeks"))
                return DateTime.Now.AddDays(14);

            return null;
        }//end of extract reminder date method

        // A method to handle showing all the tasks
        private void HandleShowTasks()
        {
            if (cyberTasks.Count == 0)
            {
                AddChatbotResponse("You don't have any tasks yet. Would you like to add a cybersecurity task?");
                return;
            }

            string response = "Here are your cybersecurity tasks:\n";
            for (int i = 0; i < cyberTasks.Count; i++)
            {
                var task = cyberTasks[i];
                string status = task.IsCompleted ? "✓ Completed" : "○ Pending";
                string reminder = task.ReminderDate.HasValue ? $" (Reminder: {task.ReminderDate.Value:MM/dd/yyyy})" : "";
                response += $"{i + 1}. {status} - {task.Title}{reminder}\n";
            }

            AddChatbotResponse(response);
        }

        // A method to handle deleting tasks
        private void HandleDeleteTask(string input)
        {
            //User will double click on the task they wishto delete
            AddChatbotResponse("To delete a task, please double-click on a completed task in the list above.");
        }//end of handle delete task method 

        // Handle showing activity log
        private void HandleShowActivityLog()
        {
            if (activityLog.Count == 0)
            {
                AddChatbotResponse("No recent activities to show.");
                return;
            }

            string response = "Here's your recent activity:\n";
            for (int i = Math.Max(0, activityLog.Count - 5); i < activityLog.Count; i++)
            {
                response += $"• {activityLog[i]}\n";
            }

            AddChatbotResponse(response);
        }//end of handle show activity log method

        // A method to handle help command, this guides the user 
        private void HandleHelp()
        {
            string helpText = "Cybersecurity Awareness Chatbot Commands:\n" +
                            "• 'Add task [description]' - Add a new cybersecurity task\n" +
                            "• 'Show tasks' - View all your tasks\n" +
                            "• 'Activity log' - See recent actions\n" +
                            "• Double-click on tasks to mark them complete\n" +
                            "• Ask me about cybersecurity topics for advice!";

            AddChatbotResponse(helpText);
        }//end of handle help method








