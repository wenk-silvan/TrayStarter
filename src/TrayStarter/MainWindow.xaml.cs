using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using TrayStarter.Commands;

namespace TrayStarter
{
   public partial class MainWindow : Window
   {
      public MainWindow()
      {
         this.DataContext = this;
         this.Hide();

         var notifyIcon = MainWindow.CreateNotifyIcon();
         this.CreateContextMenu(notifyIcon);

         this.InitializeComponent();
      }

      private static NotifyIcon CreateNotifyIcon()
      {
         return new NotifyIcon
         {
            Text = "TrayStarter",
            Icon = new Icon("Resources/captain-america.ico"),
            Visible = true
         };
      }

      private void CreateContextMenu(NotifyIcon notifyIcon)
      {
         var contextmenu = new ContextMenu();
         notifyIcon.ContextMenu = new ContextMenu();
         try { 
            foreach (var commandName in CommandManager.Instance.Commands)
            {
               var menuitem = new MenuItem(commandName, (s, e) => this.ExecuteCommand(commandName));
               notifyIcon.ContextMenu.MenuItems.Add(menuitem);
            }
         }
         catch (FileNotFoundException ex)
         {
            MessageBoxResult result = System.Windows.MessageBox.Show(
               ex.Message.ToString() + "\n\nPlease create a command file before you launch the application. Take further information from the readme.txt file.", 
               "Missing Commandsfile", 
               MessageBoxButton.OK, 
               MessageBoxImage.Error);
            ex.ToString();
         }
         var menuItem = new MenuItem("Exit", (s, e) => this.Exit_Click(notifyIcon));
         notifyIcon.ContextMenu.MenuItems.Add(menuItem);
      }

      /// <summary>
      /// This function will execute the pressed command in the context menu.
      /// </summary>
      /// <param name="commandName"></param>
      private void ExecuteCommand(string commandName)
      {
         var command = CommandManager.Instance[commandName];
         var startInfo = MainWindow.CreateStartInfo(command);
         switch (command.RunAs)
         {
            case RunAsType.Administrator: Process.Start(startInfo); break;
            case RunAsType.User:
               ProcessHelper.StartProcessAsUser(startInfo,
                  TrayStarter.Properties.Settings.Default.Domain,
                  TrayStarter.Properties.Settings.Default.UserName,
                  TrayStarter.Properties.Settings.Default.Password);
               break;
         }
      }

      /// <summary>
      /// Creates the start information for the process.
      /// </summary>
      /// <param name="command">The command.</param>
      private static ProcessStartInfo CreateStartInfo(CommandItem command)
      {
         var startInfo = new ProcessStartInfo();
         startInfo.FileName = "cmd.exe";
         startInfo.Arguments = string.Format("/C \"{0}\"", command.Command);
         startInfo.RedirectStandardOutput = true;
         startInfo.UseShellExecute = false;
         startInfo.CreateNoWindow = true;
         return startInfo;
      }

      /// <summary>
      /// This event closes the program
      /// </summary>
      private void Exit_Click(NotifyIcon notifyicon)
      {
         notifyicon.Visible = false;
         this.Close();
      }
   }
}